using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("없으면 부모에서 찾음")]
    public SlotManager slotManager;     //슬롯 매니저

    [Space(10f)]
    public Image slotIcon;              //슬롯 아이콘

    [Header("Text는 필요한것만 등록")]
    public Text itemCountText;          //아이템 개수 표시 텍스트
    public Text itemDisplayNameText;    //아이템 개수 표시 텍스트
    public Text itemDescriptionText;    //아이템 설명 표시 텍스트

    [Space(10f)]
    public string allowType;            //허용되는 아이템 타입 (공백일 경우 모두 허용)

    internal ItemHandler itemHandler;   //아이템 핸들러

    public int Index { get; internal set; } //슬롯 인덱스
    public SlotItem Item { get; internal set; } //현재 슬롯 아이템
    
    private void Start()
    {
        //캐싱 되었는지 확인
        if (slotManager == null)    
        {
            Debug.Log("슬롯매니저가 캐싱 되지 않았습니다");
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


    // 슬롯 설정
    internal void SetSlot (SlotItem slotItem)
    {

        if (slotItem != null)
        {
            this.Item = slotItem;
            if (slotIcon != null) slotIcon.sprite = slotItem.Icon; // 아이콘 설정

            if (itemCountText != null)      // 개수표시
            {
                if (slotItem.MaxCount == 1) itemCountText.text = "";
                else itemCountText.text = slotItem.Count.ToString();
            }

            if (itemDisplayNameText != null)    // 아이템 이름 표시
                itemDisplayNameText.text = slotItem.DisplayName;

            if (itemDescriptionText != null)    // 설명 표시
                itemDescriptionText.text = slotItem.Description;
        }
        else
        {
            //슬롯 기본값으로 초기화
            this.Item = null;
            if (slotIcon != null) slotIcon.sprite = slotManager.defaultIcon;
            if (itemCountText != null) itemCountText.text = "";
            if (itemDisplayNameText != null) itemDisplayNameText.text = "";
            if (itemDescriptionText != null) itemDescriptionText.text = "";
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        itemHandler.OnSlotDown?.Invoke(eventData, this);

        //좌클릭 이벤트
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //선택 아이템 등록(이동 활성화시)
            if (itemHandler.movable)
            {
                if (Item != null)
                {
                    itemHandler.SelectedItem = Item;
                    itemHandler.OnItemSelected?.Invoke(this, itemHandler.SelectedItem);
                }
            }
        }
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        itemHandler.OnSlotUp?.Invoke(eventData, this);

        //좌클릭 이벤트
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //이동 활성화 및 선택된 아이템 존재시 이벤트 발생
            if (itemHandler.movable && itemHandler.SelectedItem != null)
            {
                InventorySlot targetSlot = null;
                GameObject lastObject = null;

                //대상 아이템 등록
                try
                {
                    eventData.position = GetPointerPosition();
                    List<RaycastResult> results = new List<RaycastResult>();
                    itemHandler.gr.Raycast(eventData, results);
                    if (results.Count > 0) lastObject = results[0].gameObject;


                    if ((targetSlot = lastObject.GetComponent<InventorySlot>()) != null)    // 예외처리 안할시 에러나는 부분
                    {
                        itemHandler.TargetItem = targetSlot.Item;
                    }
                }
                // 인벤토리 외부로 드랍됨
                catch (Exception)
                {
                    itemHandler.DragOutEvent?.Invoke(itemHandler.SelectedItem);
                    itemHandler.ResetItems();

                    return;
                }

                // 슬롯의 Slot Manager가 다를 경우 이벤트 ( 옮길수 없을 때 이벤트 )
                if (targetSlot != null && this.slotManager != targetSlot.slotManager)
                {
                    if (!itemHandler.moveToOtherSlot || !targetSlot.itemHandler.moveToOtherSlot)
                    {
                        itemHandler.SlotMoveFailEvent?.Invoke(itemHandler.SelectedItem);
                        itemHandler.ResetItems();
                        return;
                    }

                }

                /*
                 *  순서도 *
                 타깃이 없다 -> 이동
                 타깃이 있다 
                    if (merging, allow type) 
                        { Merge }
                    else if ( switching ) 
                        {Switch}
                */

                // 선택아이템 != 타겟아이템 일 경우 이벤트
                if (itemHandler.SelectedItem != itemHandler.TargetItem)
                {
                    //드랍된 곳이 슬롯일 때 이벤트
                    if (targetSlot != null)
                    {
                        InventoryTab tab = targetSlot.slotManager.LastRefreshedTab;

                        // 타겟이 없을 경우 아이템 이동
                        if (itemHandler.TargetItem == null)
                        {
                            // 타겟 슬롯에 타입이 없거나 두 타입이 일치할 경우 실행
                            if (targetSlot.allowType == "" || itemHandler.SelectedItem.Type == targetSlot.allowType)
                            {
                                itemHandler.Move(itemHandler.SelectedItem, targetSlot);
                                slotManager.Refresh(slotManager.LastRefreshedTab);
                                targetSlot.slotManager.Refresh(tab);
                            }
                            else
                                itemHandler.TypeNotMatchEvent?.Invoke(itemHandler.SelectedItem, targetSlot);
                        }
                        // 타겟이 있을 경우
                        else
                        {
                            //아이템 병합이 활성화 일 때 이벤트 실행   
                            if ( itemHandler.merging && (itemHandler.SelectedItem.Name == itemHandler.TargetItem.Name))
                            {
                                itemHandler.Merge(itemHandler.SelectedItem, itemHandler.TargetItem);
                                slotManager.Refresh(slotManager.LastRefreshedTab);
                                targetSlot.slotManager.Refresh(tab);
                            }
                            else if( itemHandler.switching)
                            {
                                if (targetSlot.allowType == "" || itemHandler.SelectedItem.Type == targetSlot.allowType)
                                {
                                    itemHandler.Switch(itemHandler.SelectedItem, itemHandler.TargetItem);
                                    slotManager.Refresh(slotManager.LastRefreshedTab);
                                    targetSlot.slotManager.Refresh(tab);
                                }
                                else
                                    itemHandler.TypeNotMatchEvent?.Invoke(itemHandler.SelectedItem, targetSlot);

                            }
                        }
                    }
                }
            }
        }
        itemHandler.ResetItems();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        itemHandler.OnSlotClick?.Invoke(eventData, this);

        // 좌 더블 클릭 이벤트
        if (eventData.button == PointerEventData.InputButton.Left)
        {

#if UNITY_EDITOR || (!UNITY_ANDROID && !UNITY_IOS)
            if (!(eventData.clickCount >= 2)) return; //PC 더블 클릭 체크
#endif
            //슬롯에 아이템이 있을 경우 실행
            if (itemHandler.usable && Item != null)
            {
                try         //예외처리
                {
                    //현재 포인터에 있는 슬롯 가져오기
                    eventData.position = GetPointerPosition();
                    List<RaycastResult> results = new List<RaycastResult>();
                    itemHandler.gr.Raycast(eventData, results);

                    InventorySlot usedSlot;
                    if (results.Count > 0)
                    {
                        if ((usedSlot = results[0].gameObject.GetComponent<InventorySlot>()) != null)
                        {
                            itemHandler.Use(usedSlot, usedSlot.Item);
                            slotManager.Refresh(slotManager.LastRefreshedTab);
                        }
                    }

                }

                catch (Exception) { }
            }
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        itemHandler.OnSlotEnter?.Invoke(eventData, this);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        itemHandler.OnSlotExit?.Invoke(eventData, this);
    }

    //PC, 모바일 환경에 따라 포인터 위치 가져오기
    private Vector2 GetPointerPosition()
    {
        string os = "PC";
#if (UNITY_IOS && !UNITY_EDITOR) || (UNITY_ANDROID && !UNITY_EDITOR)
            os ="Mobile";
#endif
        if (os == "PC") return Input.mousePosition;
        else
        {
            Touch touch = Input.GetTouch(0);
            return touch.position;
        }
    }

}
