using UnityEngine;

public class HexTileChecker : MonoBehaviour
{
    private HexType _lastDetectedHexType;
    private void OnCollisionEnter(Collision hexTileCollision)
    {
        if (hexTileCollision.gameObject.layer != 4) return;

        HexData currentHex = hexTileCollision.gameObject.GetComponent<HexData>();

        _lastDetectedHexType = currentHex.HexType;
        currentHex.HexType = HexType.Occupied;
    }
    
    private void OnCollisionExit(Collision hexTileCollision)
    {
        if (hexTileCollision.gameObject.layer != 4) return;

        HexData currentHex = hexTileCollision.gameObject.GetComponent<HexData>();

        currentHex.HexType = _lastDetectedHexType;
    }
}
