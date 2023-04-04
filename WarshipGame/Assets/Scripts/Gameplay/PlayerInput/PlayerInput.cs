using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector3> onPointerClick;

    private void Update()
    {
        OnMouseClick();
    }

    private void OnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            onPointerClick?.Invoke(mousePos);
        }
    }
}
