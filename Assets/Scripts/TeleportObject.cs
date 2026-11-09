using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform destination;       // The target location transform

    [Header("Trigger Tag Filter")]
    [Tooltip("Only objects with this tag will be teleported.")]
    public string triggerTag = "Player"; // Tag required to trigger teleport

    // Instance of countdown timer
    public CountdownTimer timer;

    // Has timer completed yet
    bool timerFire = false;

    // Rigidbody to teleport
    private Rigidbody rb;

    private void Update()
    {
        // Only teleport once timerFire is true
        if (timerFire)
        {
            Teleport(rb);
        }
    }
    private void OnEnable()
    {
        // Subscribe to CountdownTimer's TimerDone event and fire FireTimer when fired.
        CountdownTimer.TimerDone += FireTimer;
    }

    // Tells us timer is complete
    private void FireTimer()
    {
        timerFire = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the entering object has the required tag
        if (other.CompareTag(triggerTag))
        {
            rb = other.transform.parent.gameObject.GetComponent<Rigidbody>();
            Debug.Log(rb.name);
            timer.StartTimer();
        }
    }

    // Teleport Rigidbody to destination
    private void Teleport(Rigidbody rb)
    {
        // Check if destination transform is valid
        if (destination == null)
        {
            Debug.LogWarning("Teleport destination is not assigned on " + gameObject.name);
            return;
        }

        if(rb == null)
        {
            Debug.LogWarning("Invalid Rigidbody");
            return;
        }

        // Disable CharacterController temporarily if present to prevent position overrides
        CharacterController cc = rb.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        // Teleport rigidbody to destination
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = destination.rotation;
        rb.position = destination.position;
        
        // re-enable CharacterController
        if (cc != null) cc.enabled = true;

        Debug.Log(rb.name + " teleported to " + destination.name);
        timerFire = false;
    }
}
