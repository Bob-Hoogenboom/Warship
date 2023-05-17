using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 25f;

    private void Update()
    {
        transform.Rotate(1f * Time.deltaTime * rotationSpeed, 0f, 0f, Space.World);
    }
}
