using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 15f;
    public float turnSpeed = 100f;

    private float moveInput;
    private float turnInput;

    [Header("Optional Visual Wheels (Axles)")]
    public Transform frontAxleTransform;
    public Transform rearAxleTransform;
    public float wheelRotationSpeed = 200f;

    void Update()
    {
        // 1. Get Keyboard/Controller Inputs (W/S and A/D)
        moveInput = Input.GetAxis("Vertical");     // Forward/Backward
        turnInput = Input.GetAxis("Horizontal");   // Left/Right

        // 2. Move the Car Forward/Backward
        transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);

        // 3. Rotate the Car Left/Right (Only allow turning if the car is moving)
        if (moveInput != 0)
        {
            // Reverse steering direction if backing up
            float steeringDirection = moveInput > 0 ? 1f : -1f;
            transform.Rotate(Vector3.up * turnInput * turnSpeed * steeringDirection * Time.deltaTime);
        }

        // 4. Spin the visual wheel axles if they are assigned
        RotateVisualWheels();

    }

    void RotateVisualWheels()
    {
        if (moveInput != 0)
        {
            float rotationAmount = moveInput * wheelRotationSpeed * Time.deltaTime;

            if (frontAxleTransform != null)
                frontAxleTransform.Rotate(Vector3.right * rotationAmount);

            if (rearAxleTransform != null)
                rearAxleTransform.Rotate(Vector3.right * rotationAmount);
        }
    }
}
