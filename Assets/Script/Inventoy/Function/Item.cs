using UnityEngine;
using System;

/// <summary>
/// SlotItem 을 상속해야 인벤토리에서 사용이 가능합니다.
/// 
/// - IUsable : 가장 일반적인 아이템 사용 인터페이스
/// - IEquipment : 사용시 지정된 슬롯으로 이동 또는 교체
/// - IConsum : 사용시 갯수가 줄어들며 0개가 되면 탭에서 자동 제거
/// </summary>

public class CommonlItem : SlotItem, IUsable, ITradable
{

    public CommonlItem(int count , int maxCount , int buyPrice , int sellPrice ,string id, string name, string displayName, string description, string type, Sprite icon)
    {
        SetCount();
        SetProperty(id, name, displayName, description, type);
        Icon = icon;
        Usable = true;
    }

    public bool Usable { get; set; }
    public Action<SlotItem> UseEvent { get; set; }

    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }

    public void SetPrice(int buyPrice, int sellPrice)
    {
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
    }
}

public class Equipment : SlotItem, IEquipment, ITradable
{

    public Equipment(int buyPrice , int sellPrice, string id, string name, string displayName, string description, string type, Sprite icon)
    {
        SetCount();
        SetProperty(id, name, displayName, description, type);
        SetPrice(buyPrice, sellPrice);
        Icon = icon;
        Usable = true;
    }

    public bool Usable { get; set; }
    public InventorySlot TargetSlot { get; set; }
    public Action<SlotItem> UseEvent { get; set; }

    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }

    public void SetPrice(int buyPrice, int sellPrice)
    {
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
    }
}

public class Consum : SlotItem, IConsum, ITradable
{
    private Consum itemFromData;

    public Consum(int count, int maxCount,int buyPrice,int sellPrice, string id, string name,string displayName, string description, string type, Sprite icon)
    {
        SetCount(count, maxCount);
        SetProperty(id, name, displayName, description, type);
        SetPrice(buyPrice, sellPrice);
        Icon = icon;
        Usable = true;
    }

    public bool Usable { get; set; }
    public Action<SlotItem> UseEvent { get; set; }

    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }

    public void SetPrice(int buyPrice, int sellPrice)
    {
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
    }
}
