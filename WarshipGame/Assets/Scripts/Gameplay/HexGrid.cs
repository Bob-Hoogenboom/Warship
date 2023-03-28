using System;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    private GridNode[,] _grid;
    [SerializeField] private int Width = 25;
    [SerializeField] private int Length = 25;
    [SerializeField] private Vector2 CellSize = new Vector2(1f, .5f); 

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _grid = new GridNode[Length, Width];
        CheckPossibleTerrain();
    }

    private void CheckPossibleTerrain()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Length; y++)
            {
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 GizmoSize = new Vector3(CellSize.x, 0f, CellSize.y);
        bool Switch = true;
        float modifier;

        for (int x = 0; x < Width; x++)
        {
            Switch = !Switch;
            for (int y = 0; y < Length; y++)
            {
                modifier = Switch ? CellSize.y / 2 : 0f;
                Vector3 pos = new Vector3(transform.position.x + (x * CellSize.x), 0f, transform.position.z + (y * CellSize.y - modifier));
                Gizmos.DrawWireCube(pos, GizmoSize);
            }
        }
    }
}
