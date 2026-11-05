using UnityEngine;

public class AntiRollBars : MonoBehaviour
{
    public WheelCollider wheelFrontLeft;
    public WheelCollider wheelFrontRight;
    public WheelCollider wheelRearLeft;
    public WheelCollider wheelRearRight;
    
    public float antiRollForce = 5000f; // Adjust this to control body roll

    void FixedUpdate()
    {
        ApplyAntiRoll(wheelFrontLeft, wheelFrontRight);
        ApplyAntiRoll(wheelRearLeft, wheelRearRight);
    }

    void ApplyAntiRoll(WheelCollider wheelLeft, WheelCollider wheelRight)
    {
        WheelHit hit;
        float travelLeft = 1.0f;
        float travelRight = 1.0f;

        bool groundedLeft = wheelLeft.GetGroundHit(out hit);
        if (groundedLeft)
            travelLeft = (-wheelLeft.transform.InverseTransformPoint(hit.point).y - wheelLeft.radius) / wheelLeft.suspensionDistance;

        bool groundedRight = wheelRight.GetGroundHit(out hit);
        if (groundedRight)
            travelRight = (-wheelRight.transform.InverseTransformPoint(hit.point).y - wheelRight.radius) / wheelRight.suspensionDistance;

        float antiRollVal = (travelLeft - travelRight) * antiRollForce;

        if (groundedLeft)
            GetComponent<Rigidbody>().AddForceAtPosition(wheelLeft.transform.up * -antiRollVal, wheelLeft.transform.position);
        if (groundedRight)
            GetComponent<Rigidbody>().AddForceAtPosition(wheelRight.transform.up * antiRollVal, wheelRight.transform.position);
    }
}
