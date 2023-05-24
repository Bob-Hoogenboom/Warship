using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// this is just a dummy script to make the game function
/// the player attack should be better implemented then this*
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private LayerMask enemyShips;
    [SerializeField] private float radius;
    [SerializeField] private bool gizmosOn = true;
    
    [SerializeField] private UnityEvent onPlayerAttack;
    
    private Ship _shipScript;
    private Transform _targetShip;

    private void Start()
    {
        _shipScript = gameObject.GetComponent<Ship>();
    }

    /// <summary>
    /// Here we check is a enemy is in range for us to hit it and then we end the ship turn
    /// </summary>
    /// <param name="targetShip"></param>
    public void Attack(Transform targetShip)
    {
        onPlayerAttack.Invoke();
        Collider[] targetColliders = Physics.OverlapSphere(transform.position, (radius * 0.866f), enemyShips);

        foreach (Collider targetCollider in targetColliders)
        {
            if (targetCollider.transform != targetShip) continue;
        
            targetShip.GetComponent<Ship>().TakeDamage(_shipScript.Damage);
            GetComponent<Ship>().shipTurn = true;
            
            return;
        }
    }
    
    private void OnDrawGizmos()
    {
        if(!gizmosOn)return;
        Gizmos.DrawWireSphere(transform.position, (radius * 0.866f));
    }
}