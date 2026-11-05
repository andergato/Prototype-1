using UnityEngine;

public class InstantTeleport : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform destination;       // The target location transform

    [Header("Trigger Tag Filter")]
    [Tooltip("Only objects with this tag will be teleported.")]
    public string triggerTag = "Player"; // Tag required to trigger teleport

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the entering object has the required tag
        if (other.CompareTag(triggerTag))
        {
            Rigidbody rb = other.transform.parent.gameObject.GetComponent<Rigidbody>();
            Debug.Log(rb.name);
            TeleportObject(rb);
        }
    }

    private void TeleportObject(Rigidbody rb)
    {
        if (destination == null)
        {
            Debug.LogWarning("Teleport destination is not assigned on " + gameObject.name);
            return;
        }

        // Disable CharacterController temporarily if present to prevent position overrides
        CharacterController cc = rb.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        // Instantly move the object to the destination
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = destination.rotation;
        rb.position = destination.position;
        
        if (cc != null) cc.enabled = true;

        Debug.Log(rb.name + " instantly teleported to " + destination.name);
    }
}
