using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public GameObject itemData;
    public bool isPickup = true; // Logic for if an item is able to be picked up or not. An item is not a pickup after it is dropped off. 
}
