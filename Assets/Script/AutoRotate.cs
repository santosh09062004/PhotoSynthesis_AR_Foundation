using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 60f; // degrees per second
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
    }
}