using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemyObjects;
    private new List<GameObject> livingEnemies;

    private bool _isObjectInScene;

    private bool _enemiesRemaining = true;

    void Start()
    {
        CheckEnemiesInScene();
    }

    public void CheckEnemiesInScene()
    {
        EnemyObjects = GameObject.FindGameObjectsWithTag("Opponent");
        if (EnemyObjects != null)
        {
            //Continue
            return;
        }
        //victory
    }
}
