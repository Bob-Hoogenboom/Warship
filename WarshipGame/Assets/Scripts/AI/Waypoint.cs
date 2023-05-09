using UnityEngine;

//Make it loop or turn back*
public class Waypoint : MonoBehaviour
{
    [SerializeField] private float GizmoRadius;
    [SerializeField] private bool IsPatroling;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        
        foreach (Transform t in transform)
        {
            Gizmos.DrawWireSphere(t.position, GizmoRadius * 0.866f / 2f);
        }
        
        Gizmos.color = !IsPatroling ? Color.red : Color.yellow;
        
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        if (IsPatroling)
        {
            Gizmos.DrawLine(transform.GetChild(transform.childCount-1).position, transform.GetChild(0).position);
            return;
        }
        
        Vector3 endPoint = transform.GetChild(transform.childCount - 1).position;
        Gizmos.DrawLine(endPoint, new Vector3(endPoint.x, endPoint.y + 1f, endPoint.z ));
    }

    /// <summary>
    /// Sets the new waypoint transform to the next objects transform in line
    /// </summary>
    /// <param name="currentWaypoint"></param>
    /// <returns>returns the new waypoint</returns>
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
            return transform.GetChild(transform.childCount - 1);
        }

        return transform.GetChild(0);
    }
}
