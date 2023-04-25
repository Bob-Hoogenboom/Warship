using UnityEngine;

/// <summary>
/// The main function of this script is to allow the button to call EndTurn() which will activate the enemy AI and afterwards reset the PlayerFleet actions
/// </summary>
public class TurnManager : MonoBehaviour
{
    // The playerFleet and enemyFleet allows the TurnManager to grab the childeren of the GameObject so that each ship can be called individually
    [Header("Fleet")]
    [SerializeField] private GameObject playerFleet;
    [SerializeField] private GameObject enemyFleet;

    public void EndTurn()
    {
        // Activate the enemy turn
        Debug.Log("Enemy turn");
        
        for (int i = 0; i < enemyFleet.transform.childCount; i++)
        {
            enemyFleet.transform.GetChild(i).GetComponent<Enemy>().StateCheck();
        } 
        
        // Reset the player Actions
        Debug.Log("Player turn");
        
        for (int i = 0; i < playerFleet.transform.childCount; i++)
        {
            playerFleet.transform.GetChild(i).GetComponent<Ship>().shipMoved = false;
            playerFleet.transform.GetChild(i).GetComponent<Player>().FindTargetsInRange();
        }
    }
}
