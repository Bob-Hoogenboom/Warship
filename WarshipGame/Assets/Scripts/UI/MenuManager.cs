using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] nextPanel;
    [SerializeField] private GameObject currentPanel;

    public void HandlePanels(int i)
    {
        nextPanel[i].SetActive(true);
        currentPanel.SetActive(false);
        if (currentPanel != null)
        {
            currentPanel = nextPanel[i];
        }
    }
}
