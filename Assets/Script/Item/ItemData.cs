using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemData : MonoBehaviour
{

    public static List<Consum> consumItemList = new List<Consum>();
    public static List<Equipment> equipmentItemList = new List<Equipment>();
    public static List<CommonlItem> nomalItemList = new List<CommonlItem>();

    public List<Sprite> consumSpriteList = new List<Sprite>();
    public List<Sprite> equipmentSpriteList = new List<Sprite>();
    public List<Sprite> commonItemSpriteList = new List<Sprite>();
    
    private void Awake()
    {
        
        RegisterConsumItem();       // 소모템
        RegisterEquipmentItem();    // 장비템
        RegisterCommonItem();       // 일반템
    }

    private void RegisterConsumItem()
    {

        Consum hp1 = new Consum(1, 3, 100, 90, "1001", "hp1", "초급 체력포션", "hp +50", "potion", consumSpriteList[0]);
        hp1.UseEvent += (item) => UseItemMethod(hp1);
        consumItemList.Add(hp1);

        Consum mp1 = new Consum(1, 3, 100, 90, "1002", "mp1", "초급 마나포션", "mp +50", "potion", consumSpriteList[1]);
        hp1.UseEvent += (item) => UseItemMethod(hp1);
        consumItemList.Add(mp1);

    }

    private void RegisterEquipmentItem()
    {
        Equipment sword1 = new Equipment(200, 150, "2001", "sword1", "도란검", "싸고 효율좋은 도란검","weapon", equipmentSpriteList[0]);
        sword1.UseEvent += (item) => UseItemMethod(sword1);
        equipmentItemList.Add(sword1);
    }

    private void RegisterCommonItem()
    {

    }

    private void UseItemMethod(SlotItem item)
    {
        Debug.Log("Use : " + item.DisplayName);
    }

    



}
