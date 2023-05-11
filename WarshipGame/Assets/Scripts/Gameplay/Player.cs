using UnityEngine;

/// <summary>
/// this is just a dummy script to make the game function
/// the player attack should be better implemented then this*
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private LayerMask EnemyShips;
    [SerializeField] private float Radius;
    [SerializeField] private bool GizmosOn = true;
    
    private bool _targetInRange;
    private Ship _shipScript;
    private Transform _targetShip;

    private void Start()
    {
        _shipScript = gameObject.GetComponent<Ship>();
    }

    public Transform FindTargetsInRange()
    {
        Collider[] targetColliders = Physics.OverlapSphere(transform.position, (Radius * 0.866f), EnemyShips);
        if (targetColliders.Length == 0)
        {
            _targetInRange = false; 
            return null;
        }

        _targetInRange = true;
        return _targetShip = targetColliders[0].GetComponentInParent<Transform>();
    }

    public void Attack(Transform targetShip)
    {
        targetShip.GetComponent<Ship>().TakeDamage(_shipScript.Damage);
        _shipScript.shipMoved = true;
        Debug.Log("Attack");
    }
    
    private void OnDrawGizmos()
    {
        if(!GizmosOn)return;
        Gizmos.DrawWireSphere(transform.position, (Radius * 0.866f));
    }
}