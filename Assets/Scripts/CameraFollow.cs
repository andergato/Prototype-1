using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target to Follow")]
    public Transform target; // Drag your main Car GameObject here

    [Header("Offset Settings")]
    public Vector3 offset = new Vector3(0f, 3f, -6f); // Position relative to the car

    [Header("Smoothness")]
    public float moveSmoothness = 5f;
    public float rotateSmoothness = 5f;

    private void FixedUpdate()
    {
        if (target == null) return;

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        // Calculate the target position based on the car's orientation and offset
        Vector3 targetPosition = target.TransformPoint(offset);

        // Smoothly interpolate the camera's position to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSmoothness * Time.deltaTime);
    }

    private void HandleRotation()
    {
        // Calculate the direction from the camera to the car
        Vector3 direction = target.position - transform.position;

        // Generate a rotation looking in that direction
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Smoothly interpolate the camera's rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSmoothness * Time.deltaTime);
    }
}
