using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryModule : MonoBehaviour
{
    Dictionary<string, Item> inventory = new Dictionary<string, Item>();
    
    public void GetItme(Item getItem)
    {
        inventory.Add(getItem.key, getItem);
    }

}


