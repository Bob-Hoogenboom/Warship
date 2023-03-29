using UnityEngine;

/// <summary>
/// This script generates a Set of tiles [Width X Length] and checks if the tile can be walked on. if the tile is not passable it turns red.
/// </summary>
public class HexGrid : MonoBehaviour
{
    private GridNode[,] _grid;
    
    [Header("Grid Settings")] 
    [SerializeField] private int width = 26;
    [SerializeField] private int length = 26;
    [SerializeField] private Vector2 cellSize = new (.75f, .865f);
    [SerializeField] private Vector3 checkBoxSize = new (0.3f, 2f, 0.3f);
    [SerializeField] private LayerMask obstacleMask;
    
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
        _grid = new GridNode[length, width];
        CheckPossibleTerrain();
    }

    //This script calculates the center position of every tile and checks every tile for an obstacle layer
    private void CheckPossibleTerrain()
    {
        _hasOffset = true;
        
        for (int x = 0; x < width; x++)
        {
            _hasOffset = !_hasOffset;
            
            for (int y = 0; y < length; y++)
            {
                Vector3 worldPosition = GetWorldPosition(x, y);
                bool passable = !Physics.CheckBox(worldPosition,  checkBoxSize , Quaternion.identity, obstacleMask); 
                _grid[x, y] = new GridNode{Passable = passable};
            }
        }
    }

    //needs optimization i know*
    public Vector2 GetGridPosition(Vector3 worldPosition)
    {
        worldPosition -= transform.position;
        Vector2 positionOnGrid = new Vector2(worldPosition.x / cellSize.x, worldPosition.z / cellSize.y);
        return positionOnGrid;
    }
    
    //draws the position of every checkbox cube
    private void OnDrawGizmos()
    {
        if (_grid == null)  return; 

        for (int x = 0; x < width; x++)
        {
            _hasOffset = !_hasOffset;

            for (int y = 0; y < length; y++)
            {
                Gizmos.color = _grid[x, y].Passable ? Color.white : Color.red;
                Gizmos.DrawWireCube(GetWorldPosition(x,y), checkBoxSize);
            }
        }
    }

    //Calculates the world position of every tile and returns it
    private Vector3 GetWorldPosition(int x, int y)
    {
        Vector3 gridTransform = transform.position;
        _positionOffset = _hasOffset ? cellSize.y / 2 : 0f;
        return new Vector3(gridTransform.x + (x * cellSize.x), 0f, gridTransform.z + (y * cellSize.y - _positionOffset));
    }
}
