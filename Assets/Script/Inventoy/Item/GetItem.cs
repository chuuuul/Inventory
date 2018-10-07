using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

namespace Item
{
    public class GetItem : MonoBehaviour
    {


        public enum MajorTypeEnum { none, CommonItem, ConsumItem, EquipmentItem, ImmediateItem }
        [Header("대분류 설정")]
        public MajorTypeEnum majorType;

        [Header("String")]
        [Tooltip(" 아이템 비교 변수. 이 값에 따라 가져오는 아이템이 달라진다. ")]
        public string itemName;

        [Header("int")]
        [Tooltip(" 아이템 획득량 결정 (Random 돌릴 범위) ")]
        public Vector2 dropAmountRange = new Vector2(1, 1);


        private int index = 0;

        private void Start()
        {
            if (itemName == null)
                Debug.Log("아이템 이름을 입력하세요 : gameObject.name");
            else index = FindItemWithItemName();
        }

        // return 아이템의 인덱스 ( 에러시 -1 )
        private int FindItemWithItemName()
        {
            switch (majorType)
            {
                case MajorTypeEnum.CommonItem:
                    
                    for (int i = 0; i < ItemData.commonItemList.Count; i++)
                        if (ItemData.commonItemList[i].Name == itemName)
                            return i;

                    Debug.Log("CommonItemList에 해당하는 아이템이 없습니다. 아이템 이름을 확인하세요" + itemName);
                    return -1;

                case MajorTypeEnum.ConsumItem:
                    for (int i = 0; i < ItemData.consumItemList.Count; i++)
                        if (ItemData.consumItemList[i].Name == itemName)
                            return i;

                    Debug.Log("ComsumItemList에 해당하는 아이템이 없습니다. 아이템 이름을 확인하세요" + itemName);
                    return -1;


                case MajorTypeEnum.EquipmentItem:
                    
                    for (int i = 0; i < ItemData.equipmentItemList.Count; i++)
                        if (ItemData.equipmentItemList[i].Name == itemName)
                            return i;

                    Debug.Log("EquipmentItemList 해당하는 아이템이 없습니다. 아이템 이름을 확인하세요" + itemName);
                    return -1;

                case MajorTypeEnum.ImmediateItem:
                    for (int i = 0; i < ItemData.immediateItemList.Count; i++)
                        if (ItemData.immediateItemList[i].Name == itemName)
                            return i;
                    Debug.Log("ImmediateItemList 해당하는 아이템이 없습니다. 아이템 이름을 확인하세요" + itemName);
                    return -1;

                default:
                    Debug.Log("대분류를 선택하세요. ItemName : " + this.gameObject.name);
                    return -1;
            }
        }

        //충동시 아이템 획득
        private void OnTriggerEnter(Collider other)
        {
            InventoryTab inventoryTab = null;

            switch (majorType)
            {
                case MajorTypeEnum.CommonItem:
                    {
                        // 깊은 복사 & 수정
                        SlotItem item = ItemData.CommonItemClone(index);

                        int dropCount = UnityEngine.Random.Range((int)dropAmountRange.x, (int)dropAmountRange.y + 1);
                        item.Count = dropCount;
                    
                        //아이템이 먹어지는 탭 지정
                        inventoryTab = TabManager.GetTab(0);
                        inventoryTab.Add(item, true);
                        break;
                    }

                case MajorTypeEnum.ConsumItem:
                    {
                        SlotItem item = ItemData.ConsumItemClone(index);

                        int dropCount = UnityEngine.Random.Range((int)dropAmountRange.x, (int)dropAmountRange.y + 1);
                        item.Count = dropCount;

                        inventoryTab = TabManager.GetTab(0);
                        inventoryTab.Add(item, true);
                        break;
                    }
                case MajorTypeEnum.EquipmentItem:
                    {
                        SlotItem item = ItemData.EquipmentItemClone(index);

                        inventoryTab = TabManager.GetTab(0);
                        inventoryTab.Add(item, true);
                        break;
                    }
                case MajorTypeEnum.ImmediateItem:
                    {
                        int dropAmount = UnityEngine.Random.Range((int)dropAmountRange.x, (int)dropAmountRange.y + 1);
                        ImmediateItem item = ItemData.ImmediateClone(index);
                        item.UseEvent.Invoke(dropAmount);
                        break;
                    }
            }
        
            SlotManager.RefreshAll();           //#### 아이템 먹을때는 인벤토리 켜져있을때만 새로고침하면됨
            Destroy(this.gameObject);

        }
    }
}
