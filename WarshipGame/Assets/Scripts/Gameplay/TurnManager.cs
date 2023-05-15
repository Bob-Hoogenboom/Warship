using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The main function of this script is to allow the button to call EndTurn() which will activate the enemy AI and afterwards reset the PlayerFleet actions
/// </summary>
public class TurnManager : MonoBehaviour
{
    // The playerFleet and enemyFleet allows the TurnManager to grab the childeren of the GameObject so that each ship can be called individually
    [Header("Fleet")]
    [SerializeField] private GameObject playerFleet;
    [SerializeField] private GameObject enemyFleet;

    [SerializeField] private GameObject playerTurnNotification;
    [SerializeField] private GameObject enemyTurnNotification;
    
    [SerializeField] private GameObject playerVictoryNotification;
    [SerializeField] private GameObject enemyVictoryNotification; 
    
    [SerializeField] private float turnTimer = 3f;
    [SerializeField] private bool beginEnemyTurn;
    
    private GameObject _currentTurn;

    private void Awake()
    {
        if (!beginEnemyTurn)
        {
            _currentTurn = playerTurnNotification;
            return;
        }

        _currentTurn = enemyTurnNotification;
    }

    public void InvokeEndTurn()
    {
        StartCoroutine(EndTurn());
    }
    
    private IEnumerator EndTurn()
    {
        _currentTurn = enemyTurnNotification;
        StartCoroutine(TurnCoroutine(_currentTurn));
        // Activate the enemy turn
        yield return new WaitForSeconds(turnTimer);
        for (int i = 0; i < enemyFleet.transform.childCount; i++)
        {
            Transform enemyChild = enemyFleet.transform.GetChild(i);
            if (!enemyChild.gameObject.activeSelf) continue;
            
            enemyChild.GetComponent<Enemy>().StateCheck();
        }
        
        // Reset the player Actions
        _currentTurn = playerTurnNotification;
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(TurnCoroutine(_currentTurn));
        for (int i = 0; i < playerFleet.transform.childCount; i++)
        {
            Transform playerChild = playerFleet.transform.GetChild(i);
            
            if (!playerChild.gameObject.activeSelf) continue;

            playerChild.GetComponent<Ship>().shipTurn = false;
        }
        
        PlayersLeftInScene();
    }
    
    public void EnemiesLeftInScene()
    {
        for (int i = 0; i< enemyFleet.transform.childCount; i++)
        {
            if (!enemyFleet.transform.GetChild(i).gameObject.activeInHierarchy) continue;
            return;
        }
        
        playerVictoryNotification.SetActive(true);
        StartCoroutine(EndGame());
    }
    
    private void PlayersLeftInScene()
    {
        for (int i = 0; i< playerFleet.transform.childCount; i++)
        {
            if (!playerFleet.transform.GetChild(i).gameObject.activeInHierarchy) continue;
            return;
        }

        enemyVictoryNotification.SetActive(true);
        StartCoroutine(EndGame());
    }

    public void TurnNotification()
    {
        if (!_currentTurn.activeSelf)
        {
            StartCoroutine(TurnCoroutine(_currentTurn));
        }
    }

    private IEnumerator TurnCoroutine(GameObject fleetTurn)
    {
        fleetTurn.SetActive(true);
        yield return new WaitForSeconds(turnTimer);
        fleetTurn.SetActive(false);
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(15);
        SceneManager.LoadScene(0);
    }
}
