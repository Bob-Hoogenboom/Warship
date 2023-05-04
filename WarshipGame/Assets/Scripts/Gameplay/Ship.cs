using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds data specifically for the ship like movement speed and the range it can move
/// Also activates shaders on selection
/// </summary>
[SelectionBase]
public class Ship : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("The time it takes to rotate the ship when it needs tot turn to a different tile")]
    public float RotationTime = 0.3f;
    
    [Tooltip("The time it takes the ship to move from tile to tile")]
    public float MovementTime = 1f;
    
    [Tooltip("The points are the cost of the range. For example if 3 points are set then the ship can move 3 tiles in range")]
    [SerializeField] private int points = 3;
    
    [Header("Health")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private Slider healthBar;
    
    public int Damage => damage;
    public Slider HealthBar => healthBar;
    
    private Queue<Vector3> _pathPositions = new();
    private GlowManager _glowManager;
    
    public int MovementPoints => points;
    public event Action<Ship> MovementFinished;
    public bool shipMoved;
    public Sprite profileTag;
    
    
    /// <summary>
    /// Gets glow manager to prevent Editor usage
    /// </summary>
    private void Awake()
    {
        _glowManager = GetComponent<GlowManager>();
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void TakeDamage(int damageTaken)
    {
        healthBar.value -= damageTaken;
        
        if (healthBar.value > 0) return; 
        gameObject.SetActive(false);
    }

    internal void Deselect()
    {
        _glowManager.ToggleGlow(false);
    }
    
    internal void Select()
    {
        _glowManager.ToggleGlow();
    }

    /// <summary>
    /// Cycles trough the current path queue and removes the tiles it passed out of the queue
    /// </summary>
    /// <param name="currentPath"></param>
    internal void MoveTroughPath(List<Vector3> currentPath)
    {
        _pathPositions = new Queue<Vector3>(currentPath);
        Vector3 firstTarget = _pathPositions.Dequeue();
        StartCoroutine(RotationCoroutine(firstTarget, RotationTime));
    }

    /// <summary>
    /// Rotates from the start location to the end location in a set time
    /// </summary>
    /// <param name="endPosition"></param>
    /// <param name="rotationTime"></param>
    /// <param name="firstRotation"></param>
    /// <returns></returns>
    private IEnumerator RotationCoroutine(Vector3 endPosition, float rotationTime)
    {
        Vector3 currentTransform = transform.position;
        Quaternion startRotation = transform.rotation;
        
        endPosition.y = currentTransform.y;
        Vector3 direction = endPosition - currentTransform;
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

    /// <summary>
    /// Lerps the player from the start position to the end position in a set Time
    /// And checks if the Ship reached its end goal.
    /// </summary>
    /// <param name="endPosition"></param>
    /// <returns></returns>
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
            StartCoroutine(RotationCoroutine(_pathPositions.Dequeue(), RotationTime));
        }
        else
        {
            MovementFinished?.Invoke(this);
        }
    }
}
