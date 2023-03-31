using System;
using UnityEngine;

public class TileData : MonoBehaviour
{
    [SerializeField] private LayerMask shipLayerMask;

    public int Visited = -1;
    public int GridX;
    public int GridY;
    
    private bool _isOccupied;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != shipLayerMask)
        {
            _isOccupied = true;
            UpdateColor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isOccupied = false;
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (_isOccupied)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            return;
        }
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
        
    }
}
