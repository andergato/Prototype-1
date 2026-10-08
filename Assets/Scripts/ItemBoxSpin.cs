using UnityEngine;

public class ItemBoxSpin : MonoBehaviour
{
    [Tooltip("Speed of the rotation around the Y-axis.")]
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotate the item box around its Y-axis continuously and smoothly across frame rates
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
