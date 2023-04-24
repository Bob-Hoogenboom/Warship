using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Puts all the enemy objects with the "opponent" tag in a list ann detects if there are remaining enemy objects in the scene
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemyObjects;
    [SerializeField] private bool _enemiesRemaining = true;
    private new List<GameObject> livingEnemies;
    private bool _isObjectInScene;

    void Start()
    {
        CheckEnemiesInScene();
    }

    /// <summary>
    /// Detects any objects with the opponent tag and puts these in a  list
    /// If there are enemies left then the bool will be set to true, if there are none then its false.
    /// </summary>
    public void CheckEnemiesInScene()
    {
        EnemyObjects = GameObject.FindGameObjectsWithTag("Opponent");
        if (EnemyObjects.Length != 0)
        {
            _enemiesRemaining = true;
            return;
        }
        _enemiesRemaining = false;
    }
}
