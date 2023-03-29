using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] private HexGrid targetGrid;
    [SerializeField] private LayerMask terrainLayerMask;

    private Vector2 _gridPosition;
    private RaycastHit _hit;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit, float.MaxValue, terrainLayerMask))
            {
                _gridPosition = targetGrid.GetGridPosition(_hit.point);
            }
        }
    }
}
