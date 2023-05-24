using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    [Tooltip("The Direction and speed at which the rotation should update")]
    [SerializeField] private Vector3 rotationStep;

    private void Update()
    {
        transform.Rotate(rotationStep * Time.deltaTime , Space.World);
    }
}
