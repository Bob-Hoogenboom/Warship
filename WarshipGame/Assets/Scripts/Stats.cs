using UnityEngine;

/// <summary>
/// This script allows us to give ships individual stats so that we can easily create a difference between ships
/// </summary>
public class Stats : MonoBehaviour
{
    [Tooltip("Stats of the Ship")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private bool isPlayer;

    // this allows us to use the stats cross script
    public bool Selected { get; set; }
    public int CurrentHealth { get; set; }
    public int MaxHealth => maxHealth;
    public bool IsPlayer => isPlayer;
    public int Damage => damage;

    // sets the CurrentHealth to the maxHealth the moment the ship becomes active
    private void Awake() { CurrentHealth = maxHealth; }
}
