using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, int> items = new Dictionary<string, int>();

    public void CollectItem(string item, int amount)
    {
        if(items.ContainsKey(item))
        {
            items[item] += amount;
        }   
        else
        {
            items.Add(item, amount);
        }

        Debug.Log(items.Count);
    }
}
