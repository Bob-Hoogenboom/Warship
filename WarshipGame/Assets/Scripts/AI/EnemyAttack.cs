using UnityEngine;

public enum States {
    Move,
    Attack,
    Skip,
}

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private LayerMask PlayerShips;
    [SerializeField] private float Radius; //works best for 1,2,3 radial values >3 becomes inefficient
    
    private States _states;
    private ShipManager _shipManagerScript;
    
    private Transform[] _targetShips;

    private void Awake()
    {
        _shipManagerScript = GetComponent<ShipManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CheckTargetInRange();
        }
    }

    private void CheckTargetInRange()
    {
        //OverlapSphere returns an array of every Collider of collision layer 'PlayerShips'
        Collider[] targetColliders = Physics.OverlapSphere(transform.position, (Radius * 0.866f), PlayerShips);
        if (targetColliders == null)
        {
            _states = States.Move;
            EnemyAction();
            return;
        }

        for (int i = 0; i < targetColliders.Length; i++)
        {
            _targetShips[i] = targetColliders[i].GetComponentInParent<Transform>();
        }

        _states = States.Attack;
    }

    private void EnemyAction()
    {
        switch (_states)
        {
            case States.Move:
                break; // move logic
            case States.Attack:
                break; // attack logic
            case States.Skip:
                break; //skip turn aka do nothing
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, (Radius * 0.866f));
    }
}
