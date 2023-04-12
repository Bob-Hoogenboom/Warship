using UnityEngine;

/// <summary>
/// This script makes it so that the ships can lose health and die which will turn them off and allows the ships current health to be displayed in a ui element
/// </summary>
public class HealthManager : MonoBehaviour
{
    // Here we can set which element is for the enemy and which one is for the player
    [SerializeField] private GameObject enemyFleet;

    private bool _alreadyHaveTarget;
    private GameObject _playerTarget;
    private GameObject _enemyTarget;
    private Ray _ray;
    private RaycastHit _hit;
    private Camera _camera;
    private Stats _stats;
    
    // here we check if a ship got and hit and if that ship is a player or a enemy
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(_ray, out _hit, Mathf.Infinity)) return;

        _stats = _hit.transform.GetComponent<Stats>();
        if (_stats.transform.IsChildOf(enemyFleet.transform))
        {
            _enemyTarget = _hit.transform.gameObject;
            EnemyDamage();
            return;
        }
        
        _playerTarget = _hit.transform.gameObject;
        
        PlayerDamage();
    }

    // Here we deal the damage to the player
    private void PlayerDamage()
    {
        if (_stats.Selected)
        {
            _stats.Selected = false;
            _alreadyHaveTarget = false;
            return;
        }

        if (_alreadyHaveTarget) return;

        _stats.healthBar.maxValue = _stats.MaxHealth;
        _stats.healthBar.value = _stats.CurrentHealth;
        
        _alreadyHaveTarget = true;
        _stats.Selected = true;
        
    }
    
    
    // Here we deal the damage to the enemy
    private void EnemyDamage()
    {
        ChangeHealth(_stats.Damage, _stats.IsPlayer);
        
        _stats.healthBar.maxValue = _stats.MaxHealth;
        _stats.healthBar.value = _stats.CurrentHealth;
    }
    
    // Here we change the health value to the player or the enemy depending on if we call PlayerDamage() or EnemyDamage()
    private void ChangeHealth(int damageTaken, bool player)
    {
        if (!player)
        {
            _stats.CurrentHealth -= damageTaken;
            _stats.healthBar.value = _stats.CurrentHealth;

            if (_stats.CurrentHealth > 0) return;
            _enemyTarget.SetActive(false);
            return;
        }
        _stats.CurrentHealth -= damageTaken;
        _stats.healthBar.value = _stats.CurrentHealth;
        
        if (_stats.CurrentHealth > 0) return;
        _playerTarget.SetActive(false);
    }
}
