using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField] private Profiles profiles;

    [SerializeField] private List<AudioSliders> audioSliders = new List<AudioSliders>();

    private void Awake()
    {
        if (profiles == null) return;
            profiles.SetProfile(profiles);
    }

    private void Start()
    {
        if (Settings.profile && Settings.profile.audioMixer == null) return;
            Settings.profile.GetAudioLevels();
        }

    public void ApplyAudioChanges()
    {
        if (Settings.profile && Settings.profile.audioMixer == null) return;
            Settings.profile.SaveAudioLevels();
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
