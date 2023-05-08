using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera CMVirtualCamera;
    [SerializeField] private Camera Cam;

    [Header("Panning")]
    [SerializeField] private Vector2 MapSize;
    private Vector3 _dragOrigin;
    
    [Header("Zooming")]
    [SerializeField] private float FocusValue;
    [SerializeField] private float ZoomValue;
    [SerializeField] private float ZoomSpeed;

    private GameObject _focusGameObject;
    private float _focusBoundaries;
    
    private void Update()
    {
        PanCamera();
        ZoomCamera();
    }

    private void PanCamera()
    {
        //also add WASD movement in the future*

        if (Input.GetMouseButtonDown(0))
        {
            _dragOrigin = Cam.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(Input.mousePosition);
            //var testVec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Cam.nearClipPlane);
            //Debug.Log(testVec);
            print(_dragOrigin);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = _dragOrigin - Cam.ScreenToWorldPoint(Input.mousePosition);

            CMVirtualCamera.transform.position += difference;
        }
    }

    private void ZoomCamera()
    {
        if (Input.mouseScrollDelta.y == 0) return;
        
        ZoomValue -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        CMVirtualCamera.m_Lens.FieldOfView = ZoomValue;
    }

    /// <summary>
    /// This 
    /// </summary>
    public void Focus(GameObject result)
    {
        Ray ray = Cam.ScreenPointToRay(Vector3.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        
        var difference = hit.point - result.transform.position;
        transform.position += difference;
        var currentPosition = transform.position;

        while (transform.position != currentPosition *1 )
        {
            CMVirtualCamera.m_Lens.FieldOfView = FocusValue;
        }
        
        CMVirtualCamera.m_Lens.FieldOfView = ZoomValue;
    }
}
