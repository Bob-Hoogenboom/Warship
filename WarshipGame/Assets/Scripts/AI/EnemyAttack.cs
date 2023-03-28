using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Ship")) return;
        
        Debug.Log(FindClosestEnemy());
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Ship");
        GameObject closest = null;
        var distance = Mathf.Infinity;

        foreach (var go in gos)
        {
            var diff = go.transform.position - transform.position;
            var curDistance = diff.sqrMagnitude;
            if (!(curDistance < distance)) continue;
            closest = go;
            distance = curDistance;
        }

        return closest;
    }
}
