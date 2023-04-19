using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private GameObject playerInput;
    [SerializeField] private GameObject playerFleet;
    [SerializeField] private bool playerTurn;

    public void EndTurn()
    {
        if (!playerTurn)
        {
            playerTurn = true;
            playerFleet.GetComponentInChildren<Ship>().shipMoved = false;

            Debug.Log("Player turn");
            return;
        }
        playerFleet.GetComponentInChildren<Ship>().shipMoved = true;
        playerTurn = false;
        // Call enemy ai script
        
        Debug.Log("Enemy turn");
    }
}
