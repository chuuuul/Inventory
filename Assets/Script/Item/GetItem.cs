using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GetItem : MonoBehaviour
{
    [Header("Int")]

    [Tooltip("슬롯 한칸당 소지개수")]
    public int maxCount = 3;

    [Tooltip("드랍되는 개수 범위")]
    public Vector2 dropAmountRange;

    

    [Header("String")]
    public string id;
    public string itemName;
    public string type;
    public string description;

    public Sprite icon;

    private InventoryTab inventoryTab;

    

    //충동시 아이템 획득
    private void OnTriggerEnter(Collider other)
    {

        int count = UnityEngine.Random.Range((int)dropAmountRange.x, (int)dropAmountRange.y + 1);
        Destroy(this.gameObject);
        //Consumable getItem = new Consumable(maxCount, count, id, itemName, type, description, icon);

        SlotItem getItem = new SlotItem();

        getItem.SetCount(maxCount,count);
        getItem.SetProperty(id, itemName, type, description);
        getItem.Icon = icon;

        inventoryTab = TabManager.GetTab("FirstTab");
        inventoryTab.Add(getItem ,true);
        SlotManager.RefreshAll();


    }

}
