using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script makes it so that the ships can lose health and die which will turn them off and allows the ships current health to be displayed in a ui element
/// </summary>
public class HealthManager : MonoBehaviour
{
    // Here we can set which element is for the enemy and which one is for the player
    [SerializeField] private Slider sliderPlayer;
    [SerializeField] private Slider sliderEnemy;
    [SerializeField] private TMP_Text textPlayer;
    [SerializeField] private TMP_Text textEnemy;
    [SerializeField] private GameObject enemyFleet;
    [SerializeField] private Material selectedM;
    [SerializeField] private Material unSelectedM;

    private bool _alreadyHaveTarget;
    private GameObject _playerTarget;
    private GameObject _enemyTarget;
    private Ray _ray;
    private RaycastHit _hit;
    
    // here we check if a ship got and hit and if that ship is a player or a enemy
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        if (Camera.main != null) _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(_ray, out _hit, Mathf.Infinity)) return;
        
        if (_hit.transform.GetComponent<Stats>().transform.IsChildOf(enemyFleet.transform))
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
        if (_playerTarget.transform.GetComponent<Stats>().Selected)
        {
            _playerTarget.GetComponent<MeshRenderer>().material = unSelectedM;
            _playerTarget.transform.GetComponent<Stats>().Selected = false;
            _alreadyHaveTarget = false;
            return;
        }

        if (_alreadyHaveTarget) return;

        textPlayer.text = _playerTarget.transform.GetComponent<Stats>().CurrentHealth.ToString();
        sliderPlayer.maxValue = _playerTarget.transform.GetComponent<Stats>().MaxHealth;
        sliderPlayer.value = _playerTarget.transform.GetComponent<Stats>().CurrentHealth;
        
        _alreadyHaveTarget = true;
        _playerTarget.transform.GetComponent<Stats>().Selected = true;
        _playerTarget.GetComponent<MeshRenderer>().material = selectedM;
        
    }
    
    
    // Here we deal the damage to the enemy
    private void EnemyDamage()
    {
        ChangeHealth(_playerTarget.transform.GetComponent<Stats>().Damage, _playerTarget.transform.GetComponent<Stats>().IsPlayer);
        
        textEnemy.text = _enemyTarget.transform.GetComponent<Stats>().CurrentHealth.ToString();
        sliderEnemy.maxValue = _enemyTarget.transform.GetComponent<Stats>().MaxHealth;
        sliderEnemy.value = _enemyTarget.transform.GetComponent<Stats>().CurrentHealth;
    }
    
    // Here we change the health value to the player or the enemy depending on if we call PlayerDamage() or EnemyDamage()
    private void ChangeHealth(int damageTaken, bool player)
    {
        if (!player)
        {
            _playerTarget.transform.GetComponent<Stats>().CurrentHealth -= damageTaken;
            sliderPlayer.value = _playerTarget.transform.GetComponent<Stats>().CurrentHealth;
            textPlayer.text = _playerTarget.transform.GetComponent<Stats>().CurrentHealth.ToString();

            if (_playerTarget.GetComponent<Stats>().CurrentHealth > 0) return;
            _playerTarget.SetActive(false);
            return;
        }
        _enemyTarget.GetComponent<Stats>().CurrentHealth -= damageTaken;
        sliderEnemy.value = _enemyTarget.GetComponent<Stats>().CurrentHealth;
        textEnemy.text = _enemyTarget.GetComponent<Stats>().CurrentHealth.ToString();
        
        if (_enemyTarget.GetComponent<Stats>().CurrentHealth > 0) return;
        _enemyTarget.SetActive(false);
    }
}
