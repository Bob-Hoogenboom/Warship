using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioSliders : MonoBehaviour
{
    private Slider _slider => GetComponent<Slider>();

    [Tooltip("This is the name of the exposed parameter")] 
    [SerializeField] private string volumeName;
    
    [SerializeField] private TextMeshProUGUI volumeLabel;

    private void Start()
    {
        ResetSliderValue();
        _slider.onValueChanged.AddListener(delegate
        {
            UpdateValueOnChange(_slider.value);
        });
    }

    private void UpdateValueOnChange(float value)
    {
        if (volumeLabel == null) return;
        volumeLabel.text = Mathf.Round(value * 100.0f) + "%";

        if (!Settings.profile) return;
        Settings.profile.SetAudioLevels(volumeName, value);
    }

    /// <summary>
    /// Function can be assigned to a button to reset all slider to its original value
    /// </summary>
    public void ResetSliderValue()
    {
        if (!Settings.profile) return;
        float volume = Settings.profile.GetAudioLevels(volumeName);
        UpdateValueOnChange(volume);
        _slider.value = volume;
    }
}
