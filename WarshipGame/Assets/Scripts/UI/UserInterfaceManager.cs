using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField] private Profiles m_profile;

    [SerializeField] private List<AudioSliders> m_audioSliders = new List<AudioSliders>();

    void Awake()
    {
        if (m_profile != null)
        {
            m_profile.SetProfile(m_profile);
        }
    }

    void Start()
    {
        if (Settings.profile && Settings.profile.audioMixer != null)
        {
            Settings.profile.GetAudioLevels();
        }
    }

    public void ApplyAudioChanges()
    {
        if (Settings.profile && Settings.profile.audioMixer != null)
        {
            Settings.profile.SaveAudioLevels();
        }
    }

    // public void CancelAudioChanges()
    // {
    //     if (Settings.profile && Settings.profile.audioMixer != null)
    //     {
    //         Settings.profile.GetAudioLevels();
    //     }
    //
    //     for (int i = 0; i < m_audioSliders.Count; i++)
    //     {
    //         m_audioSliders[i].ResetSliderValue();
    //     }
    // }

    public void LoadScene(string _loadNextScene)
    {
        SceneManager.LoadScene(_loadNextScene);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
