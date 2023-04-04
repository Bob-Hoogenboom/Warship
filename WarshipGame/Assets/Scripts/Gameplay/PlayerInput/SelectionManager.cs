using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    
    public LayerMask SelectionMask;
    public HexGrid HexGridScript;
    
    private RaycastHit _hit;
    private List<Vector2Int> _varNeighbours = new List<Vector2Int>();
    
    private void Awake()
    {
        if (mainCamera == null) mainCamera = Camera.main;
    }

    public void HandleClick(Vector3 mousePosition)
    {
        GameObject result; 
        if (FindTarget(mousePosition, out result))
        {
            HexData selectedHex = result.GetComponent<HexData>();

            selectedHex.DisableHighlight();
            foreach (Vector2Int neighbours in _varNeighbours)
            {
                HexGridScript.GetTileAt(neighbours). DisableHighlight();
            }
            _varNeighbours = HexGridScript.GetNeighboursFor(selectedHex.Grid);
            
            foreach (Vector2Int neighbours in _varNeighbours)
            {
                HexGridScript.GetTileAt(neighbours). EnableHighlight();
            }
            
            Debug.Log($"neighbours for {selectedHex.Grid} are:");
            foreach (Vector2Int neighbourPosition in _varNeighbours) 
            {
                Debug.Log(neighbourPosition);
            }
        }

    }

    //find closest point? Insteasd of hit collider?
    private bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out _hit, SelectionMask))
        {
            result = _hit.collider.gameObject;
            return true;
        }

        result = null;
        return false;
    }
}
