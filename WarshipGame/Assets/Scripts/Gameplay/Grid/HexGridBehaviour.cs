using System.Linq;
using HexTiles;
using UnityEngine;
using UnityEngine.WSA;

public class HexGridBehaviour : MonoBehaviour
{
    [SerializeField] private Transform tilePointParent;
    [SerializeField] private HexTileMap map;
    [SerializeField] private Material hexMat;

    [SerializeField]private GameObject gridPrefab;

    private Vector2Int _start = new(0, 0);
    private Vector2Int _end = new(4, 4);

    private GameObject[,] _gridArray;
    

    private void Awake()
    {
        map = FindObjectOfType<HexTileMap>();

        if (gridPrefab) TileChecker();
        else print("Missing prefab, please assign");
    }

    //Checks every tile and gets every tiles transform and instantiates a prefab onto that position
    private void TileChecker()
    {
        var arr = map.Tiles.Where(data => data.Material == hexMat).ToArray();

        for (int i = 0; i < arr.Length; i++)
        {
            var x = arr[i].Position.GetPositionVector(arr[i].Diameter).x;
            var y = arr[i].Position.Elevation;
            var z = arr[i].Position.GetPositionVector(arr[i].Diameter).z;
            
            GameObject obj = Instantiate(gridPrefab, new Vector3(x, y, z), Quaternion.identity);
            obj.transform.SetParent(tilePointParent);
            
            int gridX = arr[i].Position.Coordinates.Q;
            obj.GetComponent<TileData>().GridX = gridX;

            int gridY = arr[i].Position.Coordinates.R;
            obj.GetComponent<TileData>().GridY = gridY;

            _gridArray[gridX, gridY] = obj;
        }
    }

    //bad name*
    private void InitialSetUp()
    {
        foreach (GameObject obj in _gridArray)
        {
            obj.GetComponent<TileData>().Visited = -1;
        }
        _gridArray[_start.x, _start.y].GetComponent<TileData>().Visited = 0;
    }

    // private bool TestDrirection(int x, int y, int step, int direction)
    // {
    //     switch (direction)
    //     {
    //         case 1:
    //             if (y +1 <  )
    //             {
    //                 
    //             }
    //         
    //     }
    // }
    
    

    
}
