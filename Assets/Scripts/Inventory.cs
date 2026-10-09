using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    static Stack<PickupItem> storage = new Stack<PickupItem>();

    // Push item to top of stack.
    public void AddItem(PickupItem p)
    {
        storage.Push(p);
    }

    // Remove item from the stack if it exists. Return null if not. 
    public PickupItem RemoveItem()
    {
        if (storage.Count > 0)
        {

            return storage.Pop();
        }
        return null;
    }
}
