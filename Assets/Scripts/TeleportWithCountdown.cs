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
            TeleportObject(other.gameObject);
        }
    }

    private void TeleportObject(GameObject obj)
    {
        if (destination == null)
        {
            Debug.LogWarning("Teleport destination is not assigned on " + gameObject.name);
            return;
        }

        // Disable CharacterController temporarily if present to prevent position overrides
        CharacterController cc = obj.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        // Instantly move the object to the destination
        obj.transform.position = destination.position;
        
        // Optional: Match the target destination's rotation
        // obj.transform.rotation = destination.rotation;

        if (cc != null) cc.enabled = true;

        Debug.Log(obj.name + " instantly teleported to " + destination.name);
    }
}
