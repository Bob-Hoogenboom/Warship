using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public enum States {
    Move,
    Attack,
    Skip,
}

public class EnemyAttack : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private LayerMask PlayerShips;
    [SerializeField] private float Radius; //works best for 1,2,3 radial values >3 becomes inefficient
    private States _states;

    //attack
    private Transform _targetShip;

    //movement
    [Header("Movement")]
    [SerializeField] private Waypoint WaypointsScript;

    [Tooltip("The time it takes to rotate the ship when it needs tot turn to a different tile")]
    [SerializeField] private float rotationTime = 0.3f;
    
    [Tooltip("The time it takes the ship to move from tile to tile")]
    [SerializeField] private float movementTime = 1f;

    private Transform _currentWaypoint;


    private void Start()
    {
        _currentWaypoint = WaypointsScript.GetNextWaypoint(_currentWaypoint);
        transform.position = _currentWaypoint.position;
        
        _currentWaypoint = WaypointsScript.GetNextWaypoint(_currentWaypoint);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CheckTargetInRange();
        }
    }

    private void CheckTargetInRange()
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

    private void EnemyAction()
    {
        switch (_states)
        {
            case States.Move:
                StartCoroutine(MoveState());
                break;
            
            case States.Attack:
                AttackState();
                break;
            
            case States.Skip:
                break; //skip turn aka do nothing
        }
    }
    
    /// <summary>
    /// Stand alone movement system for now, can be made to work with grid system later but needs some figuring out
    /// on how to make the AI choose the correct path
    /// </summary>
    private IEnumerator MoveState()
    {
        Debug.Log("I am going to move to " + _currentWaypoint);
        while (transform.position != _currentWaypoint.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentWaypoint.position, movementTime * Time.deltaTime);
            yield return null;
        }
    }
    
    private void AttackState()
    {
        Debug.Log("I am Going to Attack " + _targetShip);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, (Radius * 0.866f));
    }
}
