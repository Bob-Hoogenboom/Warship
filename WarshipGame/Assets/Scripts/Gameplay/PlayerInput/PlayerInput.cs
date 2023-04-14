using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A very simple input listener script
/// </summary>
public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector3> onPointerClick;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InputReader();
        }
    }
    
    private void InputReader()
    {
        Vector3 mousePos = Input.mousePosition;
        onPointerClick?.Invoke(mousePos);
    }
}