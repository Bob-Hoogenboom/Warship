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
    [SerializeField] public AudioMixer audioMixer;
    [SerializeField] private Volume[] volumeControl;

    public bool SaveInPlayerPref = true;
    public string PrefPrefix = "Settings_";
    
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

        if (!audioMixer)
        {
            return volume;
        }
        
        for (int i = 0; i < volumeControl.Length; i++)
        {
            if (volumeControl[i].AudioName == name)
            {
                if (SaveInPlayerPref)
                {
                    if (PlayerPrefs.HasKey(PrefPrefix + volumeControl[i].AudioName))
                    {
                        volumeControl[i].AudioVolume = PlayerPrefs.GetFloat(PrefPrefix + volumeControl[i].AudioName);
                    }
                }
                //resets the audio volume
                volumeControl[i].TempVolume = volumeControl[i].AudioVolume;

                //sets the mixer to match the volume
                if (audioMixer)
                {
                    audioMixer.SetFloat(volumeControl[i].AudioName, Mathf.Log(volumeControl[i].AudioVolume) * 20f);
                }
                volume = volumeControl[i].AudioVolume;
                break;
            }
        }
        return volume;
    }

    /// <summary>
    /// Changes all the audio levels.
    /// </summary>
    public void GetAudioLevels()
    {
        if (audioMixer)
        {
            for (int i = 0; i < volumeControl.Length; i++)
            {
                if (SaveInPlayerPref)
                {
                    if (PlayerPrefs.HasKey(PrefPrefix + volumeControl[i].AudioName))
                    {
                        volumeControl[i].AudioVolume = PlayerPrefs.GetFloat(PrefPrefix + volumeControl[i].AudioName);
                    }
                }
                //resets the audio volume
                volumeControl[i].TempVolume = volumeControl[i].AudioVolume;

                //sets the mixer to match the volume
                audioMixer.SetFloat(volumeControl[i].AudioName, Mathf.Log(volumeControl[i].AudioVolume) * 20f);
            }
        }
    }

    /// <summary>
    /// Sets the audio level of the current scene with 'tempVolume' and changes it for the other scenes with 'volume'.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="volume"></param>
    public void SetAudioLevels(string name, float volume)
    {
        if (audioMixer)
        {
            for (int i = 0; i < volumeControl.Length; i++)
            {
                if (volumeControl[i].AudioName == name)
                {
                    audioMixer.SetFloat(volumeControl[i].AudioName, Mathf.Log(volume) * 20f);
                    volumeControl[i].TempVolume = volume;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Saves the updated values into the scriptable object.
    /// </summary>
    public void SaveAudioLevels()
    {
        if (audioMixer)
        {
            float volume = 0f;
            for (int i = 0; i < volumeControl.Length; i++)
            {
                volume = volumeControl[i].TempVolume;
                if (SaveInPlayerPref)
                {
                    PlayerPrefs.SetFloat(PrefPrefix + volumeControl[i].AudioName, volume);
                }
                audioMixer.SetFloat(volumeControl[i].AudioName, Mathf.Log(volume) * 20f);
                volumeControl[i].AudioVolume = volume;
            }
        }
    }
}
