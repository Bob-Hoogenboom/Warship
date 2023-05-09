using System;
using System.Collections;
using UnityEngine;

public enum States {
    Move,
    Attack,
    Skip,
}

public class Enemy : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private LayerMask PlayerShips;
    
    [Tooltip("works best for 1,2,3 radial values >3 becomes inaccurate")] 
    [Range(1,3)] 
    [SerializeField] private int Radius;
    
    [SerializeField] private bool GizmosOn;
    private States _states;

    //attack
    private Ship _shipScript;
    private Transform _targetShip;
    
    [Header("Movement")]
    [SerializeField] private Waypoint WaypointsScript;
    
    private Transform _currentWaypoint;
    private float _rotationTime;
    private float _movementTime;


    private void Start()
    {
        _shipScript = gameObject.GetComponent<Ship>();

        _movementTime = _shipScript.MovementTime;
        _rotationTime = _shipScript.RotationTime;
        
        if (WaypointsScript == null) return;
        _currentWaypoint = WaypointsScript.GetNextWaypoint(_currentWaypoint);
        transform.position = _currentWaypoint.position;
        
        _currentWaypoint = WaypointsScript.GetNextWaypoint(_currentWaypoint);
    }

    /// <summary>
    /// It checks the action the enemy ship is able to perform and switches to that state
    /// </summary>
    public void StateCheck()
    {
        //OverlapSphere returns an array of every Collider of collision layer 'PlayerShips'
        Collider[] targetColliders = Physics.OverlapSphere(transform.position, (Radius * 0.866f), PlayerShips);
        if (targetColliders.Length == 0)
        {
            _states = WaypointsScript == null ? States.Skip : States.Move;

            EnemyAction();
            return;
        }

        _targetShip = targetColliders[0].GetComponentInParent<Transform>(); //gets first in the Collider[] to attack it

        _states = States.Attack;
        EnemyAction();
    }

    /// <summary>
    /// State handler inside of a switch case
    /// </summary>
    private void EnemyAction()
    {
        switch (_states)
        {
            case States.Move:
                StartCoroutine(RotateTowards());
                break;
            
            case States.Attack:
                AttackState();
                break;
            
            case States.Skip:
                break; //skip turn aka do nothing
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    /// <summary>
    /// Rotates towards the next waypoint
    /// </summary>
    private IEnumerator RotateTowards()
    {
        Vector3 endPosition = _currentWaypoint.position;
        Vector3 currentTransform = transform.position;
        Quaternion startRotation = transform.rotation;
        
        endPosition.y = currentTransform.y;
        Vector3 direction = endPosition - currentTransform;
        Quaternion endRotation = Quaternion.LookRotation(direction, Vector3.up);

        if (!Mathf.Approximately(Mathf.Abs(Quaternion.Dot(startRotation, endRotation)),1.0f))
        {
            float timeElapsed = 0;
            
            while (timeElapsed < _rotationTime)
            {
                timeElapsed += Time.deltaTime;
                float lerpStep = timeElapsed / _rotationTime;
                transform.rotation = Quaternion.Lerp(startRotation,endRotation, lerpStep);
                yield return null;
            }

            transform.rotation = endRotation;
        }
        StartCoroutine(MoveTo());
    }
    
    /// <summary>
    /// Stand alone movement system for now, can be made to work with grid system later but needs some figuring out
    /// on how to make the AI choose the correct path
    /// </summary>
    private IEnumerator MoveTo()
    {
        if (Vector3.Distance(transform.position, _currentWaypoint.position) < .2f) yield break;

        Vector3 endPosition = _currentWaypoint.position;
        Vector3 startPosition = transform.position;
        endPosition.y = startPosition.y;
        float timeElapsed = 0;
        
        while(timeElapsed < _movementTime)
        {
            timeElapsed += Time.deltaTime;
            float lerpStep = timeElapsed / _movementTime;
            transform.position = Vector3.Lerp(startPosition,endPosition, lerpStep);
            yield return null;
        }

        _currentWaypoint = WaypointsScript.GetNextWaypoint(_currentWaypoint);
    }

    /// <summary>
    /// Deals damage to the closest player ship in range
    /// </summary>
    private void AttackState()
    {
        _targetShip.GetComponent<Ship>().TakeDamage(_shipScript.Damage);
    }

    //Draws the attackRange of the enemy ship
    private void OnDrawGizmos()
    {
        if(!GizmosOn)return;
        Gizmos.DrawWireSphere(transform.position, (Radius * 0.866f));
    }
}
