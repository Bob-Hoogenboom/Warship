using UnityEngine;
using UnityEngine.UI;

public class AllyInformationUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image BoatProfilePicture;
    [SerializeField] private Ship activeShip;
    [SerializeField] private Slider _selectedShipSlider;
    
    private ShipManager _shipManager;

    void Awake()
    {
        _shipManager = FindObjectOfType<ShipManager>();

    }

    public void GetSelectedShip()
    {
        if (_shipManager.selectedShip == null) return;
        
        activeShip = _shipManager.selectedShip;
        healthSlider.value = _shipManager.selectedShip.HealthBar.value;
    }  
}
