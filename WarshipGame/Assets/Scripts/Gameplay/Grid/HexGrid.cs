using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    private Dictionary<Vector2Int, Hex> _hexTileDict = new Dictionary<Vector2Int, Hex>();

    private Dictionary<Vector2Int, List<Vector2Int>> _hexTileNeighboursDict =
        new Dictionary<Vector2Int, List<Vector2Int>>();

    private void Start()
    {
        //fills dictionary with hextiles
        foreach (Hex hex in FindObjectsOfType<Hex>())
        {
            _hexTileDict[hex.Grid] = hex;
        }

        List<Vector2Int> neighbours = GetNeighboursFor(new Vector2Int(0, 0));
        Debug.Log("neighbours for(0,0) are:");
        foreach (Vector2Int neighbourPosition in neighbours)
        {
            Debug.Log(neighbourPosition);
        }
    }

    public Hex GetTileAt(Vector2Int hexCoordinates)
    {
        Hex result = null;
        _hexTileDict.TryGetValue(hexCoordinates, out result);
        return result;
    }

    public List<Vector2Int> GetNeighboursFor(Vector2Int hexCoordinates)
    {
        //if the dictionary doesn't contains the key it will return an empty list
        if (!_hexTileDict.ContainsKey(hexCoordinates)) return new List<Vector2Int>();

        //if the neighbours does contain the key it will return the correct value
        if (_hexTileNeighboursDict.ContainsKey(hexCoordinates)) return _hexTileNeighboursDict[hexCoordinates];
   
        //if both do not work it will return a new value and an empty list
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


    public static class Directions
    {
        public static List<Vector2Int> directions = new List<Vector2Int>()
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
