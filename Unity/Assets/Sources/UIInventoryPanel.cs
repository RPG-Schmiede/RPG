using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIInventoryPanel : MonoBehaviour
{
    public UIInventoryElement[] Slots;
    public InventoryManager inventoryManager;

    private void FixedUpdate()
    {
        int index = 0;

        foreach (KeyValuePair<string, int> item in inventoryManager.items)
        {
            if(Slots.Length-1 >= index)
            {
                Slots[index].UpdateSlot(item.Value, null);
            }

            ++index;
        }
    }
}
