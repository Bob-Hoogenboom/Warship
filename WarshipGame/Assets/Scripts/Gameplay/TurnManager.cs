using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyFleet;
    private bool _playerTurn = true;

    private void Update()
    {
        if (!_playerTurn)
        {
            Debug.Log("enemy's turn");
            EnemyTurn();
            return;
        }

        if (!Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Still Player's turn");
            return;
        }

        _playerTurn = false;
    }

    private void EnemyTurn()
    {
        foreach (GameObject enemy in enemyFleet)
        {
            enemy.GetComponent<EnemyDetect>().Fire = true;
        }
        _playerTurn = true;
    }
}
