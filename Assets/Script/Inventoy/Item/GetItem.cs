using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetItem : MonoBehaviour
{

    
    public enum MajorTypeEnum { none, commonItem, consum, equipment,  }
    [Header("대분류 설정")]
    public MajorTypeEnum majorType;

    [Header("String")]
    [Tooltip(" 아이템 비교 변수. 이 값에 따라 가져오는 아이템이 달라진다. ")]
    public string itemName;



    [Header("int")]
    [Tooltip(" 아이템 획득량 결정 ")]
    public Vector2 dropAmountRange = new Vector2(1, 1);


    private CommonItem commonItem;
    private Consum consumItem;
    private Equipment equipmentItem;

    private void Start()
    {
        
        FindItemWithItemName();

    }

    private void ErrorCheck()
    {
        if (majorType == MajorTypeEnum.none)
        {
            Debug.Log(this.gameObject.name);
            Debug.Log("대분류를 선택하세요");
            return;
        }

        if (itemName == null)
        {

            Debug.Log(this.gameObject.name);
            Debug.Log("아이템 이름을 입력하세요");
            return;
        }
    }
    private void FindItemWithItemName()
    {
        ErrorCheck();

        if (majorType == MajorTypeEnum.commonItem )
        {
            for (int i = 0; i < ItemData.commonItemList.Count; i++)
            { 
                if (ItemData.commonItemList[i].Name == itemName)
                {
                    commonItem = ItemData.commonItemList[i];
                    return;
                }
            }

            Debug.Log("CommonItemList에 해당하는 아이템이 없습니다. 아이템 이름을 확인하세요" + itemName);
            return;
        }

        if (majorType == MajorTypeEnum.consum)
        {
            for (int i = 0; i < ItemData.consumItemList.Count; i++)
            {
                if (ItemData.consumItemList[i].Name == itemName)
                {
                    consumItem = ItemData.consumItemList[i];
                    return;
                }
            }
            Debug.Log("ComsumItemList에 해당하는 아이템이 없습니다. 아이템 이름을 확인하세요" +itemName);
            return;
        }


        if (majorType == MajorTypeEnum.equipment)
        {
            for (int i = 0; i < ItemData.equipmentItemList.Count; i++)
            { 
                if (ItemData.equipmentItemList[i].Name == itemName)
                { 
                    equipmentItem = ItemData.equipmentItemList[i];
                    return;
                }
            }
            Debug.Log("EquipmentItemList 해당하는 아이템이 없습니다. 아이템 이름을 확인하세요" + itemName);
            return;
        }

    }


    //충동시 아이템 획득
    private void OnTriggerEnter(Collider other)
    {
        InventoryTab inventoryTab = null ;

        ErrorCheck();

        if (majorType == MajorTypeEnum.commonItem)
        {

            // 드랍될 해당 아이템 수 결정
            int dropCount = UnityEngine.Random.Range((int)dropAmountRange.x, (int)dropAmountRange.y + 1);
            // 깊은 복사 & 수정
            CommonItem item = new CommonItem(dropCount, commonItem.MaxCount, commonItem.BuyPrice, commonItem.SellPrice, commonItem.Id, commonItem.Name, commonItem.DisplayName, commonItem.Description, commonItem.Type, commonItem.Icon);

            //아이템이 먹어지는 탭 지정
            inventoryTab = TabManager.GetTab(0);
            inventoryTab.Add(item, true);
        }

        else if (majorType == MajorTypeEnum.consum)
        {

            // 드랍될 해당 아이템 수 결정
            int dropCount = UnityEngine.Random.Range((int)dropAmountRange.x, (int)dropAmountRange.y + 1);
            // 깊은 복사 & 수정
            Consum item = new Consum(dropCount, consumItem.MaxCount, consumItem.BuyPrice, consumItem.SellPrice, consumItem.Id, consumItem.Name, consumItem.DisplayName, consumItem.Description, consumItem.Type, consumItem.Icon);
            //아이템이 먹어지는 탭 지정
            inventoryTab = TabManager.GetTab(0);
            inventoryTab.Add(item, true);
        }
        else if (majorType == MajorTypeEnum.equipment)
        {
            // 깊은 복사 & 수정
            Equipment item = new Equipment(equipmentItem.BuyPrice, equipmentItem.SellPrice, equipmentItem.Id, equipmentItem.Name, equipmentItem.DisplayName, equipmentItem.Description, equipmentItem.Type, equipmentItem.Icon);

            //아이템이 먹어지는 탭 지정
            inventoryTab = TabManager.GetTab(0);
            inventoryTab.Add(item, true);
        }

        SlotManager.RefreshAll();           //#### 아이템 먹을때는 인벤토리 켜져있을때만 새로고침하면됨
        Destroy(this.gameObject);

    }
}
