using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioSliders : MonoBehaviour
{
    private Slider slider
    {
        get { return GetComponent<Slider>(); }
    }

    [Tooltip("This is the name of the exposed parameter")] 
    [SerializeField] private string volumeName;
    
    [SerializeField] private TextMeshProUGUI volumeLabel;

    private void Start()
    {
        ResetSliderValue();
        slider.onValueChanged.AddListener(delegate
        {
            UpdateValueOnChange(slider.value);
        });
    }

    public void UpdateValueOnChange(float value)
    {
        if (volumeLabel == null) return;
            volumeLabel.text = Mathf.Round(value * 100.0f) + "%";

        if (!Settings.profile) return;
            Settings.profile.SetAudioLevels(volumeName, value);
    }

    public void ResetSliderValue()
    {
        if (!Settings.profile) return;
            float volume = Settings.profile.GetAudioLevels(volumeName);
            UpdateValueOnChange(volume);
            slider.value = volume;
    }
}
