using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventorySlot : MonoBehaviour, IPointerDownHandler//, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public SlotManager slotManager;     //슬롯 매니저
    public Image slotIcon;             
    //슬롯 아이콘
    public Text itemCountText;          //아이템 개수 표시 텍스트
    public string allowType;            //허용되는 아이템 타입 (공백일 경우 모두 허용)

    internal ItemHandler itemHandler;   //아이템 핸들러

    public int Index { get; internal set; } //슬롯 인덱스
    public SlotItem Item { get; internal set; } //현재 슬롯 아이템

    Action<string> good { get; set; }

    private void Awake()
    {
        // 슬롯 매니저 캐싱 확인
        if (slotManager == null)
        { 
            Debug.Log("슬롯 매니저가 캐싱되지 않았습니다.");
            return;
        }

        // 이미지 할당 확인
        if (slotIcon == null)
        {
            Image image;
            if ((image = GetComponent<Image>()) != null)
                slotIcon = image;
        }

        // 탭매니저에 슬롯 등록
        if (!slotManager.slotList.Contains(this))
            slotManager.slotList.Add(this);

        // 아이템 핸들러 설정
        itemHandler = slotManager.itemHandler;

    }

    //슬롯 설정
    internal void SetSlot (SlotItem slotItem)
    {

        if (slotItem != null)
        {
            this.Item = slotItem;
            if (slotIcon != null) slotIcon.sprite = slotItem.Icon; // 아이콘 설정
            if (itemCountText != null)
            {
                if (slotItem.MaxCount == 1) itemCountText.text = "";
                else itemCountText.text = slotItem.Count.ToString();
            }
        }
        else
        {
            //슬롯 기본값으로 초기화
            this.Item = null;
            if (slotIcon != null) slotIcon.sprite = slotManager.defaultIcon;
            if (itemCountText != null) itemCountText.text = "";
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {

        //itemHandler.On

    }
}
