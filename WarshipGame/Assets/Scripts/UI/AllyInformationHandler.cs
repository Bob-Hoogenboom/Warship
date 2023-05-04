using UnityEngine;
using UnityEngine.UI;

public class AllyInformationHandler : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image currentProfilePicture;

    private Slider _selectedShipSlider;
    private ShipManager _shipManager;

    private void Awake()
    {
        _shipManager = FindObjectOfType<ShipManager>();
    }

    public void GetSelectedShip()
    {
        if (_shipManager.SelectedShip == null) return;
        
        Ship activeShipScript = _shipManager.SelectedShip.GetComponent<Ship>();
        
        healthSlider.maxValue = activeShipScript.HealthBar.maxValue;
        healthSlider.value = activeShipScript.HealthBar.value;
        
        currentProfilePicture.sprite = activeShipScript.profileTag;
    }


}
