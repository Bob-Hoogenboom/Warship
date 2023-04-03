using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Transform playerFleet;
    [SerializeField] private Transform enemyFleet;
    
    // private bool _playerTurn = true;
    private readonly List<Ray> _ray = new();
    // private List<RaycastHit> _hit;
    private RaycastHit[] _hit;
    private EnemyFire _enemyFire;
    private float _closestTarget;
    private Collider _shipToFireAt;

    private void Awake()
    {
        EnemyTurn(false);
    }

    private void Update()
    {
        foreach (Ray ray in _ray)
        {
            if (!Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity)) continue;
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.black);
            _hit[] = hit;
        }
        // foreach (Ray ray in _ray)
        // {
        //     if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity)) continue;
        //     Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.black);
        //     _hit.Add(hit);
        // }

        if (!Input.GetKeyDown(KeyCode.Space)) return;

        // _playerTurn = false;
        _closestTarget = 1f;
        
        EnemyTurn(true);

        if (_closestTarget <= 0.8)
        {
            EnemyFire.Fire("Fire at: " + _shipToFireAt.name + ", distance is: " + _closestTarget);
        }
        else
        {
            Debug.Log("Closest ship is " + _closestTarget + " which is out of range");
        }
    }

    private void EnemyTurn(bool go)
    {
        _ray.Clear();
        
        for (int i = 0; i < enemyFleet.childCount; i++)
        {
            for (int j = 0; j < playerFleet.childCount; j++)
            {
                _ray.Add(new Ray(enemyFleet.GetChild(i).position, playerFleet.GetChild(j).position));
                if (!go) continue;
                if (_closestTarget < _hit[i+j].distance) continue;
                _closestTarget = _hit[i+j].distance;
                _shipToFireAt = _hit[i+j].collider;
            }
        }
        // _playerTurn = true;
    }
}
