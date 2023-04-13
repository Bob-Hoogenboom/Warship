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

    // Sets the healthBar value to the maxHealth
    private void Awake()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }
}