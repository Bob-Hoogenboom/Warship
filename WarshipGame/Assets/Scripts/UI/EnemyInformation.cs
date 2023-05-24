using UnityEngine;
using UnityEngine.UI;

public class EnemyInformation : MonoBehaviour
{ 
    [SerializeField] private Ship _enemyShip;
    [SerializeField] private Image enemyBorder;
    [SerializeField] private Sprite deadShipBorder;
    private Image enemyPictures;

    private void Awake()
    {
        enemyPictures = GetComponent<Image>();
        enemyPictures.sprite = _enemyShip.GetComponent<Ship>().profileTag;
    }

    private void Update()
    {
        if (!_enemyShip.shipDead) return;
        enemyBorder.sprite = deadShipBorder;
    }
}

