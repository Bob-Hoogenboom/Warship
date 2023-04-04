using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector3> onPointerClick;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClick();
        }
    }

    private void OnMouseClick()
    {
        if (onPointerClick == null) return;
        
        Vector3 mousePos = Input.mousePosition;
        onPointerClick?.Invoke(mousePos);
    }
}
