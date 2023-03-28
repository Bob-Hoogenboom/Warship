using System;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [Header("GridStats")]
    private GridNode[,] _grid;
    [SerializeField] private int Width = 25;
    [SerializeField] private int Length = 25;
    [SerializeField] private Vector2 CellSize = new Vector2(.75f, .865f);
    [SerializeField] private Vector2 DetectionSize;
    [SerializeField] private LayerMask ObstacleMak;
    
    private bool Switch = true;
    private float PositionOffset;

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
                Vector3 WorldPosition = GetWorldPosition(x, y);
                bool Passable = Physics.CheckBox(WorldPosition,  new Vector3(DetectionSize.x/2,0f,DetectionSize.y/2) * DetectionSize  , Quaternion.identity, ObstacleMak); 
                _grid[x, y] = new GridNode();
                _grid[x, y].Passable = Passable;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_grid == null) { return; }
    
        Vector3 GizmoSize = new Vector3(DetectionSize.x, 0f, DetectionSize.y);
        Switch = true;
        
        for (int x = 0; x < Width; x++)
        {
            Switch = !Switch;
            
            for (int y = 0; y < Length; y++)
            {
                Vector3 pos = GetWorldPosition(x, y);
                Gizmos.color = _grid[x, y].Passable ? Color.red : Color.white;
                Gizmos.DrawWireCube(pos, GizmoSize);
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        PositionOffset = Switch ? CellSize.y / 2 : 0f;
        return new Vector3(transform.position.x + (x * CellSize.x), 0f, transform.position.z + (y * CellSize.y - PositionOffset));
    }
}
