using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Handles the movement of the Ships
/// </summary>
public class Movement : MonoBehaviour
{
    private BFSResult _movementRange;
    private List<Vector2Int> _currentPath = new();

    /// <summary>
    /// Hides the Range of the currently selected Ships
    /// </summary>
    /// <param name="hexGrid"></param>
    public void HideRange(HexGrid hexGrid)
    {
        foreach (Vector2Int hexPosition in _movementRange.GetRangePositions())
        {
            hexGrid.GetTileAt(hexPosition).DisableHighlight();
        }

        _movementRange = new BFSResult();
    }

    /// <summary>
    /// Draws the Range of the currently selected Ship
    /// </summary>
    /// <param name="selectedShip"></param>
    /// <param name="hexGrid"></param>
    public void ShowRange(Ship selectedShip, HexGrid hexGrid)
    {
        CalculateRange(selectedShip, hexGrid);

        Vector2Int shipPos = hexGrid.GetClosestHex(selectedShip.transform.position);
        
        foreach (Vector2Int hexPosition in _movementRange.GetRangePositions())
        {
            if (shipPos == hexPosition) continue;
            hexGrid.GetTileAt(hexPosition).EnableHighlight();
        }
    }
    
    private void CalculateRange(Ship selectedShip, HexGrid hexGrid)
    {
        _movementRange = GraphSearch.BFSGetRange(hexGrid, hexGrid.GetClosestHex(selectedShip.transform.position), selectedShip.MovementPoints);
    }

    /// <summary>
    /// Highlights the currently selected path for the currently selected Ship
    /// </summary>
    /// <param name="selectedHexPosition"></param>
    /// <param name="hexGrid"></param>
    public void ShowPath(Vector2Int selectedHexPosition, HexGrid hexGrid)
    {
        if (!_movementRange.GetRangePositions().Contains(selectedHexPosition)) return;
        foreach (Vector2Int hexPosition in _currentPath)
        {
            hexGrid.GetTileAt(hexPosition).ResetHighlight();
        }

        _currentPath = _movementRange.GetPathTo(selectedHexPosition);
        foreach (Vector2Int hexPosition in _currentPath)
        {
            hexGrid.GetTileAt(hexPosition).HighlightPath();
        }
    }
    
    public void MoveShip(Ship selectedShip, HexGrid hexGrid)
    {
        selectedShip.MoveTroughPath(_currentPath.Select(pos=> hexGrid.GetTileAt(pos).transform.position).ToList());
    }

    public bool IsHexInRange(Vector2Int hexPosition)
    {
        return _movementRange.IsHexPositionInRange(hexPosition);
    }
}