using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles all userinterface data being used by the player.
/// </summary>
public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] nextPanel;
    [SerializeField] private GameObject currentPanel;

    /// <summary>
    /// Activates the panel that is requested from a list. The panel that is no longer used will be inactivated while the new panel becomes the new current panel.
    /// </summary>
    /// <param name="i"></param>
    public void HandlePanels(int i)
    {
        nextPanel[i].SetActive(true);
        currentPanel.SetActive(false);
        if (currentPanel == null) return;
            currentPanel = nextPanel[i];
    }
    
    public void LoadScene(string loadNextScene)
    {
        SceneManager.LoadScene(loadNextScene);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
