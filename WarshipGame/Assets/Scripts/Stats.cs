using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    public int CurrentHealth { get; set; }
    
    private void Awake()
    {
        CurrentHealth = maxHealth;
    }
}
