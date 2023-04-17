using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private GameObject playerInput;
    [SerializeField] private bool playerTurn;

    public void EndTurn()
    {
        if (!playerTurn)
        {
            playerInput.SetActive(true);
            playerTurn = true;
            Debug.Log("Player turn");
            return;
        }
        
        playerInput.SetActive(false);
        playerTurn = false;
        Debug.Log("Enemy turn");
        // Call enemy ai script
    }
}
