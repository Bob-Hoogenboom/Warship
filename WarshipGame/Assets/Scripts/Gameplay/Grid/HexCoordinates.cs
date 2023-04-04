using System.Linq;
using HexTiles;
using UnityEngine;

public class HexCoordinates : MonoBehaviour
{
    [SerializeField] private HexTileMap map;
    [SerializeField] private Material hexMat;

    [SerializeField] private GameObject gridPrefab;
    private GameObject _instantiateObject;

    private HexTileData[] _hexTileDatas;
    
    private void Awake()
    {
        map = FindObjectOfType<HexTileMap>();

        if (gridPrefab) SetTileData();
        else print("Missing prefab, please assign");
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
        
            _instantiateObject = Instantiate(gridPrefab, new Vector3(x, y, z), Quaternion.identity);
            _instantiateObject.transform.SetParent(gameObject.transform);
        
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
}
