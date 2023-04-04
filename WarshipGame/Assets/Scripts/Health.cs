using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider sliderPlayer;
    [SerializeField] private Slider sliderEnemy;
    
    [SerializeField] private TMP_Text textPlayer;
    [SerializeField] private TMP_Text textEnemy;
    
    [SerializeField] private GameObject playerFleet;
    [SerializeField] private GameObject enemyFleet;

    private void Awake()
    {
        textPlayer.text = playerFleet.GetComponentInChildren<Stats>().CurrentHealth.ToString();
        textEnemy.text = enemyFleet.GetComponentInChildren<Stats>().CurrentHealth.ToString();
        
        sliderPlayer.maxValue = playerFleet.GetComponentInChildren<Stats>().CurrentHealth;
        sliderEnemy.maxValue = enemyFleet.GetComponentInChildren<Stats>().CurrentHealth;
        
        sliderPlayer.value = playerFleet.GetComponentInChildren<Stats>().CurrentHealth;
        sliderEnemy.value = enemyFleet.GetComponentInChildren<Stats>().CurrentHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeHealth(1, true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeHealth(1, false);
        }
    }

    private void ChangeHealth(int damageTaken, bool player)
    {
        if (player)
        {
            playerFleet.GetComponentInChildren<Stats>().CurrentHealth -= damageTaken;
            sliderPlayer.value = playerFleet.GetComponentInChildren<Stats>().CurrentHealth;
            textPlayer.text = playerFleet.GetComponentInChildren<Stats>().CurrentHealth.ToString();
            
            if (playerFleet.GetComponentInChildren<Stats>().CurrentHealth > 0) return;
            Destroy(playerFleet.GetComponentInChildren<Component>());
            return;
        }
        enemyFleet.GetComponentInChildren<Stats>().CurrentHealth -= damageTaken;
        sliderEnemy.value = enemyFleet.GetComponentInChildren<Stats>().CurrentHealth;
        textEnemy.text = enemyFleet.GetComponentInChildren<Stats>().CurrentHealth.ToString();
        
        if (enemyFleet.GetComponentInChildren<Stats>().CurrentHealth > 0) return;
        Destroy(enemyFleet.GetComponentInChildren<Component>());
    }
}
