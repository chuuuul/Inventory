using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GetConsumItem : MonoBehaviour
{

    [Header("String")]
    [Tooltip(" 아이템 비교 변수. 이 값에 따라 가져오는 아이템이 달라진다. ")]
    public string itemName;

    [Header("int")]
    [Tooltip(" 아이템 획득량 결정 ")]
    public Vector2 dropAmountRange = new Vector2(1,1);

    private Consum itemFromData;

    private void Start()
    {
        itemFromData = FindWithItemData(itemName);
    }

    // 아이템 이름이 일치하는 데이터를 불러옴
    private Consum FindWithItemData( string itemName )
    {
        for (int i = 0; i < ItemData.consumItemList.Count; i++)
            if (ItemData.consumItemList[i].Name == itemName)
                return ItemData.consumItemList[i];

        Debug.Log("  ItemData의 Consum 분류에서 아이템을 찾을 수 없습니다. ItemName : " + itemName);
        return null;
    }

    //충동시 아이템 획득
    private void OnTriggerEnter(Collider other)
    {
        // 드랍될 해당 아이템 수 결정
        int dropCount = UnityEngine.Random.Range((int)dropAmountRange.x, (int)dropAmountRange.y + 1);

        Consum item = new Consum(dropCount, itemFromData.MaxCount, itemFromData.BuyPrice, itemFromData.SellPrice, itemFromData.Id, itemFromData.Name, itemFromData.DisplayName, itemFromData.Description, itemFromData.Type, itemFromData.Icon);
        

        InventoryTab inventoryTab = TabManager.GetTab(0);
        inventoryTab.Add(item, true);

        SlotManager.RefreshAll();           //#### 아이템 먹을때는 인벤토리 켜져있을때만 새로고침하면됨
        Destroy(this.gameObject);

    }
    


}
