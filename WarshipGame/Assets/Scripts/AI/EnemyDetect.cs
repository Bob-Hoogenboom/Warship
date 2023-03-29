using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    public void FireAtEnemy()
    {
        Debug.Log(FindClosestEnemy());
    }
    
    // private void OnTriggerStay(Collider other)
    // {
    //     if (!other.CompareTag("Ship")) return;
    //     
    //     Debug.Log(FindClosestEnemy());
    // }

    private GameObject FindClosestEnemy()
    {
        var playerShips = GameObject.FindGameObjectsWithTag("Ship");
        GameObject closestShip = null;
        var distance = Mathf.Infinity;

        foreach (var go in playerShips)
        {
            var diff = go.transform.position - transform.position;
            var curDistance = diff.sqrMagnitude;
            if (curDistance > distance || curDistance == 0) continue;
            closestShip = go;
            distance = curDistance;
        }
        return closestShip;
    }
}
