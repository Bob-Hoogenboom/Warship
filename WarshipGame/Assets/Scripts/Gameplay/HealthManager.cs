using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script makes it so that the ship's can lose health and die which will turn them off and allows the ships current health to be displayed in a UI element
/// </summary>
public class HealthManager : MonoBehaviour
{
    [SerializeField] private GameObject playerFleet;
    // Here we set which fleet is the enemy fleet so we can check later who we are targeting
    [SerializeField] private GameObject enemyFleet;

    // Here we have a variable used to keep track of what has happened and who is receiving damage
    private bool _alreadyHaveTarget;
    private Camera _camera;
    private Stats _playerStats;
    private Stats _enemyStats;
    private Transform _hit;

    private void Awake()
    {
        // Here we set the main camera for later use
        _camera = Camera.main;
        
        // Here we set a default stats value for the HealthManager to work with in case something goes wrong
        _enemyStats = enemyFleet.GetComponentInChildren<Stats>();
        _playerStats = playerFleet.GetComponentInChildren<Stats>();
    }
    
    // Here we check if a ship got hit and if that ship is a player or an enemy
    public void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;
        
        _hit = hit.transform;
        // Here we check if the target is an enemy ship and then change the health of one of the enemies ships if true
        if (_hit.IsChildOf(enemyFleet.transform))
        {
            _enemyStats = _hit.GetComponent<Stats>();
            ChangeHealth(_playerStats.Damage, _enemyStats.HealthBar);
            return;
        }
        
        // Here we change the health of one of the player's ships
        _playerStats = _hit.GetComponent<Stats>();
        ChangeHealth(_enemyStats.Damage, _playerStats.HealthBar);
    }
    
    // Here we change the health value to the player or the enemy depending on who is the target
    private void ChangeHealth(int damageTaken, Slider healthBar)
    {
        healthBar.value -= damageTaken;
        
        if (healthBar.value > 0) return; 
        _hit.gameObject.SetActive(false);
    }
}
