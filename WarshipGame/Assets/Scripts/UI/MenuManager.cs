using UnityEngine;

/// <summary>
/// Handles all the panels in a list and activated these when an int is assigned.
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
}
