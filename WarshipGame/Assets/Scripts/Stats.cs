using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script allows us to give ships individual stats so that we can easily create a difference between ships
/// </summary>
public class Stats : MonoBehaviour
{
    [Tooltip("Stats of the Ship")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private bool isPlayer;
    private Camera _camera;
    public Slider healthBar;
    

    // this allows us to use the stats cross script
    public bool Selected { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth => maxHealth;
    public bool IsPlayer => isPlayer;
    public int Damage => damage;

    private void LateUpdate()
    {
        healthBar.transform.LookAt(healthBar.transform.position + _camera.transform.forward);
    }

    // sets the CurrentHealth to the maxHealth the moment the ship becomes active
    private void Awake()
    {
        _camera = Camera.main;
        CurrentHealth = maxHealth;
    }
}