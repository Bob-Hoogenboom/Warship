using UnityEngine;
using UnityEngine.UI;

public class AllyInformationUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image BoatProfilePicture;
    private ShipManager _shipManager;

    void Awake()
    {
        _shipManager = GetComponent<ShipManager>();
    }

    public void OnSelectedShip(GameObject ship)
    {
        Ship shipReference = ship.GetComponent<Ship>();
        healthSlider.value = shipReference.HealthBar.value;
    }

    private void ShipReference(Ship shipReference)
    {
        
    }
}
