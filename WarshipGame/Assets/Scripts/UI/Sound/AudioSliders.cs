using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioSliders : MonoBehaviour
{
    Slider slider
    {
        get { return GetComponent<Slider>(); }
    }

    [Header("Volume Name")] [Tooltip("This is the name of the exposed parameter")] 
    [SerializeField] private string volumeName;
    
    [Header("Volume Label")] 
    [SerializeField] private TextMeshProUGUI volumeLabel;

    void Start()
    {
        ResetSliderValue();
        slider.onValueChanged.AddListener(delegate
        {
            UpdateValueOnChange(slider.value);
        });
    }

    public void UpdateValueOnChange(float value)
    {
        if (volumeLabel != null)
        {
            volumeLabel.text = Mathf.Round(value * 100.0f) + "%";
        }

        if (Settings.profile)
        {
            Settings.profile.SetAudioLevels(volumeName, value);
        }
    }

    public void ResetSliderValue()
    {
        if (Settings.profile)
        {
            float volume = Settings.profile.GetAudioLevels(volumeName);
            
            UpdateValueOnChange(volume);
            slider.value = volume;
        }
    }
}
