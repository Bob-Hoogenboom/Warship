using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera CMVirtualCamera;
    [SerializeField] private Camera Cam;

    [Header("Panning")]
    
    [Tooltip("The speed in which the player can drag the camera across the game scene.")]
    [SerializeField] private float PanningSpeed;
    [Tooltip("The amount of Hexes in the X-axis on the map.")]
    [SerializeField] private float MapSizeX;
    [Tooltip("The amount of Hexes in the Z-axis on the map.")]
    [SerializeField] private float MapSizeZ;
    private Vector3 _cameraOrigin;
    private Vector3 _dragOrigin;
    private Vector3 _dragDirection;
    
    [Header("Zooming")]
    
    [Tooltip("The FOV of the camera will change to this value once a focus point is selected.")]
    [SerializeField] private float FocusValue;
    private GameObject _focusGameObject;
    private float _focusBoundaries;
    private bool _isFocussing;
    
    [Space]
    [Tooltip("The Cameras FOV will always be in between these 2 values.")]
    [SerializeField] private Vector2 MaxZoom;
    [Tooltip("The Speed in which the player can zoom the camera FOV.")]
    [SerializeField] private float ZoomSpeed;
    [Tooltip("The standard zoomFOV for this camera controller.")]
    [SerializeField] private float DefaultFOVValue = 45;
    private float _currentFOVValue;
    
    private void Awake()
    {
        MapSizeX *= 0.866f;
        MapSizeZ = MapSizeZ * 0.866f - (0.866f / 2);
        
        _currentFOVValue = DefaultFOVValue;
        CMVirtualCamera.m_Lens.FieldOfView = _currentFOVValue;
    }

    private void FixedUpdate()
    {
        PanCamera();
        ZoomCamera();
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _dragOrigin = Input.mousePosition;
            _cameraOrigin = CMVirtualCamera.transform.position;
        }

        if (!Input.GetMouseButton(0)) return;
        if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0) return;
        
        _dragDirection = Input.mousePosition;
        Vector3 screenDelta = _dragOrigin - _dragDirection;
        
        Vector2 screenSize = ScaleScreenToWorldSize(Cam.aspect, Cam.orthographicSize, Cam.scaledPixelWidth, Cam.scaledPixelHeight, screenDelta.x, screenDelta.y);
        
        Vector3 move = new Vector3(screenSize.x + screenSize.y, 0f, screenSize.y - screenSize.x);
        Vector3 camPosMove = new Vector3(_cameraOrigin.x - move.x * Time.deltaTime * PanningSpeed, _cameraOrigin.y, _cameraOrigin.z - move.z * Time.deltaTime * PanningSpeed);
        
        
        if (camPosMove.x <= -MapSizeX || camPosMove.x >= MapSizeX)
        {
            return;
        }
        
        if (camPosMove.z <= -MapSizeZ || camPosMove.z >= MapSizeZ)
        {
            
            return;
        }
        
        CMVirtualCamera.transform.position =  camPosMove;
    }

    private Vector2 ScaleScreenToWorldSize(float camAspect, float camFOV, float camScreenPixelWidth,
        float camScreenPixelHeight, float screenW, float screenH)
    {
        float cameraWidth = camAspect * camFOV * 2f;
        float cameraHeight = camFOV * 2f;
        float screenWorldW = screenW * (cameraWidth / camScreenPixelWidth);
        float screenWorldH = screenH * (cameraHeight / camScreenPixelHeight);
        
        return new Vector2(screenWorldW, screenWorldH);
    }

    private void ZoomCamera()
    {
        if (Input.GetAxis("Mouse ScrollWheel") == 0 && _isFocussing) return;
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        float newZoomValue = _currentFOVValue -= scrollValue * ZoomSpeed;

        if (newZoomValue <= MaxZoom.x)
        {
            _currentFOVValue = MaxZoom.x;
            CMVirtualCamera.m_Lens.FieldOfView = _currentFOVValue;
            return;
        }
        
        if (newZoomValue >= MaxZoom.y)
        {
            _currentFOVValue = MaxZoom.y;
            CMVirtualCamera.m_Lens.FieldOfView = _currentFOVValue;
            return;
        }
        
        _currentFOVValue = newZoomValue;
        CMVirtualCamera.m_Lens.FieldOfView = _currentFOVValue;
    }

    /// <summary>
    /// This 
    /// </summary>
    public void Focus(GameObject result)
    {
        _isFocussing = true;
        Vector3 vCamPosition = transform.position;
        
        Physics.Raycast(vCamPosition,transform.forward,  out RaycastHit hit);

        Vector2 resultV2 = new Vector2(result.transform.position.x, result.transform.position.z);
        Vector2 hitV2 = new Vector2(hit.point.x, hit.point.z);
        
        Vector2 difference = hitV2 - resultV2;

        Vector3 newPosition = new Vector3(vCamPosition.x - difference.x, vCamPosition.y, vCamPosition.z - difference.y);
        transform.position = newPosition;
        
        StartCoroutine(IsInFocus(newPosition));
    }

    IEnumerator IsInFocus(Vector3 focusPosition)
    {
        while (Vector3.Distance(transform.position, focusPosition) <= 1f) 
        {
            CMVirtualCamera.m_Lens.FieldOfView = FocusValue;
            yield return null;
        }
    
        CMVirtualCamera.m_Lens.FieldOfView = _currentFOVValue;
        _isFocussing = false;
    }
}
