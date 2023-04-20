using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains all the data of the grid, position of tiles [Vector2Int Coordinates]
/// </summary>
public class HexGrid : MonoBehaviour
{
    private readonly Dictionary<Vector2Int, HexData> _hexTileDict = new();
    public readonly Dictionary<Vector2Int, List<Vector2Int>> _hexTileNeighboursDict = new();

    private HexCoordinates _hexCoords;
    
    
    //Fills dictionary with hex tiles
    private void Start()
    {
        _hexCoords = FindObjectOfType<HexCoordinates>();
        
        foreach (HexData hex in FindObjectsOfType<HexData>())
        {
            _hexTileDict[hex.Grid] = hex;
        }
    }

    /// <summary>
    /// Returns the coordinate value of a tile
    /// </summary>
    /// <param name="hexCoordinates"></param>
    /// <returns></returns>
    public HexData GetTileAt(Vector2Int hexCoordinates)
    {
        _hexTileDict.TryGetValue(hexCoordinates, out var result);
        return result;
    }

    /// <summary>
    /// Gets a Tile-Coordinate and returns the surrounding tiles [neighbours] of the given Tile-Coordinate
    /// </summary>
    /// <param name="hexCoordinates"></param>
    /// <returns></returns>
    public List<Vector2Int> GetNeighboursFor(Vector2Int hexCoordinates)
    {
        //If the dictionary doesn't contains the key it will return an empty list
        if (!_hexTileDict.ContainsKey(hexCoordinates)) return new List<Vector2Int>();

        //If the neighbours does contain the key it will return the correct value
        if (_hexTileNeighboursDict.ContainsKey(hexCoordinates)) return _hexTileNeighboursDict[hexCoordinates];
   
        //If both do not work it will return a new value and an empty list
        _hexTileNeighboursDict.Add(hexCoordinates, new List<Vector2Int>());

        foreach (Vector2Int direction in Directions.directions)
        {
            if (_hexTileDict.ContainsKey(hexCoordinates +direction))
            {
                _hexTileNeighboursDict[hexCoordinates].Add(hexCoordinates +direction);
            }
        }

        return _hexTileNeighboursDict[hexCoordinates];
    }
    
    /// <summary>
    /// Parses the worldPosition of a ship to the HexCoord script and returns the closest tile.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public Vector2Int GetClosestHex(Vector3 worldPosition)
    {
        worldPosition.y = transform.position.y; 
        return _hexCoords.PosToCord(worldPosition);
    }

    /// <summary>
    /// Coordinate offsets
    /// </summary>
    private static class Directions
    {
        public static readonly List<Vector2Int> directions = new()
        {
            new Vector2Int(0, 1), //N
            new Vector2Int(1, 0), //NO
            new Vector2Int(1, -1), //ZO
            new Vector2Int(0, -1), //Z
            new Vector2Int(-1, 0), //ZW
            new Vector2Int(-1, 1), //NW
        };
    }
}
