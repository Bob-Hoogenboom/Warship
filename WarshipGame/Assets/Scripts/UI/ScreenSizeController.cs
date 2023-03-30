using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScreenSizeController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown Dropdown;
    private int screenMode;

    void Awake()
    {
        Dropdown.value = 0;
    }

    public void setScreenSize()
    {
        screenMode = Dropdown.value;
        switch (screenMode)
        {
            case 0:
                Dropdown.value = 0;
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Dropdown.value = 1;
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 2:
                Dropdown.value = 2;
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
        }
    }
}
