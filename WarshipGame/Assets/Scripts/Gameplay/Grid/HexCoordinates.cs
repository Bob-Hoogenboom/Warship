using System;
using System.Collections.Generic;
using System.Linq;
using HexTiles;
using Unity.VisualScripting;
using UnityEngine;

public class HexCoordinates : MonoBehaviour
{
    [SerializeField] private HexTileMap map;
    [SerializeField] private Material hexMat;

    [SerializeField] private GameObject gridPrefab;
    private GameObject _instantiateObject;
    
    //~~~~~~~~~~
    private static HexPosition hexPos;

    private static float hexTileSize;
    

    private List<GameObject> _gridPrefabsList = new List<GameObject>();
    private GameObject _closestGameObject;
    //~~~~~~~~~~
    
    private HexTileData[] _hexTileDatas;
    
    private void Awake()
    {
        map = FindObjectOfType<HexTileMap>();

        if (gridPrefab) SetTileData();
        else print("Missing prefab, please assign");

        hexTileSize = map.tileDiameter;
    }

    //Checks every tile and gets every tiles transform and instantiates a prefab onto that position
    private void SetTileData()
    {
        _hexTileDatas = map.Tiles.Where(data => data.Material == hexMat).ToArray();

        for(int i = 0; i < _hexTileDatas.Length; i++)
        {
            float x = _hexTileDatas[i].Position.GetPositionVector(_hexTileDatas[i].Diameter).x;
            float y = _hexTileDatas[i].Position.Elevation;
            float z = _hexTileDatas[i].Position.GetPositionVector(_hexTileDatas[i].Diameter).z;

            _instantiateObject = Instantiate(gridPrefab, new Vector3(x, y, z), Quaternion.Euler(0,90,0));
            _instantiateObject.transform.SetParent(gameObject.transform);
            
            _gridPrefabsList.Add(_instantiateObject);
            Coordinates(i);
        }
    }

    private void Coordinates(int i)  
    {
        int gridX = _hexTileDatas[i].Position.Coordinates.Q;
        _instantiateObject.GetComponent<HexData>().Grid.x = gridX;
        
        int gridY = _hexTileDatas[i].Position.Coordinates.R;
        _instantiateObject.GetComponent<HexData>().Grid.y = gridY;
    }

    
    //not entirely the correct calculation
    public Vector2Int PosToCord(Vector3 worldPosition)
    {
        GameObject tMin = null;
        float minDistance = Mathf.Infinity;
        for (int i = 0; i < _gridPrefabsList.Count; i++)
        {
            float distance = Vector3.Distance(_gridPrefabsList[i].transform.position, worldPosition);
            if (distance < minDistance)
            {
                tMin = _gridPrefabsList[i];
                minDistance = distance;
            }
        }
        _closestGameObject = tMin;
        
        HexData hexData = _closestGameObject.GetComponent<HexData>();
        
        int x = hexData.Grid.x;
        int z = hexData.Grid.y;
        
        Debug.Log("int X= " + x + " & int Z = " + z);
        Debug.Log(worldPosition.x + " " + worldPosition.z);
        return new Vector2Int( x,z);
    }
}
