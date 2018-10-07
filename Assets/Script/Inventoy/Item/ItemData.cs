using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;

namespace Item
{
    public class ItemData : MonoBehaviour
    {
        // Count : 개수 / maxCount : 최대개수 / buyPrice : 구매가격 / sellPrice : 판매가격
        // id : 아이템 찾을때 인덱서로 사용하는 아이템 아이디 (아주 가끔?? 사용)
        // name : 실질적으로 같은 아이템인지 비교할때 사용하는 이름
        // display naem : 화면표시에 사용하는 아이템 이름
        // description : 아이템 설명
        // type : 아이템 타입, 특정슬롯에 타입이 맞는 아이템만 들어갈수 있게끔 (type : weapon / shoes
        public static List<Consum> consumItemList = new List<Consum>();
        public static List<Equipment> equipmentItemList = new List<Equipment>();
        public static List<CommonItem> commonItemList = new List<CommonItem>();
        public static List<ImmediateItem> immediateItemList = new List<ImmediateItem>();

        public List<Sprite> consumSpriteList = new List<Sprite>();
        public List<Sprite> equipmentSpriteList = new List<Sprite>();
        public List<Sprite> commonItemSpriteList = new List<Sprite>();

        // targetList 0 : weapon  / 1 : shoulder / 2 : potion /
        public List<InventorySlot> targetSlotList = new List<InventorySlot>();  // type을 설정할 타겟슬롯
     

        private void Awake()
        {
        
            RegisterConsumItem();       // 소모템
            RegisterEquipmentItem();    // 장비템
            RegisterCommonItem();       // 일반템
            RegisterImmediateItem();
        }

        private void RegisterConsumItem()
        {
            Consum hp1 = new Consum(1, 5, 100, 90, "1001", "hp1", "초급 체력포션", "hp +50", "potion",true, consumSpriteList[0], null);
            Consum mp1 = new Consum(1, 5, 100, 90, "1002", "mp1", "초급 마나포션", "mp +50", "potion",true, consumSpriteList[1], null);

            consumItemList.Add(hp1);
            consumItemList.Add(mp1);
        }

        private void RegisterEquipmentItem()
        {

            Equipment sword1 = new Equipment(200, 150, "2001", "sword1", "도란검", "싸고 효율좋은 도란검", "weapon"
                , true, equipmentSpriteList[0], targetSlotList[0], ItemEffect.UseItemFunction );

            Equipment bow1 = new Equipment(220, 170, "2003", "bow1", "활활타오르는활활", "화살은 별도입니다", "weapon"
                , true, equipmentSpriteList[2], targetSlotList[0], ItemEffect.UseItemFunction);

            Equipment shoulder1 = new Equipment(210, 160, "2002", "shoulder1", "어깨빵", "싸고 효율좋은 어깨빵", "shoulder"
                , true, equipmentSpriteList[1], targetSlotList[1], ItemEffect.UseItemFunction);

            equipmentItemList.Add(sword1);
            equipmentItemList.Add(bow1);
            equipmentItemList.Add(shoulder1);
        }


        private void RegisterCommonItem()
        {
            CommonItem iron1 = new CommonItem(1, 3, 20, 10, "3001", "iron1", "철철김철강철", "고급인력이다", "material",true, commonItemSpriteList[0],null);
            CommonItem paper1 = new CommonItem(1, 3, 30, 15, "3002", "paper1", "컨닝페이퍼", " A+ or F", "material",true, commonItemSpriteList[1],null);

            commonItemList.Add(iron1);
            commonItemList.Add(paper1);
        }

        private void RegisterImmediateItem()
        {
            ImmediateItem moneyUp1 = new ImmediateItem("4001", "MoneyUp1", "돈", "돈이 오른다", "Money을 획득한다", true, ItemEffect.MoneyUp);
            ImmediateItem sizeUp1 = new ImmediateItem("4001", "sizeUp1", "하급 사이즈 업", "Size가 커진다 ", "Size", true, ItemEffect.SizeUp);

            immediateItemList.Add(moneyUp1);
            immediateItemList.Add(sizeUp1);

        }



///////////////////////////////////////////////// 아이템 복사 //////////////////////////////////////////////


        // 깊은 복사 메서드
        // 인덱스로 찾기와 이름이로 찾기 2가지 오버로딩
        public static SlotItem CommonItemClone(int index)
        {
            CommonItem newItem = new CommonItem(commonItemList[index].Count, commonItemList[index].MaxCount
                , commonItemList[index].BuyPrice, commonItemList[index].SellPrice, commonItemList[index].Id
                , commonItemList[index].Name, commonItemList[index].DisplayName, commonItemList[index].Description
                , commonItemList[index].Type, commonItemList[index].Usable, commonItemList[index].Icon, commonItemList[index].UseEvent);

            return newItem;
        }

        public static SlotItem CommonItemClone(string findWithName)
        {
            int index = -1;


            for (int i = 0; i < commonItemList.Count; i++)
                if (commonItemList[i].Name == findWithName)
                    index = i;

            if (index == -1)
            {
                Debug.Log("list에서 이름으로 찾지 못했습니다");
                return null;
            }

            return CommonItemClone(index);
        }

        public static SlotItem ConsumItemClone(int index)
        {
            Consum newItem = new Consum(consumItemList[index].Count, consumItemList[index].MaxCount, consumItemList[index].BuyPrice
                , consumItemList[index].SellPrice, consumItemList[index].Id, consumItemList[index].Name
                , consumItemList[index].DisplayName, consumItemList[index].Description, consumItemList[index].Type
                , consumItemList[index].Usable, consumItemList[index].Icon, consumItemList[index].UseEvent);

            return newItem;
        }

        public static SlotItem ConsumItemClone(string findWithName)
        {
            int index = -1;

            for (int i = 0; i < consumItemList.Count; i++)
                if (consumItemList[i].Name == findWithName)
                    index = i;


            if (index == -1)
            {
                Debug.Log("list에서 이름으로 찾지 못했습니다");
                return null;
            }
            return ConsumItemClone(index);
        }


        public static SlotItem EquipmentItemClone(int index)
        {
            Equipment newItem = new Equipment(equipmentItemList[index].BuyPrice, equipmentItemList[index].SellPrice
                , equipmentItemList[index].Id, equipmentItemList[index].Name, equipmentItemList[index].DisplayName
                , equipmentItemList[index].Description, equipmentItemList[index].Type, equipmentItemList[index].Usable
                , equipmentItemList[index].Icon, equipmentItemList[index].TargetSlot, equipmentItemList[index].UseEvent );

            return newItem;
        }

        //인덱스 쓰는게 더빠름
        public static SlotItem EquipmentItemClone(string findWithName)
        {
            int index = -1;


            for (int i = 0; i < equipmentItemList.Count; i++)
                if (equipmentItemList[i].Name == findWithName)
                    index = i;

            if (index == -1)
            {
                Debug.Log("list에서 이름으로 찾지 못했습니다");
                return null;
            }

            return EquipmentItemClone(index);
        }

        public static ImmediateItem ImmediateClone(int index)
        {
            ImmediateItem newItem = new ImmediateItem(immediateItemList[index].Id
                , immediateItemList[index].Name, immediateItemList[index].DisplayName, immediateItemList[index].Description
                , immediateItemList[index].Type, immediateItemList[index].Usable, immediateItemList[index].UseEvent);

            return newItem;

        }

        public static ImmediateItem ImmediateClone(string findWithName)
        {
            int index = -1;


            for (int i = 0; i < immediateItemList.Count; i++)
                if (immediateItemList[i].Name == findWithName)
                    index = i;

            return ImmediateClone(index);
        }

    }
}