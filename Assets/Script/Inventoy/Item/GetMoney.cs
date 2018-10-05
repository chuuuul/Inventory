using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory;


namespace Item
{
    public class GetMoney : MonoBehaviour
    {

        public Vector2 DropMoneyRange;

        // 돈머겅
        private void OnTriggerEnter(Collider other)
        {
            InventoryManager.money += (int)UnityEngine.Random.Range(DropMoneyRange.x, DropMoneyRange.y);
            Destroy(this.gameObject);
            InventoryManager.MoneyRefresh();
        }

    }
}