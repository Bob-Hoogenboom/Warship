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

    private bool _enemyDefeat;
    private bool _playerDefeat;

    public void InvokeEndTurn()
    {
        StartCoroutine(EndTurn());
    }
    
    private IEnumerator EndTurn()
    {
        // Activate the enemy turn
        for (int i = 0; i < enemyFleet.transform.childCount; i++)
        {
            Transform enemyChild = enemyFleet.transform.GetChild(i);
            
            if (!enemyChild.gameObject.activeSelf) continue;
            
            enemyChild.GetComponent<Enemy>().StateCheck();
        }

        EnemiesLeftInScene();

        // Reset the player Actions
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < playerFleet.transform.childCount; i++)
        {
            Transform playerChild = playerFleet.transform.GetChild(i);
            
            if (!playerChild.gameObject.activeSelf) continue;

            playerChild.GetComponent<Ship>().shipMoved = false;
            playerChild.GetComponent<Player>().FindTargetsInRange();
        }
        
        PlayersLeftInScene();

        if (_enemyDefeat || _playerDefeat) SceneManager.LoadScene(0);
    }
    
    private void EnemiesLeftInScene()
    {
        for (int i = 0; i< enemyFleet.transform.childCount; i++)
        {
            if (!enemyFleet.transform.GetChild(i).gameObject.activeInHierarchy) continue;
            _enemyDefeat = false;
            return;
        }
        
        _enemyDefeat = true;
    }
    
    private void PlayersLeftInScene()
    {
        for (int i = 0; i< playerFleet.transform.childCount; i++)
        {
            if (!playerFleet.transform.GetChild(i).gameObject.activeInHierarchy) continue;
            _playerDefeat = false;
            return;
        }
        
        _playerDefeat = true;
    }
}
