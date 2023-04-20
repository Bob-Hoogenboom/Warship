using System;
using UnityEngine;
using UnityEngine.UI;

public class AttackInformation : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image BoatProfile;
    private Ship _shipManager;

    void Start()
    {
        _shipManager = gameObject.GetComponent<Ship>();

    }

    public void OnSelectedShip()
    {
        healthSlider.value = _shipManager.HealthBar.value;
    }
}
