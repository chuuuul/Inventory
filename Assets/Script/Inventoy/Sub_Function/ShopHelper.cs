using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ShopHelper {

    //아이템 구매
    public static void Buy(ref int money, SlotItem item, InventoryTab targetTab, Action buyFailEvent = null, Action tabFullEvent = null)
    {
        if (item is ITradable)
        {
            ITradable tradableItem = item as ITradable;

            //아이템이 추가될 탭이 꽉 찼을 경우 이벤트 발생 및 종료
            if (targetTab.IsFull)
            {
                tabFullEvent?.Invoke();
                return;
            }

            //아이템 구매
            if (money >= tradableItem.BuyPrice)
            {
                money -= tradableItem.BuyPrice;
                targetTab.Add(item, true);
            }
            else buyFailEvent?.Invoke(); //돈이 부족할 경우 이벤트 실행및 종료
        }
    }

    //아이템 모두 판매
    public static void Sell(ref int money, SlotItem item)
    {
        if (item is ITradable)
        {
            ITradable tradableItem = item as ITradable;
            int total = tradableItem.SellPrice * item.Count;
            item.Tab.Remove(item);
            money += total;
        }
    }

    //아이템 특정 갯수 판매
    public static void Sell(ref int money, SlotItem item, int count)
    {
        if (item is ITradable)
        {
            if (item.Count >= count)
            {
                ITradable tradableItem = item as ITradable;
                int total = tradableItem.SellPrice * count;
                item.Count -= count;
                if (item.Count <= 0) item.Tab.Remove(item);
                money += total;
            }
            else Sell(ref money, item);
        }
    }
}
