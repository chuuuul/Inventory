using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Inventory;

namespace Item
{

    public static class ItemEffect
    {

        public static void UseItemFunction(SlotItem item)
        {
            Debug.Log("Useasd : " + item.DisplayName);
        }

        public static void MoneyUp(int amount)
        {
            Debug.Log("돈을 " + amount + " 을 얻었습니다.");
            InventoryManager.GetMoney(amount);
        }

        public static void SizeUp(int amount)
        {
            Debug.Log("사이즈업 미구현");
        }



    }
}