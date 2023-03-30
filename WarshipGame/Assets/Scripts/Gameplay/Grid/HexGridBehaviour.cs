using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexTiles;
using UnityEngine;

public class HexGridBehaviour : MonoBehaviour
{
    [SerializeField] private HexTileMap map;
    [SerializeField] private Material hexMat;

    [SerializeField]private GameObject gridPrefab;


    private void Awake()
    {
        map = FindObjectOfType<HexTileMap>();

        TileChecker();
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
            Instantiate(gridPrefab, new Vector3(x, y, z), Quaternion.identity);
        }
    }
    
    

    
}
