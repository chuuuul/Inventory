﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuViewer : ContentViewer {

    [Space(8f)]

    public InventoryManager inventory;
    public Text useText;
    public Text removeText;
    public Button removeButton;
    public Button sellButton;

    private InventorySlot slot;

	// 슬롯을 클릭했을때 EventCall
    protected override void EventCall()
    {
        
        for (int i = 0; i < ItemHandler.HandlerList.Count; i++)
            ItemHandler.HandlerList[i].OnSlotClick += OnDisplay;
    }

    protected override void OnDisplay(PointerEventData eventData, InventorySlot slot)
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
    protected override void OnDisappear(PointerEventData eventData, InventorySlot slot)
    {
    }

    
    protected override void DrawContent(InventorySlot slot)
    {
        this.slot = slot;   //슬롯 등록
        SlotItem item = slot.Item;      //아이템 가져오기

        // Tab에 따라서 MenuViewer 위치 변경 및 버튼 변경
        if (inventory.ShopTabList.Exists(x => x.name == slot.Item.Tab.TabName))
        {
            anchor = ViewerAnchor.BottomRight;
            useText.text = "구매";
            removeText.text = "버리기";
            removeButton.interactable = false;
            sellButton.interactable = false;
            Debug.Log("Wow");

        }
        /*
        else if (inventory.QuickSlotTabList.Exists(x => x.name == slot.Item.Tab.TabName))
        {
            anchor = ViewerAnchor.TopRight;
            removeText.text = "착용 해제";
            removeButton.interactable = true;
            sellButton.interactable = false;
        }
        */
        else if (inventory.InvenTabList.Exists(x => x.name == slot.Item.Tab.TabName))
        {
            anchor = ViewerAnchor.BottomRight;
            removeText.text = "버리기";

            if (item is Equipment)
                useText.text = "착용";
            else
                useText.text = "사용";

            removeButton.interactable = true;
            sellButton.interactable = true;
            Debug.Log("Wow22");
        }
    }


    private void Use()
    {
        if( slot != null )
        {
            slot.itemHandler.Use(slot, slot.Item);
            SlotManager.RefreshAll();
            Cancel();
        }
    }

    private void Remove()
    {
        if (slot != null)
        {
            slot.Item.Tab.Remove(slot.Item);
            /*
            if (inventory.QuickSlotTabList.Exists(x => x.name == slot.Item.Tab.TabName))
            {
                inventory.slotManager.LastRefreshedTab.Add(slot.Item, false);
            } //퀵슬롯일 경우 착용 해제 (제거 후 재추가)
            */
            SlotManager.RefreshAll();
            Cancel();
        }

    }
    private void Sell()
    {
        if (slot != null)
        {
            //shopHelper.Sell(ref inventory.money, slot.Item);

            inventory.moneyText.text = inventory.money.ToString();
            SlotManager.RefreshAll();
            Cancel();
        }

    }



    private void Cancel()
    {
        ViewerDisable();
        slot = null;
    }
}
