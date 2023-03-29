using UnityEngine;

/// <summary>
/// This script generates a Set of tiles [Width X Length] and checks if the tile can be walked on. if the tile is not passable it turns red.
/// </summary>
public class HexGrid : MonoBehaviour
{
    private GridNode[,] _grid;
    
    [Header("Grid Settings")] 
    [SerializeField] private int Width = 26;
    [SerializeField] private int Length = 26;
    [SerializeField] private Vector2 CellSize = new Vector2(.75f, .865f);
    [SerializeField] private Vector3 CheckBoxSize = new Vector3 (0.3f, 2f, 0.3f);
    [SerializeField] private LayerMask ObstacleMak;
    
    //Calculation Variable
    private bool _hasOffset;
    private float _positionOffset;

    private void Start()
    {
        GenerateGrid();
    }

    //instantiates a new GridNode before calculating the grid 
    private void GenerateGrid()
    {
        _grid = new GridNode[Length, Width];
        CheckPossibleTerrain();
    }

    //This script calculates the center position of every tile and checks every tile for an obstacle layer
    private void CheckPossibleTerrain()
    {
        _hasOffset = true;
        
        for (int x = 0; x < Width; x++)
        {
            _hasOffset = !_hasOffset;
            
            for (int y = 0; y < Length; y++)
            {
                Vector3 WorldPosition = GetWorldPosition(x, y);
                bool Passable = Physics.CheckBox(WorldPosition,  CheckBoxSize , Quaternion.identity, ObstacleMak); 
                _grid[x, y] = new GridNode();
                _grid[x, y].Passable = Passable;
            }
        }
    }
    
    //draws the position of every checkbox cube
    private void OnDrawGizmos()
    {
        if (_grid == null) { return; }

        for (int x = 0; x < Width; x++)
        {
            _hasOffset = !_hasOffset;

            for (int y = 0; y < Length; y++)
            {
                Vector3 pos = GetWorldPosition(x, y);
                Gizmos.color = _grid[x, y].Passable ? Color.red : Color.white;
                Gizmos.DrawWireCube(pos, CheckBoxSize);
            }
        }
    }

    //Calculates the world position of every tile and returns it
    private Vector3 GetWorldPosition(int x, int y)
    {
        _positionOffset = _hasOffset ? CellSize.y / 2 : 0f;
        return new Vector3(transform.position.x + (x * CellSize.x), 0f, transform.position.z + (y * CellSize.y - _positionOffset));
    }
}
