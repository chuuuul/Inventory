using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetItem : MonoBehaviour
{
    [Header("Int")]
    public int index;
    public int maxCount = 3;
    public int count = 1;

    [Header("String")]
    public string id;
    public string itemName;
    public string type;
    public string description;

    public Sprite icon;
    private InventoryTab inventoryTab;



    private void OnTriggerEnter(Collider other)
    {
        Consumable getItem = new Consumable(maxCount, count, id, itemName, type, description, icon);
        
        inventoryTab = TabManager.GetTab("ItemTable");
        inventoryTab.Add(getItem ,true);
        SlotManager.
        Destroy(this.gameObject);

    }

}
