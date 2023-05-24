using UnityEngine;

public class Seagull : MonoBehaviour
{
    [Tooltip("The path Object of the Seagull")]
    [SerializeField] private Waypoint waypoints;
    
    [Tooltip("The speed * Time.deltaTime at which the Seagull travels")]
    [SerializeField] private float movementSpeed;
    
    [Tooltip("The threshold distance of the Seagull to the waypoint")]
    [SerializeField] private float distanceThreshold;

    private Transform _currentWaypoint;
    
    private void Start()
    {
        _currentWaypoint = waypoints.GetNextWaypoint(_currentWaypoint);
        transform.position = _currentWaypoint.position;

        _currentWaypoint = waypoints.GetNextWaypoint(_currentWaypoint);
        transform.LookAt(_currentWaypoint);
    }
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentWaypoint.position, movementSpeed * Time.deltaTime);
        
        if (!(Vector3.Distance(transform.position, _currentWaypoint.position) < distanceThreshold)) return;
        _currentWaypoint = waypoints.GetNextWaypoint(_currentWaypoint);
        transform.LookAt(_currentWaypoint);
    }
}
