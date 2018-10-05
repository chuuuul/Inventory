using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ShopManager : WidgetToggle
    {
        public float rotateSpeed = 0.5f;

        private new void Start()
        {
            base.Start();
            GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * rotateSpeed;

        }

        // 가까우면 상점 키고
        private void OnTriggerEnter(Collider other)
        {
            toggle = true;
            InvenDisplay();
        }

        // 멀어지면 상점 끄고
        private void OnTriggerExit(Collider other)
        {
            toggle = false;
            InvenDisplay();
        }


    }
}