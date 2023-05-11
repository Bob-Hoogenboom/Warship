using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera CMVirtualCamera;
    [SerializeField] private Camera Cam;     
    [SerializeField]private Transform _dollyTransform;

    [Header("Panning")]
    [SerializeField] private Vector2 MapSize;
    [SerializeField] private float panningSpeed;
    private Vector3 _cameraOrigin;
    private Vector3 _dragOrigin;
    private Vector3 _dragDirection;
    
    
    [Header("Zooming")]
    [Tooltip("The FOV of the camera will change to this value once a focus point is selected.")]
    [SerializeField] private float FocusValue;
    
    [SerializeField] private Vector2 maxZoom;
    [SerializeField] private float ZoomSpeed;
    [SerializeField] private float DefaultFOVValue = 40;
    private float _currentFOVValue;

    private GameObject _focusGameObject;
    private float _focusBoundaries;

    private void Awake()
    {
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
        //also add WASD movement in the future*

        if (Input.GetMouseButtonDown(0))
        {
            _dragOrigin = Input.mousePosition;
            _cameraOrigin = CMVirtualCamera.transform.position;
        }

        if (!Input.GetMouseButton(0)) return;
        if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0) return;
        
        _dragDirection = Input.mousePosition;
        Vector3 screenDelta = _dragOrigin - _dragDirection;
        
        //change Cam for cinemachine variables?
        Vector2 screenSize = ScaleScreenToWorldSize(Cam.aspect, Cam.orthographicSize, Cam.scaledPixelWidth, Cam.scaledPixelHeight, screenDelta.x, screenDelta.y);
        
        Vector3 move = new Vector3(screenSize.x + screenSize.y, 0f, screenSize.y - screenSize.x);
        Vector3 camPosMove = new Vector3(_cameraOrigin.x - move.x * Time.deltaTime * panningSpeed, _cameraOrigin.y, _cameraOrigin.z - move.z * Time.deltaTime * panningSpeed);
        
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
        if (Input.GetAxis("Mouse ScrollWheel") == 0) return;
        var scrollValue = Input.GetAxis("Mouse ScrollWheel");
        var newZoomValue = _currentFOVValue -= scrollValue * ZoomSpeed;

        if (newZoomValue <= maxZoom.x)
        {
            _currentFOVValue = maxZoom.x;
            CMVirtualCamera.m_Lens.FieldOfView = _currentFOVValue;
            return;
        }
        
        if (newZoomValue >= maxZoom.y)
        {
            _currentFOVValue = maxZoom.y;
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
        var vCamPosition = transform.position;
        
        //get the hit.point
        //Ray ray = transform.position.Vector3.forward);
        Physics.Raycast(vCamPosition,transform.forward,  out RaycastHit hit);

        //get the camera position

        Vector2 resultV2 = new Vector2(result.transform.position.x, result.transform.position.z);
        Vector2 hitV2 = new Vector2(hit.point.x, hit.point.z);
        
        Vector2 difference = hitV2 - resultV2;
        
        print(hitV2 + " - " + resultV2 + " = " + difference);
        
        var newPosition = new Vector3(vCamPosition.x - difference.x, vCamPosition.y, vCamPosition.z - difference.y);
            
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
    }
}
