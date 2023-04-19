using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private float GizmoRadius;
    [SerializeField] private bool IsPatroling;

    private GameObject[] _childObjects;

    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(t.position, GizmoRadius * 0.866f);
        }

        
        Gizmos.color = Color.red;
        
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        if (IsPatroling)
        {
            Gizmos.DrawLine(transform.GetChild(transform.childCount-1).position, transform.GetChild(0).position);
        }
    }

    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        if (currentWaypoint != null)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }

        return transform.GetChild(0);
    }
}
