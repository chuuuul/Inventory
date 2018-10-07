using UnityEngine;
using System;
using Inventory;
/// <summary>
/// SlotItem 을 상속해야 인벤토리에서 사용이 가능합니다.
/// 
/// - ICommon : 가장 일반적인 아이템 사용 인터페이스
/// - IEquipment : 사용시 지정된 슬롯으로 이동 또는 교체
/// - IConsum : 사용시 갯수가 줄어들며 0개가 되면 탭에서 자동 제거
/// </summary>

namespace Item
{
    public class CommonItem : SlotItem, ICommon, ITradable
    {

        public CommonItem(int count, int maxCount, int buyPrice, int sellPrice, string id, string name, string displayName, string description, string type
            ,bool usable, Sprite icon, Action<SlotItem> useEvent)
        {
            SetCount(count,maxCount);
            SetProperty(id, name, displayName, description, type);
            SetPrice(buyPrice, sellPrice);
            Icon = icon;
            Usable = usable;
            UseEvent += useEvent;
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

        public Equipment(int buyPrice, int sellPrice, string id, string name, string displayName, string description, string type
                        , bool usable, Sprite icon, InventorySlot targetSlot, Action<SlotItem> useEvent)
        {
            SetCount();
            SetProperty(id, name, displayName, description, type);
            SetPrice(buyPrice, sellPrice);
            Icon = icon;
            Usable = usable;
            UseEvent += useEvent;
            TargetSlot = targetSlot;

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

        public Consum(int count, int maxCount, int buyPrice, int sellPrice, string id, string name, string displayName, string description, string type
                    , bool usable, Sprite icon, Action<SlotItem> useEvent)
        {
            SetCount(count, maxCount);
            SetProperty(id, name, displayName, description, type);
            SetPrice(buyPrice, sellPrice);
            Icon = icon;
            Usable = usable;
            UseEvent += useEvent;
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

    public class ImmediateItem : IImmediate
    {
        public ImmediateItem( string id ,string name ,string displayName , string description , string type
                            , bool usable , Action<int> useEvent)
        {
            Id = id;
            Name = name;
            DisplayName = displayName;
            Description = description;
            Type = type;
            Usable = usable;
            UseEvent += useEvent;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public bool Usable { get; set; }
        public Action<int> UseEvent { get; set; }

    }
}
