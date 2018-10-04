using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InfoView : ContentViewer
{
    public InventoryManager inventory;


    [Space(8f)]
    public Text nameText;
    public Text typeText;
    public Text descText;
    public Text priceText;

    private enum Widget { NoSelect, Shop, Inventory, QuickSlot };
    private Widget SelectWidget = Widget.NoSelect;

    protected override void EventCall() //이벤트 할당 메서드
    {
        for (int i = 0; i < ItemHandler.HandlerList.Count; i ++)
            ItemHandler.HandlerList[i].OnSlotUp += OnDisplay;
    }

    protected override void OnDisplay(PointerEventData eventData, InventorySlot slot) //활성화 이벤트
    {
        // 이상한 클릭일때 안보이기
        if (slot.Item == null || eventData.button == PointerEventData.InputButton.Left ||
            eventData.button == PointerEventData.InputButton.Middle)
            Cancel();

        //#### 모바일버전 호환 ??
        // 정상 클릭일때 (오른쪽 클릭) Viewer Enable
        if (slot.Item != null && eventData.button == PointerEventData.InputButton.Right)
            ViewerEnable(slot);
    }


    protected override void OnDisappear(PointerEventData eventData, InventorySlot slot) //비활성화 이벤트
    {
    }


    protected override void DrawContent(InventorySlot slot)
    {
        SlotItem item = slot.Item;

        
        // 잠깐 없앰 ui 지저분함.. 위치 잡을수가 없음
        //슬롯의 아이템 탭 이름에 따라 정렬 기준 설정
        if (inventory.ShopTabList.Exists(x => x.name == item.Tab.TabName))
        {
            SelectWidget = Widget.Shop;
            //anchor = ViewerAnchor.TopRight;           // #### 지우면 안돼 XX
        }
        else if (inventory.QuickTabList.Exists(x => x.name == item.Tab.TabName))
        {
            SelectWidget = Widget.QuickSlot;
            //anchor = ViewerAnchor.BottomRight;
        }
        else if (inventory.InvenTabList.Exists(x => x.name == item.Tab.TabName))
        {
            SelectWidget = Widget.Inventory;
            //anchor = ViewerAnchor.TopLeft;
        }
        
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

    public void Cancel()
    {
        ViewerDisable();
    }


}
