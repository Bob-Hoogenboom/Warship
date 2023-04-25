using UnityEngine;
using UnityEngine.UI;

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
    
    //attack
    private bool _targetInRange;
    private Ship _shipScript;
    private Transform _targetShip;

    private void Start()
    {
        _shipScript = gameObject.GetComponent<Ship>();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Alpha2) || !_targetInRange || _shipScript.shipMoved) return;
        Attack();
    }

    /// <summary>
    /// !needs to be merged into ship.CS because Enemy.cs uses the same code
    /// </summary>
    public void FindTargetsInRange()
    {
        Collider[] targetColliders = Physics.OverlapSphere(transform.position, (Radius * 0.866f), EnemyShips);
        if (targetColliders.Length == 0)
        {
            _targetInRange = false; 
            return;
        }

        _targetInRange = true;
        _targetShip = targetColliders[0].GetComponentInParent<Transform>();
    }
    
    private void Attack()
    {
        _targetShip.GetComponent<Ship>().TakeDamage(_shipScript.Damage);
        _shipScript.shipMoved = true;
    }
    
    private void OnDrawGizmos()
    {
        if(!GizmosOn)return;
        Gizmos.DrawWireSphere(transform.position, (Radius * 0.866f));
    }
}
