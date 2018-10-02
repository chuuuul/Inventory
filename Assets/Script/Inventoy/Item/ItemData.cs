using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public List<Sprite> consumSpriteList = new List<Sprite>();
    public List<Sprite> equipmentSpriteList = new List<Sprite>();
    public List<Sprite> commonItemSpriteList = new List<Sprite>();

    public List<InventorySlot> targetSlotList = new List<InventorySlot>();  // type을 설정할 타겟슬롯
    private void Awake()
    {
        
        RegisterConsumItem();       // 소모템
        RegisterEquipmentItem();    // 장비템
        RegisterCommonItem();       // 일반템
    }

    private void RegisterConsumItem()
    {
        Consum hp1 = new Consum(1, 5, 100, 90, "1001", "hp1", "초급 체력포션", "hp +50", "potion", consumSpriteList[0]);
        hp1.UseEvent += (item) => UseItemFunction(hp1);
        consumItemList.Add(hp1);

        Consum mp1 = new Consum(1, 5, 100, 90, "1002", "mp1", "초급 마나포션", "mp +50", "potion", consumSpriteList[1]);
        hp1.UseEvent += (item) => UseItemFunction(hp1);
        consumItemList.Add(mp1);
    }

    private void RegisterEquipmentItem()
    {
        Equipment sword1 = new Equipment(200, 150, "2001", "sword1", "도란검", "싸고 효율좋은 도란검", "weapon", equipmentSpriteList[0]);
        sword1.UseEvent += (item) => UseItemFunction(sword1);
        equipmentItemList.Add(sword1);

        Equipment shoulder1 = new Equipment(200, 150, "2002", "shoulder1", "어깨빵", "싸고 효율좋은 어깨빵", "shoulder", equipmentSpriteList[1]);
        shoulder1.UseEvent += (item) => UseItemFunction(sword1);
        equipmentItemList.Add(shoulder1);

        Equipment bow1 = new Equipment(200, 150, "2003", "bow1", "활활타오르는활활", "화살은 별도입니다", "weapon", equipmentSpriteList[2]);
        bow1.UseEvent += (item) => UseItemFunction(bow1);
        equipmentItemList.Add(bow1);

        // #### Equipment의 Target Slot을 지정해야됨
        // ex ) bow1.TargetSlot = targetSlotList[0];



    }

    private void RegisterCommonItem()
    {
        CommonItem iron1 = new CommonItem(1, 5, 200, 150, "3001", "iron1", "철철김철강철", "고급인력이다", "material", commonItemSpriteList[0]);
        iron1.UseEvent += (item) => UseItemFunction(iron1);
        commonItemList.Add(iron1);

        CommonItem paper1 = new CommonItem(1, 5, 200, 150, "3002", "paper1", "컨닝페이퍼", " A+ or F", "material", commonItemSpriteList[1]);
        paper1.UseEvent += (item) => UseItemFunction(paper1);
        commonItemList.Add(paper1);

    }

    private void UseItemFunction(SlotItem item)
    {
        Debug.Log("Use : " + item.DisplayName);
    }


    // 깊은 복사 메서드
    // 인덱스로 찾기와 이름이로 찾기 2가지 오버로딩
    public static CommonItem CommonItemClone(int index)
    {
        CommonItem newItem = new CommonItem(commonItemList[index].Count, commonItemList[index].MaxCount
            , commonItemList[index].BuyPrice, commonItemList[index].SellPrice, commonItemList[index].Id
            , commonItemList[index].Name, commonItemList[index].DisplayName, commonItemList[index].Description
            , commonItemList[index].Type, commonItemList[index].Icon);

        return newItem;
    }

    public static CommonItem CommonItemClone(string findWithName)
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

        CommonItem newItem = new CommonItem(commonItemList[index].Count, commonItemList[index].MaxCount
            , commonItemList[index].BuyPrice, commonItemList[index].SellPrice, commonItemList[index].Id
            , commonItemList[index].Name, commonItemList[index].DisplayName, commonItemList[index].Description
            , commonItemList[index].Type, commonItemList[index].Icon);

        return newItem;
    }

    public static Consum ConsumItemClone(int index)
    {
        Consum newItem = new Consum(consumItemList[index].Count, consumItemList[index].MaxCount, consumItemList[index].BuyPrice
            , consumItemList[index].SellPrice, consumItemList[index].Id, consumItemList[index].Name
            , consumItemList[index].DisplayName, consumItemList[index].Description, consumItemList[index].Type
            , consumItemList[index].Icon);

        return newItem;
    }

    public static Consum ConsumItemClone(string findWithName)
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

        Consum newItem = new Consum(consumItemList[index].Count, consumItemList[index].MaxCount, consumItemList[index].BuyPrice
            , consumItemList[index].SellPrice, consumItemList[index].Id, consumItemList[index].Name
            , consumItemList[index].DisplayName, consumItemList[index].Description, consumItemList[index].Type
            , consumItemList[index].Icon);

        return newItem;
    }


    public static Equipment EquipmentItemClone(int index)
    {
        Equipment newItem = new Equipment(equipmentItemList[index].BuyPrice, equipmentItemList[index].SellPrice
            , equipmentItemList[index].Id, equipmentItemList[index].Name, equipmentItemList[index].DisplayName
            , equipmentItemList[index].Description, equipmentItemList[index].Type, equipmentItemList[index].Icon);

        return newItem;
    }

    //인덱스 쓰는게 더빠름
    public static Equipment EquipmentItemClone(string findWithName)
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

        Equipment newItem = new Equipment(equipmentItemList[index].BuyPrice, equipmentItemList[index].SellPrice
            , equipmentItemList[index].Id, equipmentItemList[index].Name, equipmentItemList[index].DisplayName
            , equipmentItemList[index].Description, equipmentItemList[index].Type, equipmentItemList[index].Icon);

        return newItem;
    }
}
