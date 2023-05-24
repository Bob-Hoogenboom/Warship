using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInformation : MonoBehaviour
{ 
    [SerializeField] private Ship _enemyShip;
    private Image enemyPictures;

    private void Awake()
    {
        enemyPictures = GetComponent<Image>();
        enemyPictures.sprite = _enemyShip.GetComponent<Ship>().profileTag;;
    }

    private void Update()
    {
        
    }
}

