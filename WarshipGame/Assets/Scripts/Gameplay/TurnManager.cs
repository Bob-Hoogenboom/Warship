using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Transform playerFleet;
    [SerializeField] private Transform enemyFleet;
    
    private bool _playerTurn = true;
    private readonly List<Ray> _ray = new();
    private RaycastHit _hit;
    private EnemyFire _enemyFire;
    private float _closestTarget;
    
    private void Update()
    {
        foreach (Ray ray in _ray)
        {
            if (!Physics.Raycast(ray.origin, ray.direction, out _hit, Mathf.Infinity)) continue;
            Debug.DrawRay(ray.origin, ray.direction * _hit.distance, Color.black);
        }
        
        if (!_playerTurn)
        {
            EnemyTurn();
            return;
        }

        if (!Input.GetKeyDown(KeyCode.Space)) return;

        _playerTurn = false;
    }

    private void EnemyTurn()
    {
        Debug.Log(_hit.distance);
        _ray.Clear();

        if (_closestTarget < _hit.distance)
        {
            _enemyFire.shipToFireAt = ;
            _enemyFire.Fire();
        }
        
        for (int i = 0; i < enemyFleet.childCount; i++)
        {
            for (int j = 0; j < playerFleet.childCount; j++)
            {
                _ray.Add(new Ray(enemyFleet.GetChild(i).position, playerFleet.GetChild(j).position));
            }
        }
        
        _playerTurn = true;
    }
}
