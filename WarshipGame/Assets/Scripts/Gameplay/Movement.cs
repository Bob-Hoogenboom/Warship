using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private BFSResult _movementRange = new BFSResult();
    private List<Vector2Int> _currentPath = new List<Vector2Int>();

    public void HideRange(HexGrid hexGrid)
    {
        foreach (Vector2Int hexPosition in _movementRange.GetRangePositions())
        {
            hexGrid.GetTileAt(hexPosition).DisableHighlight();
        }

        _movementRange = new BFSResult();
    }

    public void ShowRange(Ship selectedShip, HexGrid hexGrid)
    {
        CalculateRange(selectedShip, hexGrid);
        foreach (Vector2Int hexPosition in _movementRange.GetRangePositions())
        {
            hexGrid.GetTileAt(hexPosition).EnableHighlight();
        }
    }

    public void CalculateRange(Ship selectedShip, HexGrid hexGrid)
    {
        _movementRange = GraphSearch.BFSGetRange(hexGrid, hexGrid.GetClosestHex(selectedShip.transform.position), selectedShip.MovementPoints);
    }

    public void ShowPath(Vector2Int selectedHexPosition, HexGrid hexGrid)
    {
        if (_movementRange.GetRangePositions(). Contains(selectedHexPosition))
        {
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
    }

    public void MoveShip(Ship selectedShip, HexGrid hexGrid)
    {
        Debug.Log("moving Ship " + selectedShip.name);
        selectedShip.MoveTroughPath(_currentPath.Select(pos=> hexGrid.GetTileAt(pos).transform.position).ToList());
    }

    public bool IsHexInRange(Vector2Int hexPosition)
    {
        return _movementRange.IsHExPositionInRange(hexPosition);
    }
}