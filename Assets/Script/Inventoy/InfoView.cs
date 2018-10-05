using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Inventory
{
    [RequireComponent(typeof(CanvasGroup))]
    public class InfoView : MonoBehaviour
    {
        public InventoryManager inventory;


        [Space(8f)]
        public Text nameText;
        public Text typeText;
        public Text descText;
        public Text priceText;

        public enum Widget { NoSelect, Shop, Inventory, QuickSlot };
        private Widget SelectWidget = Widget.NoSelect;

        private CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        private void ActiveToggle(bool toggle)
        {
            if (toggle == true) canvasGroup.alpha = 1;
            else canvasGroup.alpha = 0;
        }

        public void NoShow()
        {
            ActiveToggle(false);
        }

        public void Show(InventorySlot slot, Widget widget)
        {
            ActiveToggle(true);

            SlotItem item = slot.Item;
            SelectWidget = widget;       //받아온값 넘겨받기


            //슬롯의 아이템 탭 이름에 따라 가격 표시 설정
            if (item is ITradable)
            {
                ITradable tradable = item as ITradable;
                if (SelectWidget == Widget.Shop) priceText.text = "구매 가격 : " + tradable.BuyPrice.ToString();
                else if (SelectWidget == Widget.QuickSlot) priceText.text = "판매 가격 : " + tradable.SellPrice * item.Count;
                else if (SelectWidget == Widget.Inventory) priceText.text = "판매 가격 : " + tradable.SellPrice * item.Count;
            }

            //아이템 갯수에 따라 이름 및 갯수 표시
            if (item.Count == 1)
                nameText.text = item.DisplayName;
            else
                nameText.text = $"{item.DisplayName} ({item.Count})";

            //아이템 타입에 따라 색상 변경
            switch (item.Type)
            {
                case "weapon": typeText.color = new Color32(50, 160, 160, 255); break;
                case "material": typeText.color = new Color32(160, 50, 160, 255); break;
                case "potion": typeText.color = new Color32(160, 160, 50, 255); break;
                default: typeText.color = new Color32(50, 50, 50, 255); break;
            }

            //아이템 타입 및 설명 표시
            typeText.text = slot.Item.Type;
            descText.text = slot.Item.Description;
        }
    }
}