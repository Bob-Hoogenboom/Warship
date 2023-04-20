using Unity.VisualScripting;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private float GizmoRadius;
    [SerializeField] private bool IsPatroling;
    

    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(t.position, GizmoRadius * 0.866f /2);
        }

        
        Gizmos.color = Color.red;
        
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        if (IsPatroling)
        {
            Gizmos.DrawLine(transform.GetChild(transform.childCount-1).position, transform.GetChild(0).position);
            return;
        }

        Gizmos.color = Color.magenta;
        Transform EndPoint = transform.GetChild(transform.childCount - 1);
        Gizmos.DrawLine(EndPoint.position, new Vector3(EndPoint.position.x, EndPoint.position.y +1f, EndPoint.position.z ));
    }

    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        if (currentWaypoint == null)
        {
            return transform.GetChild(0);
        }

        if (currentWaypoint.GetSiblingIndex() < transform.childCount - 1)
        {
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
        
        if (currentWaypoint.GetSiblingIndex() >= transform.childCount - 1 && !IsPatroling)
        {
            Debug.Log("End");
            return transform.GetChild(transform.childCount - 1);
        }

        return transform.GetChild(0);
    }
}
