using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInformation : MonoBehaviour
{
    [SerializeField] private List<GameObject> activeEnemies = new List<GameObject>();
    [SerializeField] private List<Image> enemyPictures = new List<Image>();
    private ShipManager _shipManager;

    private void Awake()
    {
        _shipManager = FindObjectOfType<ShipManager>();
    }

    public void GetActiveEnemies()
    {
        if (_shipManager.SelectedPlayerShip == null) return;
        
        Ship enemyShipsObjects = _shipManager.GetComponent<Ship>();
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            enemyPictures[i].sprite = GetComponent<Ship>().profileTag;
        }
    }
}

