using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private LayerMask EnemyShips;
    [SerializeField] private float Radius;
    [SerializeField] private bool GizmosOn = true;
    
    //attack
    [SerializeField] private Button _attackButtton;
    private Ship _shipScript;
    private Transform _targetShip;

    private void Start()
    {
        _shipScript = gameObject.GetComponent<Ship>();
        _attackButtton = FindObjectOfType<Button>(); //Awfull line but works for now
    }

    /// <summary>
    /// !needs to be merged into ship.CS because Enemy.cs uses the same code
    /// </summary>
    public void FindTargetsInRange()
    {
        Collider[] targetColliders = Physics.OverlapSphere(transform.position, (Radius * 0.866f), EnemyShips);
        if (targetColliders.Length == 0)
        {
            _attackButtton.enabled = false;
            return;
        }
        _attackButtton.enabled = true;
        _targetShip = targetColliders[targetColliders.Length].GetComponentInParent<Transform>();
        AttackState();
    }
    
    private void AttackState()
    {
        _targetShip.GetComponent<Ship>().TakeDamage(_shipScript.Damage);
    }
    
    private void OnDrawGizmos()
    {
        if(!GizmosOn)return;
        Gizmos.DrawWireSphere(transform.position, (Radius * 0.866f));
    }
}
