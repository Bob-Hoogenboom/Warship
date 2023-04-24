using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private GameObject playerFleet;
    [SerializeField] private GameObject enemyFleet;

    public void EndTurn()
    {
        Debug.Log("Enemy turn");
            
        for (int i = 0; i < playerFleet.transform.childCount; i++)
        {
            playerFleet.transform.GetChild(i).GetComponent<Ship>().shipMoved = true;
        }

        for (int i = 0; i < enemyFleet.transform.childCount; i++)
        {
            enemyFleet.transform.GetChild(i).GetComponent<Enemy>().StateCheck();
        }
        
        Debug.Log("Player turn");
        
        for (int i = 0; i < playerFleet.transform.childCount; i++)
        {
            playerFleet.transform.GetChild(i).GetComponent<Ship>().shipMoved = false;
        }
    }
}
