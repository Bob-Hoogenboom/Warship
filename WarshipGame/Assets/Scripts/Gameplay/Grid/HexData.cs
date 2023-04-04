using System;
using Unity.VisualScripting;
using UnityEngine;

[SelectionBase]
public class HexData : MonoBehaviour
{
    [SerializeField] private GlowManager _highlight;
    [SerializeField] private HexType hexType;
    public Vector2Int Grid;

    private void Awake()
    {
        _highlight = GetComponent<GlowManager>();
    }

    public int GetType() => hexType switch
    {
        HexType.Default => 1,
        _ => throw new Exception($"Hex of type{hexType} not supported")
    };

    public bool IsObstacle()
    {
        return this.hexType == HexType.Occupied;
    }

    public void EnableHighlight()
    {
        _highlight.ToggleGlow(true);
    }
    
    public void DisableHighlight()
    {
        _highlight.ToggleGlow(false);
    }

    public enum HexType
    {
        Default,
        Occupied
    }
    


    // [SerializeField] private LayerMask shipLayerMask;
    //
    // public int Visited = -1;
    //
    // private bool _isOccupied;
    //
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.layer != shipLayerMask)
    //     {
    //         _isOccupied = true;
    //         UpdateColor();
    //     }
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     _isOccupied = false;
    //     UpdateColor();
    // }
    //
    // private void UpdateColor()
    // {
    //     if (_isOccupied)
    //     {
    //         gameObject.GetComponent<Renderer>().material.color = Color.red;
    //         return;
    //     }
    //     gameObject.GetComponent<Renderer>().material.color = Color.blue;
    //     
    // }
}
