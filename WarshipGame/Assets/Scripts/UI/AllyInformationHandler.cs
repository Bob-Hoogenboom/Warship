using UnityEngine;
using UnityEngine.UI;

public class AllyInformationHandler : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image CurrentProfilePicture;
    [SerializeField] private Sprite[] nextProfileImage;
    [SerializeField] private Ship activeShip;
    
    private Slider _selectedShipSlider;

    private ShipManager _shipManager;
    public int currentImage;

    void Awake()
    {
        _shipManager = FindObjectOfType<ShipManager>();
    }

    public void GetSelectedShip()
    {
        if (_shipManager.SelectedShip == null) return;
        
        activeShip = _shipManager.SelectedShip;
        healthSlider.maxValue = activeShip.GetComponent<Ship>().HealthBar.maxValue;
        healthSlider.value = activeShip.GetComponent<Ship>().HealthBar.value;
        HandleProfileImage();
    }

    private void HandleProfileImage()
    {
        currentImage = activeShip.GetComponent<Ship>().profileTag;
        CurrentProfilePicture.sprite = nextProfileImage[currentImage];
    }
}
