using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages data getters and setters for the audio settings.
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField] private Profiles profiles;

    private void Awake()
    {
        if (profiles == null) return;
        profiles.SetProfile(profiles);
    }

    private void Start()
    {
        if (Settings.profile && Settings.profile.Mixer == null) return;
        Settings.profile.GetAudioLevels();
    }

    public void ApplyAudioChanges()
    {
        if (Settings.profile && Settings.profile.Mixer == null) return;
        Settings.profile.SaveAudioLevels();
    }
}
