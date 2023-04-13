using UnityEngine;

// This updated a gameObject rotation to always look at the camera
public class BillBoard : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        gameObject.transform.LookAt(gameObject.transform.position + _camera.transform.forward);
    }
}
