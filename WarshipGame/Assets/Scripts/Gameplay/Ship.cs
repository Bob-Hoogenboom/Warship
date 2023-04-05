using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[SelectionBase]
public class Ship : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float RotationTime = 0.3f;
    [SerializeField] private float MovementTime = 1f;
    [SerializeField] private int Points = 3;
    
    private Queue<Vector3> _pathPositions = new Queue<Vector3>();
    private GlowManager _glowManager;
    
    public int MovementPoints => Points;
    public event Action<Ship> MovementFinished;

    private void Awake()
    {
        _glowManager = GetComponent<GlowManager>();
    }

    internal void Deselect()
    {
        _glowManager.ToggleGlow(false);
    }
    
    internal void Select()
    {
        _glowManager.ToggleGlow();
    }

    internal void MoveTroughPath(List<Vector3> currentPath)
    {
        _pathPositions = new Queue<Vector3>(currentPath);
        Vector3 firstTarget = _pathPositions.Dequeue();
        StartCoroutine(RotationCoroutine(firstTarget, RotationTime, true));
    }

    private IEnumerator RotationCoroutine(Vector3 endPosition, float rotationTime, bool firstRotation = false)
    {
        Quaternion startRotation = transform.rotation;
        endPosition.y = transform.position.y;
        Vector3 direction = endPosition - transform.position;
        Quaternion endRotation = Quaternion.LookRotation(direction, Vector3.up);

        if (!Mathf.Approximately(Mathf.Abs(Quaternion.Dot(startRotation, endRotation)),1.0f))
        {
            float timeElapsed = 0;
            while (timeElapsed < rotationTime)
            {
                timeElapsed += Time.deltaTime;
                float lerpStep = timeElapsed / rotationTime;
                transform.rotation = Quaternion.Lerp(startRotation,endRotation, lerpStep);
                yield return null;
            }

            transform.rotation = endRotation;
        }

        StartCoroutine(MovementCoroutine(endPosition));
    }

    
    private IEnumerator MovementCoroutine(Vector3 endPosition)
    {
        Vector3 startPosition = transform.position;
        endPosition.y = startPosition.y;
        float timeElapsed = 0;
        
        while(timeElapsed < MovementTime)
        {
            timeElapsed += Time.deltaTime;
            float lerpStep = timeElapsed / MovementTime;
            transform.position = Vector3.Lerp(startPosition,endPosition, lerpStep);
            yield return null;
        }

        transform.position = endPosition;

        if (_pathPositions.Count > 0)
        {   
            Debug.Log("Selecting the next position");
            StartCoroutine(RotationCoroutine(_pathPositions.Dequeue(), RotationTime));
        }
        else
        {
            Debug.Log("Movement Finished!");
            MovementFinished?.Invoke(this);
        }
    }
}
