using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemData : MonoBehaviour
{

    public static List<Consum> consumItemList = new List<Consum>();
    public static List<Equipment> equipmentItemList = new List<Equipment>();
    public static List<CommonlItem> commonItemList = new List<CommonlItem>();

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

        Consum hp1 = new Consum(1, 5, 100, 90, "1001", "hp1", "초급 체력포션", "hp +50", "potion", consumSpriteList[0]);
        hp1.UseEvent += (item) => UseItemMethod(hp1);
        consumItemList.Add(hp1);

        Consum mp1 = new Consum(1, 5, 100, 90, "1002", "mp1", "초급 마나포션", "mp +50", "potion", consumSpriteList[1]);
        hp1.UseEvent += (item) => UseItemMethod(hp1);
        consumItemList.Add(mp1);

    }

    private void RegisterEquipmentItem()
    {
        Equipment sword1 = new Equipment(200, 150, "2001", "sword1", "도란검", "싸고 효율좋은 도란검", "weapon", equipmentSpriteList[0]);
        sword1.UseEvent += (item) => UseItemMethod(sword1);
        equipmentItemList.Add(sword1);

        Equipment shoulder1 = new Equipment(200, 150, "2002", "shoulder1", "어깨빵", "싸고 효율좋은 어깨빵", "shoulder", equipmentSpriteList[1]);
        shoulder1.UseEvent += (item) => UseItemMethod(sword1);
        equipmentItemList.Add(shoulder1);

        Equipment bow1 = new Equipment(200, 150, "2003", "bow1", "활활타오르는활활", "화살은 별도입니다", "weapon", equipmentSpriteList[2]);
        bow1.UseEvent += (item) => UseItemMethod(bow1);
        equipmentItemList.Add(bow1);


    } 

    private void RegisterCommonItem()
    {
        CommonlItem iron1 = new CommonlItem(1, 5, 200, 150, "3001", "iron1", "철철김철강철", "고급인력이다", "material", commonItemSpriteList[0]);
        iron1.UseEvent += (item) => UseItemMethod(iron1);
        commonItemList.Add(iron1);

        CommonlItem paper1 = new CommonlItem(1, 5, 200, 150, "3002", "paper1", "컨닝페이퍼", " A+ or F", "material", commonItemSpriteList[1]);
        paper1.UseEvent += (item) => UseItemMethod(paper1);
        commonItemList.Add(paper1);

    }

    private void UseItemMethod(SlotItem item)
    {
        Debug.Log("Use : " + item.DisplayName);
    }

    



}
