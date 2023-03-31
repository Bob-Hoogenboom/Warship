using UnityEngine;
using UnityEngine.Serialization;

public class EnemyFire : MonoBehaviour
{
    [FormerlySerializedAs("ShipToFireAt")] public GameObject shipToFireAt;// { get; set; }
    
    public void Fire()
    {
        Debug.Log(shipToFireAt);
    }
}
