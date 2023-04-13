using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script allows us to give ships individual stats so that we can easily create a difference between ships
/// </summary>
public class Stats : MonoBehaviour
{
    private Camera _camera;
    
    [Header("Stats of the Ship")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private Slider healthBar;

    // This allows us to use the stats cross script
    public int Damage => damage;
    public Slider HealthBar => healthBar;

    // This updated the health bar rotation to always look at the camera
    private void LateUpdate()
    {
        healthBar.transform.LookAt(healthBar.transform.position + _camera.transform.forward);
    }

    // Sets the CurrentHealth to the maxHealth the moment the ship becomes active
    private void Awake()
    {
        _camera = Camera.main;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }
}