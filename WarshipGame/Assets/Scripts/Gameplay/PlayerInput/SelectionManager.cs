using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    
    public LayerMask SelectionMask;
    //public HexGrid HexGridScript;
    
    private RaycastHit _hit;
    //private List<Vector2Int> _varNeighbours = new List<Vector2Int>();

    public UnityEvent<GameObject> OnShipSelected;
    public UnityEvent<GameObject> OnTerrainSelected;

    private void Awake()
    {
        if (mainCamera == null) mainCamera = Camera.main;
    }

    public void HandleClick(Vector3 mousePosition)
    {
        if (FindTarget(mousePosition, out var result))
        {
            if (ShipSelected(result))
            {
                OnShipSelected?.Invoke(result);
            }
            else
            {
                OnTerrainSelected.Invoke(result);
            }
        }
        
    }

    private bool ShipSelected(GameObject result)
    {
        return result.GetComponent<Ship>() != null;
    }

    //find closest point? Insteasd of hit collider?
    private bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out _hit, 100,SelectionMask))
        {
            result = _hit.collider.gameObject;
            return true;
        }

        result = null;
        return false;
    }
}

// HexData selectedHex = result.GetComponent<HexData>();
//
// selectedHex.DisableHighlight();
// foreach (Vector2Int neighbours in _varNeighbours)
// {
//     HexGridScript.GetTileAt(neighbours). DisableHighlight();
// }
// //_varNeighbours = HexGridScript.GetNeighboursFor(selectedHex.Grid);
//
// BFSResult bfsResult = GraphSearch.BFSGetRange(HexGridScript, selectedHex.Grid, 3);
// _varNeighbours = new List<Vector2Int>(bfsResult.GetRangePositions());
//             
// foreach (Vector2Int neighbours in _varNeighbours)
// {
//     HexGridScript.GetTileAt(neighbours). EnableHighlight();
// }
//             
// Debug.Log($"neighbours for {selectedHex.Grid} are:");
// foreach (Vector2Int neighbourPosition in _varNeighbours) 
// {
//     Debug.Log(neighbourPosition);
// }
