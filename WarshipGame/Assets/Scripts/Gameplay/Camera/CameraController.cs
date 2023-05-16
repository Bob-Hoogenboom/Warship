using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cmVirtualCamera;
    [SerializeField] private Camera cam;

    [Header("Panning")]
    
    [Tooltip("The speed in which the player can drag the camera across the game scene.")]
    [SerializeField] private float panningSpeed = 10f;
    [Tooltip("The amount of Hexes in the X-axis on the map.")]
    [SerializeField] private float mapSizeX = 8f;
    [Tooltip("The amount of Hexes in the Z-axis on the map.")]
    [SerializeField] private float mapSizeZ = 8f;
    private Vector3 _cameraOrigin;
    private Vector3 _dragOrigin;
    private Vector3 _dragDirection;
    
    [Header("Zooming")]
    
    [Tooltip("The FOV of the camera will change to this value once a focus point is selected.")]
    [SerializeField] private float focusValue = 30f;
    private GameObject _focusGameObject;
    private float _focusBoundaries;
    private bool _isFocussing;
    
    [Space]
    [Tooltip("The Cameras FOV will always be in between these 2 values.")]
    [SerializeField] private Vector2 maxZoom = new(35f,60f);
    [Tooltip("The Speed in which the player can zoom the camera FOV.")]
    [SerializeField] private float zoomSpeed = 9;
    [Tooltip("The standard zoomFOV for this camera controller.")] 
    [SerializeField] private float defaultFOVValue = 45;
    private float _currentFOVValue;
    
    /// <summary>
    /// Awake first sets the size of the map by calculating mapSizeX and mapSizeY
    /// with the hexDiameter.
    /// And the Default FOV is set to the camera
    /// </summary>
    private void Awake()
    {
        mapSizeX *= 0.866f;
        mapSizeZ = mapSizeZ * 0.866f - (0.866f / 2);
        
        _currentFOVValue = defaultFOVValue;
        cmVirtualCamera.m_Lens.FieldOfView = _currentFOVValue;
    }

    private void FixedUpdate()
    {
        PanCamera();

        float localInput = Input.GetAxis("Mouse ScrollWheel");
        if (localInput != 0 && !_isFocussing)
        {
            ZoomCamera(localInput);
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
            _cameraOrigin = cmVirtualCamera.transform.position;
        }

        if (!Input.GetMouseButton(0)) return;
        if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0) return;
        
        _dragDirection = Input.mousePosition;
        Vector3 screenDelta = _dragOrigin - _dragDirection;

        Vector2 screenSize = ScaleScreenToWorldSize(cam.aspect, cam.orthographicSize, cam.scaledPixelWidth, cam.scaledPixelHeight, screenDelta.x, screenDelta.y);

        Vector3 move = new (screenSize.x + screenSize.y, 0f, screenSize.y - screenSize.x);
        move = Quaternion.Euler(0,-90,0) * move;
        Vector3 camPosMove = new (_cameraOrigin.x + move.x * Time.deltaTime * panningSpeed, _cameraOrigin.y, _cameraOrigin.z + move.z * Time.deltaTime * panningSpeed);

        if (camPosMove.z <= -mapSizeZ || camPosMove.z >= mapSizeZ || camPosMove.x <= -mapSizeX || camPosMove.x >= mapSizeX)
        {  
            return;
        }
        
        cmVirtualCamera.transform.position =  camPosMove;
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
        float screenWorldWidth = screenW * (cameraWidth / camScreenPixelWidth);
        float screenWorldHeight = screenH * (cameraHeight / camScreenPixelHeight);
        
        return new Vector2(screenWorldWidth, screenWorldHeight);
    }

    /// <summary>
    /// Zooms the camera when the scrolling axis is changed
    /// </summary>
    private void ZoomCamera(float scrollValue)
    {
        float newZoomValue = _currentFOVValue - scrollValue * zoomSpeed;

        if (newZoomValue <= maxZoom.x)
        {
            _currentFOVValue = maxZoom.x;
            cmVirtualCamera.m_Lens.FieldOfView = _currentFOVValue;
            return;
        }
        
        if (newZoomValue >= maxZoom.y)
        {
            _currentFOVValue = maxZoom.y;
            cmVirtualCamera.m_Lens.FieldOfView = _currentFOVValue;
            return;
        }
        
        _currentFOVValue = newZoomValue;
        cmVirtualCamera.m_Lens.FieldOfView = _currentFOVValue;
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
        
        
        Vector2 selectedShipPosition = new (resultPosition.x, resultPosition.z);
        Vector2 hitPosition = new (rayHitPosition.x, rayHitPosition.z);
        
        Vector2 difference = hitPosition - selectedShipPosition;

        Vector3 newPosition = new (vCamPosition.x - difference.x, vCamPosition.y, vCamPosition.z - difference.y);
        transform.position = newPosition;
        
        StartCoroutine(IsInFocus(newPosition));
    }

    /// <summary>
    /// This Coroutine makes sure that the player remains in a close-up perspective while the camera is still
    /// close to the selected ship.
    /// </summary>
    /// <param name="focusPosition"></param>
    /// <returns></returns>
    private IEnumerator IsInFocus(Vector3 focusPosition)
    {
        while (Vector3.Distance(transform.position, focusPosition) <= 1f) 
        {
            cmVirtualCamera.m_Lens.FieldOfView = focusValue;
            yield return null;
        }
    
        cmVirtualCamera.m_Lens.FieldOfView = _currentFOVValue;
        _isFocussing = false;
    }
}
