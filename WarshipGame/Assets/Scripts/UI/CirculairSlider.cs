using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CirculairSlider : MonoBehaviour
{
    [SerializeField] private Image bar;
    [SerializeField] private RectTransform button;
    public float _sliderValue;

    void Update()
    {
        ValueChange(_sliderValue);
    }

    void ValueChange(float sliderValue)
    {
        float amount = (sliderValue / 100.0f) * 180.0f / 360;
        bar.fillAmount = amount;
        float buttonAngle = amount * 360;
        button.localEulerAngles = new Vector3(0, 0, -buttonAngle);
    }
}
