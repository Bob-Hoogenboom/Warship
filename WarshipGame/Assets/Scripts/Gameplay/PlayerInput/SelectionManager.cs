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

            List<Vector2Int> neighbours = HexGridScript.GetNeighboursFor(selectedHex.Grid);
            Debug.Log($"neighbours for {selectedHex.Grid} are:");
            foreach (Vector2Int neighbourPosition in neighbours) 
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
