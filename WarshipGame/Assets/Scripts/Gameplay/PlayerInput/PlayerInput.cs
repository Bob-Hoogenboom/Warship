using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector3> onPointerClick;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        OnMouseClick();
    }

    private void OnMouseClick()
    {
        Vector3 mousePos = Input.mousePosition;
        onPointerClick?.Invoke(mousePos);
    }
}
