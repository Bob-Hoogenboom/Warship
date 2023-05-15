using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera CMVirtualCamera;
    [SerializeField] private Camera Cam;

    [Header("Panning")]
    
    [Tooltip("The speed in which the player can drag the camera across the game scene.")]
    [SerializeField] private float PanningSpeed = 10f;
    [Tooltip("The amount of Hexes in the X-axis on the map.")]
    [SerializeField] private float MapSizeX = 8f;
    [Tooltip("The amount of Hexes in the Z-axis on the map.")]
    [SerializeField] private float MapSizeZ = 8f;
    private Vector3 _cameraOrigin;
    private Vector3 _dragOrigin;
    private Vector3 _dragDirection;
    
    [Header("Zooming")]
    
    [Tooltip("The FOV of the camera will change to this value once a focus point is selected.")]
    [SerializeField] private float FocusValue = 30f;
    private GameObject _focusGameObject;
    private float _focusBoundaries;
    private bool _isFocussing;
    
    [Space]
    [Tooltip("The Cameras FOV will always be in between these 2 values.")]
    [SerializeField] private Vector2 MaxZoom = new(35f,60f);
    [Tooltip("The Speed in which the player can zoom the camera FOV.")]
    [SerializeField] private float ZoomSpeed = 9;
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

        if (Input.GetAxis("Mouse ScrollWheel") != 0 && !_isFocussing)
        {
            ZoomCamera();
        }
    }

    /// <summary>
    /// This function makes it possible to drag the camera across the
    /// play field between a certain boundary
    /// </summary>
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
        move = Quaternion.Euler(0,-90,0) * move;
        Vector3 camPosMove = new Vector3(_cameraOrigin.x + move.x * Time.deltaTime * PanningSpeed, _cameraOrigin.y, _cameraOrigin.z + move.z * Time.deltaTime * PanningSpeed);

        if (camPosMove.z <= -MapSizeZ || camPosMove.z >= MapSizeZ || camPosMove.x <= -MapSizeX || camPosMove.x >= MapSizeX)
        {  
            return;
        }
        
        CMVirtualCamera.transform.position =  camPosMove;
    }

    /// <summary>
    /// Corrects the screen panning by correcting the screen size and FOV distance
    /// relative to the camera width and height
    /// </summary>
    /// <returns></returns>
    private Vector2 ScaleScreenToWorldSize(float camAspect, float camFOV, float camScreenPixelWidth,
        float camScreenPixelHeight, float screenW, float screenH)
    {
        float cameraWidth = camAspect * camFOV * 2f;
        float cameraHeight = camFOV * 2f;
        float screenWorldW = screenW * (cameraWidth / camScreenPixelWidth);
        float screenWorldH = screenH * (cameraHeight / camScreenPixelHeight);
        
        return new Vector2(screenWorldW, screenWorldH);
    }

    /// <summary>
    /// Zooms the camera when the scrolling axis is changed
    /// </summary>
    private void ZoomCamera()
    {
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
    /// A function that should be added on the OnShipSelected Unity Event to invoke a focus
    /// This Function moves to a selected ship and changes to a close-up FOV
    /// </summary>
    public void Focus(GameObject result)
    {
        _isFocussing = true;
        Vector3 vCamPosition = transform.position;
        
        Physics.Raycast(vCamPosition,transform.forward,  out RaycastHit hit);
        
        Vector3 resultPosition = result.transform.position;
        Vector3 rayHitPosition = hit.point;
        
        
        Vector2 selectedShipPosition = new Vector2(resultPosition.x, resultPosition.z);
        Vector2 hitPosition = new Vector2(rayHitPosition.x, rayHitPosition.z);
        
        Vector2 difference = hitPosition - selectedShipPosition;

        Vector3 newPosition = new Vector3(vCamPosition.x - difference.x, vCamPosition.y, vCamPosition.z - difference.y);
        transform.position = newPosition;
        
        StartCoroutine(IsInFocus(newPosition));
    }

    /// <summary>
    /// This Coroutine makes sure that the player remains in a close-up perspective while the camera is still
    /// close to the selected ship.
    /// </summary>
    /// <param name="focusPosition"></param>
    /// <returns></returns>
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
