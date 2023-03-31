using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Volume
{
    public string name;
    public float volume = 1f;
    public float tempVolume = 1f;
}

public class Settings
{
    public static Profiles profile;
}
[CreateAssetMenu(menuName = "Data/Create Profile")]
public class Profiles : ScriptableObject
{
    [SerializeField] public AudioMixer audioMixer;
    [SerializeField] private Volume[] volumeControl;

    public bool saveInPlayerPref = true;
    public string prefPrefix = "Settings_";
    
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
            if (volumeControl[i].name == name)
            {
                if (saveInPlayerPref)
                {
                    if (PlayerPrefs.HasKey(prefPrefix + volumeControl[i].name))
                    {
                        volumeControl[i].volume = PlayerPrefs.GetFloat(prefPrefix + volumeControl[i].name);
                    }
                }
                //resets the audio volume
                volumeControl[i].tempVolume = volumeControl[i].volume;

                //sets the mixer to match the volume
                if (audioMixer)
                {
                    audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volumeControl[i].volume) * 20f);
                }
                volume = volumeControl[i].volume;
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
                if (saveInPlayerPref)
                {
                    if (PlayerPrefs.HasKey(prefPrefix + volumeControl[i].name))
                    {
                        volumeControl[i].volume = PlayerPrefs.GetFloat(prefPrefix + volumeControl[i].name);
                    }
                }
                //resets the audio volume
                volumeControl[i].tempVolume = volumeControl[i].volume;

                //sets the mixer to match the volume
                audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volumeControl[i].volume) * 20f);
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
                if (volumeControl[i].name == name)
                {
                    audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volume) * 20f);
                    volumeControl[i].tempVolume = volume;
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
                volume = volumeControl[i].tempVolume;
                if (saveInPlayerPref)
                {
                    PlayerPrefs.SetFloat(prefPrefix + volumeControl[i].name, volume);
                }
                audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volume) * 20f);
                volumeControl[i].volume = volume;
            }
        }
    }
}
