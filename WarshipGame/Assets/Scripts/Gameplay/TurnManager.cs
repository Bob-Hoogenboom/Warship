using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private GameObject playerFleetManager;
    [SerializeField] private GameObject enemyFleetManager;
    private List<GameObject> _playerFleet;
    private List<GameObject> _enemyFleet;
    private bool _playerTurn = true;

    private void Awake()
    {
        foreach (var child in playerFleetManager.GetComponentInChildren(Component))
        {
            _playerFleet.Add(child);
        }
        foreach (var child in enemyFleetManager.GetComponentsInChildren(typeof(GameObject)))
        {
            _enemyFleet.Add(child);
        }
    }

    private void Update()
    {
        if (!_playerTurn) EnemyTurn();
        
        if (!Input.GetKey(KeyCode.Space)) return;

        _playerTurn = false;
    }

    private void EnemyTurn()
    {
        foreach (var fire in _enemyFleet)
        {
            fire.GetComponent<EnemyDetect>().FireAtEnemy();
        }
        _playerTurn = true;
    }
}
