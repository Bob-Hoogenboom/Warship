using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]

public class Volume
{
    public string AudioName;
    public float AudioVolume = 1f;
    public float TempVolume = 1f;
}

public class Settings
{
    public static Profiles profile;
}

/// <summary>
/// Create a scriptable object to store the adjusted audio data in-game.
/// </summary>
[CreateAssetMenu(menuName = "Data/Create Profile")]
public class Profiles : ScriptableObject
{
    [SerializeField] private Volume[] VolumeControl;
    private bool _saveInPlayerPref = true;
    private string _prefPrefix = "Settings_";
    
    public AudioMixer Mixer;
    
    public void SetProfile(Profiles profile)
    {
        Settings.profile = profile;
    }
    
    /// <summary>
    /// Changes the Audio level by name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public float GetAudioLevels(string name)
    {
        float volume = 1f;

        if (!Mixer)
        {
            return volume;
        }
        
        foreach (Volume volumeControl in VolumeControl)
        {
            if (volumeControl.AudioName != name) continue;
            
            if (_saveInPlayerPref)
            {
                if (PlayerPrefs.HasKey(_prefPrefix + volumeControl.AudioName))
                {
                    volumeControl.AudioVolume = PlayerPrefs.GetFloat(_prefPrefix + volumeControl.AudioName);
                }
            }
            //resets the audio volume
            volumeControl.TempVolume = volumeControl.AudioVolume;

            //sets the mixer to match the volume
            if (Mixer)
            {
                Mixer.SetFloat(volumeControl.AudioName, Mathf.Log(volumeControl.AudioVolume) * 20f);
            }
            volume = volumeControl.AudioVolume;
            break;
        }
        return volume;
    }

    /// <summary>
    /// Changes all the audio levels.
    /// </summary>
    public void GetAudioLevels()
    {
        if (!Mixer) return;

        foreach (Volume volumeControl in VolumeControl)
        {
            if (_saveInPlayerPref)
            {
                if (PlayerPrefs.HasKey(_prefPrefix + volumeControl.AudioName))
                {
                    volumeControl.AudioVolume = PlayerPrefs.GetFloat(_prefPrefix + volumeControl.AudioName);
                }
            }
            //resets the audio volume
            volumeControl.TempVolume = volumeControl.AudioVolume;

            //sets the mixer to match the volume
            Mixer.SetFloat(volumeControl.AudioName, Mathf.Log(volumeControl.AudioVolume) * 20f);
        }
    }

    /// <summary>
    /// Sets the audio level of the current scene with 'tempVolume' and changes it for the other scenes with 'volume'.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="volume"></param>
    public void SetAudioLevels(string name, float volume)
    {
        if (!Mixer) return;
        foreach (Volume volumeControl in VolumeControl)
        {
            if (volumeControl.AudioName != name) continue;
            
            Mixer.SetFloat(volumeControl.AudioName, Mathf.Log(volume) * 20f);
            volumeControl.TempVolume = volume;
            break;
        }
    }

    /// <summary>
    /// Saves the updated values into the scriptable object.
    /// </summary>
    public void SaveAudioLevels()
    {
        if (!Mixer) return;

        foreach (Volume volumeControl in VolumeControl)
        {
            float volume = volumeControl.TempVolume;
            if (_saveInPlayerPref)
            {
                PlayerPrefs.SetFloat(_prefPrefix + volumeControl.AudioName, volume);
            }
            Mixer.SetFloat(volumeControl.AudioName, Mathf.Log(volume) * 20f);
            volumeControl.AudioVolume = volume;
        }
    }
}
