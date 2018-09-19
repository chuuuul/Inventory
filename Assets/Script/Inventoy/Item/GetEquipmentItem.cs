using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEquipmentItem : MonoBehaviour
{


    [Header("String")]
    [Tooltip(" 아이템 비교 변수. 이 값에 따라 가져오는 아이템이 달라진다. ")]
    public string itemName;

    private Equipment itemFromData;

    private void Start()
    {
        itemFromData = FindWithItemData(itemName);
    }

    // 아이템 이름이 일치하는 데이터를 불러옴
    private Equipment FindWithItemData(string itemName)
    {
        for (int i = 0; i < ItemData.equipmentItemList.Count; i++)
            if (ItemData.equipmentItemList[i].Name == itemName)
                return ItemData.equipmentItemList[i];

        Debug.Log("  ItemData의 Equipment 분류에서 아이템을 찾을 수 없습니다. ItemName : " + itemName);
        return null;
    }

    //충동시 아이템 획득
    private void OnTriggerEnter(Collider other)
    {
        // 깊은 복사 & 수정
        Equipment item = new Equipment(itemFromData.BuyPrice, itemFromData.SellPrice, itemFromData.Id, itemFromData.Name, itemFromData.DisplayName, itemFromData.Description, itemFromData.Type, itemFromData.Icon);

        //아이템이 먹어지는 탭 지정
        InventoryTab inventoryTab = TabManager.GetTab(0);
        inventoryTab.Add(item, true);

        SlotManager.RefreshAll();           //#### 아이템 먹을때는 인벤토리 켜져있을때만 새로고침하면됨
        Destroy(this.gameObject);

    }
}
