using System;
using System.Collections.Generic;
using System.Linq;
using HexTiles;
using UnityEngine;

/// <summary>
/// Retrieves all data and sets it for the grid
/// </summary>
public class HexCoordinates : MonoBehaviour
{
    [SerializeField] private HexTileMap map;
    [Tooltip("Waypoints for the ships get placed on Hexes with this material")]
    [SerializeField] private Material hexMat;
    [SerializeField] private GameObject gridPrefab;

    private readonly List<GameObject> _gridPrefabsList = new();
    private GameObject _instantiateObject;
    private HexTileData[] _hexTileData;
    
    /// <summary>
    /// checks if there is a HexTileMap in the scene
    /// And if there is a gridPrefab implemented
    /// </summary>
    private void Awake()
    {
        map = FindObjectOfType<HexTileMap>();

        if (gridPrefab == null)
        {
            Debug.LogException(new Exception("testObj was null"));
            //find out how logger works, https://docs.unity3d.com/ScriptReference/Logger.LogError.html
            //Logger.LogError("kTAG", "memberVariable must be set to point to a Transform.");
            return;
        }

        SetTileData();
    }

    /// <summary>
    /// Checks every position of every tile of a specific material type
    /// And places a waypoint object on every tile with that specific material
    /// </summary>
    private void SetTileData()
    {
        _hexTileData = map.Tiles.Where(data => data.Material == hexMat).ToArray();

        for(int i = 0; i < _hexTileData.Length; i++)
        {
            HexPosition hexTilePosition = _hexTileData[i].Position;
            
            float x = hexTilePosition.GetPositionVector(_hexTileData[i].Diameter).x;
            float y = hexTilePosition.Elevation;
            float z = hexTilePosition.GetPositionVector(_hexTileData[i].Diameter).z;

            _instantiateObject = Instantiate(gridPrefab, new Vector3(x, y, z), Quaternion.Euler(0,90,0));
            _instantiateObject.transform.SetParent(gameObject.transform);
            
            _gridPrefabsList.Add(_instantiateObject);
            Coordinates(i);
        }
    }

    /// <summary>
    /// Fills in the grid variable in the waypoint with coordinate data
    /// </summary>
    /// <param name="i"></param>
    private void Coordinates(int i)
    {
        HexData hexData = _instantiateObject.GetComponent<HexData>();
        
        int gridX = _hexTileData[i].Position.Coordinates.Q;
        hexData.Grid.x = gridX;
        
        int gridY = _hexTileData[i].Position.Coordinates.R;
        hexData.Grid.y = gridY;
    }

    /// <summary>
    /// Checks what waypoint is closest to the ship
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public Vector2Int PosToCord(Vector3 worldPosition)
    {
        GameObject closeObject = null;
        float minDistance = Mathf.Infinity;
        
        foreach (GameObject obj in _gridPrefabsList)
        {
            float distance = Vector3.Distance(obj.transform.position, worldPosition);
            
            if (!(distance < minDistance)) continue;
            closeObject = obj;
            minDistance = distance;
        }

        HexData hexData = closeObject.GetComponent<HexData>();
        
        int x = hexData.Grid.x;
        int z = hexData.Grid.y;
        
        return new Vector2Int( x,z);
    }
}
