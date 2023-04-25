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
        Debug.Log("Enemy turn");

        for (int i = 0; i < enemyFleet.transform.childCount; i++)
        {
            //How to invert if?
            if (!enemyFleet.transform.GetChild(i).gameObject.activeSelf) continue;
            
            enemyFleet.transform.GetChild(i).GetComponent<Enemy>().StateCheck();
        }

        EnemiesLeftInScene();

        // Reset the player Actions
        Debug.Log("Player turn");

        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < playerFleet.transform.childCount; i++)
        {
            //How to invert if?
            if (!playerFleet.transform.GetChild(i).gameObject.activeSelf) continue;
            playerFleet.transform.GetChild(i).GetComponent<Ship>().shipMoved = false;
            playerFleet.transform.GetChild(i).GetComponent<Player>().FindTargetsInRange();
            
        }
        
        PlayersLeftInScene();

        if (_enemyDefeat || _playerDefeat) SceneManager.LoadScene(0);
        yield return null;
    }
    
    private void EnemiesLeftInScene()
    {
        for (int i = 0; i< enemyFleet.transform.childCount; i++)
        {
            if(enemyFleet.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                Debug.Log("false");
                _enemyDefeat = false;
                return;
            }
        }
        
        Debug.Log("Game End, Player win");
        _enemyDefeat = true;
    }
    
    private void PlayersLeftInScene()
    {
        for (int i = 0; i< playerFleet.transform.childCount; i++)
        {
            if(playerFleet.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                Debug.Log("false");
                _playerDefeat = false;
                return;
            }
        }
        
        Debug.Log("Game End, Enemy win");
        _playerDefeat = true;
    }
}
