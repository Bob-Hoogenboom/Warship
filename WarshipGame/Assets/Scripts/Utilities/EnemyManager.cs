using System.Collections.Generic;
using UnityEngine;

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
