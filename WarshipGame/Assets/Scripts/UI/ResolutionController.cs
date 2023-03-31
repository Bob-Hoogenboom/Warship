using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] _resolutions;
    private List<Resolution> _filteredResolution;

    private float _currentRefreshRate;
    private int _currentResolutionIndex = 0;

    private void Start()
    {
        _resolutions = Screen.resolutions;
        _filteredResolution = new List<Resolution>();
        
        resolutionDropdown.ClearOptions();
        _currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            if (_resolutions[i].refreshRate != _currentRefreshRate) return;
                _filteredResolution.Add(_resolutions[i]);
        }

        List<string> options = new List<string>();
        for (int i = 0; i < _filteredResolution.Count; i++)
        {
            string resolutionOption = _filteredResolution[i].width + "x" + _filteredResolution[i].height + " " + _filteredResolution[i].refreshRate + " Hz";
            options.Add(resolutionOption);
            if (_filteredResolution[i].width == Screen.width && _filteredResolution[i].height == Screen.height)
                _currentResolutionIndex = i;
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = _currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _filteredResolution[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }
}
