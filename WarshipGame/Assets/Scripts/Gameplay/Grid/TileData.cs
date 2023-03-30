using System;
using UnityEngine;

public class TileData : MonoBehaviour
{
    [SerializeField] private Color occupied = Color.red;
    [SerializeField] private Color free = Color.green;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.gameObject.CompareTag("Ship"))
        {
            gameObject.GetComponent<Renderer>().material.color = free;
            return;
        }
        
        gameObject.GetComponent<Renderer>().material.color = occupied;
    }
}
