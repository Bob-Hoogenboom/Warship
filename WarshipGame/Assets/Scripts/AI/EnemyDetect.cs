using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    [SerializeField] private Transform playerFleet;

    private List<Ray> _ray = new List<Ray>();
    public bool Fire {get; set;}
    // private void OnTriggerStay(Collider other)
    // {
    //     if (!other.CompareTag("Ship") || !Fire)
    //     {
    //         return;
    //     }
    //
    //     Debug.Log(FindClosestEnemy());
    //
    //     Fire = false;
    // }
    
    private void Update()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            for (int j = 0; j < playerFleet.childCount; j++)
            {
                _ray.Add(new Ray(gameObject.transform.GetChild(i).position, playerFleet.GetChild(j).position));
            }
        }
        foreach (Ray ray in _ray)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, Mathf.Infinity))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.black);
            }
        }
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] playerShips = GameObject.FindGameObjectsWithTag("Ship");
        GameObject closestShip = null;
        float distance = Mathf.Infinity;

        foreach (GameObject go in playerShips)
        {
            Vector3 diff = go.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance > distance || curDistance == 0) continue;
            closestShip = go;
            distance = curDistance;
        }
        return closestShip;
    }
}
