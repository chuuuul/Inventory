﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MenuViewer : ContentViewer  {

    [Space(8f)]

    public InventoryManager inventory;
    public Text useText;
    public Text removeText;
    public Button removeButton;
    public Button sellButton;
    public InfoView infoView;

    private InventorySlot slot;
    

    private enum Widget { NoSelect, Shop, Inventory, QuickSlot };
    private Widget SelectWidget = Widget.NoSelect;


    private void Update()
    {
        // 외부클릭 감지
        if( group.alpha > 0 && Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false )
        {
            Cancel();
        }

    }

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
            SelectWidget = Widget.Shop;
            anchor = ViewerAnchor.BottomRight;
            useText.text = "구매";
            removeText.text = "버리기";
            removeButton.interactable = false;
            sellButton.interactable = false;

        }
        else if (inventory.QuickTabList.Exists(x => x.name == slot.Item.Tab.TabName))
        {
        
            SelectWidget = Widget.QuickSlot;
            anchor = ViewerAnchor.TopRight;
            if (item is Equipment)
                removeText.text = "착용 해제";
            else
                removeText.text = "등록 해제";
            removeButton.interactable = true;
            sellButton.interactable = false;
        }

        else if (inventory.InvenTabList.Exists(x => x.name == slot.Item.Tab.TabName))
        {

            SelectWidget = Widget.Inventory;
            anchor = ViewerAnchor.BottomLeft;
            removeText.text = "버리기";

            if (item is Equipment)
                useText.text = "착용";
            else
                useText.text = "사용";

            removeButton.interactable = true;
            sellButton.interactable = true;
        }
    }


    public void Use()
    {
        if (slot != null)
        {
            if (SelectWidget == Widget.Shop)
            {
                // 아이템을 구매하고 각각 다른 인벤토리에 저장할경우 inventory.InvenTabList[0].name 를 수정해서 사용한다
                if (slot.Item is Consum)
                    slot.Item = ItemData.ConsumItemClone(slot.Item.Name);

                else if (slot.Item is Equipment)
                    slot.Item = ItemData.EquipmentItemClone(slot.Item.Name);

                else if (slot.Item is CommonItem)
                    slot.Item = ItemData.CommonItemClone(slot.Item.Name);

                ShopHelper.Buy(ref InventoryManager.money, slot.Item, TabManager.GetTab(inventory.InvenTabList[0].name), () => Debug.Log("돈이 부족합니다"), () => Debug.Log("탭이 꽉찼습니다"));
                InventoryManager.MoneyRefresh();
                SlotManager.RefreshAll();

                Cancel();
            }
            else
            {
                slot.itemHandler.Use(slot, slot.Item);
                SlotManager.RefreshAll();
                Cancel();
            }
        }
    }

    public void Remove()
    {
        if (slot != null)
        {
            

            if (inventory.QuickTabList.Exists(x => x.name == slot.Item.Tab.TabName))
            {
                if (slot.Item is Consum)
                    inventory.invenSlotManager.LastRefreshedTab.Add(ItemData.ConsumItemClone(slot.Item.Name) as SlotItem, true);

                if (slot.Item is Equipment)
                    inventory.invenSlotManager.LastRefreshedTab.Add(ItemData.EquipmentItemClone(slot.Item.Name) as SlotItem, true);

                if (slot.Item is CommonItem)
                    inventory.invenSlotManager.LastRefreshedTab.Add(ItemData.CommonItemClone(slot.Item.Name) as SlotItem, true);
            } //퀵슬롯일 경우 착용 해제 (제거 후 재추가)

            slot.Item.Tab.Remove(slot.Item);

            SlotManager.RefreshAll();
            Cancel();
        }

    }
    public void Sell()
    {
        if (slot != null)
        {
            ShopHelper.Sell(ref InventoryManager.money, slot.Item);

            InventoryManager.MoneyRefresh();
            SlotManager.RefreshAll();
            Cancel();
        }

    } 


    public void Cancel()
    {
        if (infoView != null)
            infoView.Cancel();
        ViewerDisable();
        slot = null;
        SelectWidget = Widget.NoSelect;
    }


}
