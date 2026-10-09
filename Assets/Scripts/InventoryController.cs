using System;
using UnityEditor.Rendering;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public Inventory inventory; // Player's current inventory, operates as a stack so each new item picked up will be dropped first. 
    public static event Action OnItemDrop; // Event to let the PlayerController know that an item has been dropped off.

    private void OnTriggerEnter(Collider other)
    {
        // When an item has not been picked up yet, as the user moves over it, it will disappear and go to the top of the player's inventory.
        if (other.gameObject.CompareTag("ItemPickup"))
        {
            PickupItem pickup = other.GetComponent<PickupItem>();

            if (pickup != null && pickup.isPickup)
            {
                inventory.AddItem(pickup);
                other.gameObject.SetActive(false);
            }
        }

        // When the player moves over an item drop location and the player's inventory is not empty, the item will be popped out of the inventory and float above the drop location.
        // The player's score will also be increased. 
        if (other.gameObject.CompareTag("ItemDrop"))
        {
            Debug.Log("Item drop passed over");
            PickupItem res = inventory.RemoveItem();
            if (res)
            {
                res.isPickup = false;
                OnItemDrop.Invoke();
                SpawnItem(res, other);
            }
        }
    }

    // Spawn item above designated collider
    private void SpawnItem(PickupItem pickup, Collider other)
    {
        Bounds bounds = other.bounds;
        Vector3 spawnPosition = new Vector3(bounds.center.x, bounds.max.y, bounds.center.z);
        float halfHeight = bounds.extents.y;

        pickup.transform.position = spawnPosition;
        pickup.transform.position += new Vector3(0, halfHeight, 0);
        pickup.gameObject.SetActive(true);
    }
}
