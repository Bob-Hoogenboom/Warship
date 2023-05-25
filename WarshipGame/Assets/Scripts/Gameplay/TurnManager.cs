using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

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
    
    [SerializeField] private Button endTurnButton;
    [SerializeField] private float turnTimer = 3f;
    [SerializeField] private bool beginEnemyTurn;
    
    [SerializeField] private UnityEvent onGameWon;
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
        endTurnButton.enabled = false;
        StartCoroutine(EndTurn());
    }
    
    private IEnumerator EndTurn()
    {
        _currentTurn = enemyTurnNotification;
        StartCoroutine(TurnCoroutine(_currentTurn));
        beginEnemyTurn = true;
        EndMovement();
        // Activate the enemy turn
        yield return new WaitForSeconds(turnTimer);
        for (int i = 0; i < enemyFleet.transform.childCount; i++)
        {
            Transform enemyChild = enemyFleet.transform.GetChild(i);
            
            enemyChild.GetComponent<Enemy>().StateCheck();
        }
        
        // Reset the player Actions
        _currentTurn = playerTurnNotification;
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(TurnCoroutine(_currentTurn));
        endTurnButton.enabled = true;
        beginEnemyTurn = false;

        EndMovement();
        
        PlayersLeftInScene();
    }

    private void EndMovement()
    {
        for (int i = 0; i < playerFleet.transform.childCount; i++)
        {
            Transform playerChild = playerFleet.transform.GetChild(i);
            
            if (!playerChild.gameObject.activeSelf) continue;

            if (beginEnemyTurn)
            {
                playerChild.GetComponent<Ship>().shipTurn = true;
                continue;
            }
            playerChild.GetComponent<Ship>().shipTurn = false;
        }
    }

    public void EnemiesLeftInScene()
    {
        for (int i = 0; i< enemyFleet.transform.childCount; i++)
        {
            if (enemyFleet.transform.GetChild(i).GetComponent<Ship>().shipDead) continue;
            return;
        }
        
        onGameWon.Invoke();
        playerVictoryNotification.SetActive(true);
        beginEnemyTurn = true;
        EndMovement();
        StartCoroutine(EndGame());
    }
    
    private void PlayersLeftInScene()
    {
        for (int i = 0; i< playerFleet.transform.childCount; i++)
        {
            if (playerFleet.transform.GetChild(i).GetComponent<Ship>().shipDead) continue;
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
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(0);
    }
}
