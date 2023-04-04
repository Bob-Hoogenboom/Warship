using UnityEngine;

public class HexData : MonoBehaviour
{
    public Vector2Int Grid;





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
