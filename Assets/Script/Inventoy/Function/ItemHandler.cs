using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/// <summary>
///  - 아이템 이동, 위치 교환, 병합 : 드래그 앤 드롭
///  - 아이템 사용 : 더블 클릭 (PC), 한번 탭 (모바일)
/// </summary>

namespace Inventory
{
    [RequireComponent(typeof(SlotManager))]
    public class ItemHandler : MonoBehaviour {

        // 아이템 핸들러 리스트 , 딕셔너리 ( 오브젝트명 )
        public static List<ItemHandler> HandlerList { get; private set; } = new List<ItemHandler>();
        public static Dictionary<string, ItemHandler> HandlerDiction { get; internal set; } = new Dictionary<string, ItemHandler>();

        [Tooltip("raycast를 할 canvas")]
        public Canvas canvas;           // 캔버스
        internal GraphicRaycaster gr;   // 그래픽 레이캐스트

        [Header("Item Managemnet Option")]
        [Tooltip("아이템 이동 가능 여부")]
        public bool movable = true;         

        [Tooltip("다른 탭으로 이동 가능 여부")]
        public bool moveToOtherSlot = true; // 이동이 가능 할 때 동작

        [Tooltip("아이템간 위치 교환 가능 여부")]
        public bool switching = true;       // 이동이 가능 할 때 동작

        [Tooltip("아이템 병합 가능 여부")]
        public bool merging = true;         // 사용이 가능 할 때 동작

        [Tooltip("아이템 사용 가능 여부")]
        public bool usable = true;          

    
        public Action<InventorySlot, SlotItem> OnItemSelected { get; set; } //아이템 선택 이벤트 (선택 아이템)
        public Action OnEventEnded { get; set; } //이벤트 종료 알림 이벤트

        public Action<SlotItem> OnItemMoved { get; set; } //아이템 이동 이벤트 (선택 아이템)
        public Action<SlotItem, SlotItem> OnItemSwitched { get; set; } //아이템 스위칭 이벤트 (선택 아이템, 대상 아이템)
        public Action<SlotItem> OnItemMerged { get; set; } //아이템 병합 이벤트 (대상 아이템)
        public Action<SlotItem> OnItemUsed { get; set; } //아이템 사용 이벤트 (선택 아이템)

        public Action<SlotItem> DragOutEvent { get; set; } //아이템을 인벤토리 밖으로 드래그했을 경우 이벤트
        public Action<SlotItem, InventorySlot> TypeNotMatchEvent { get; set; } //타입이 맞지 않을 경우 발생하는 이벤트
        public Action<SlotItem> SlotMoveFailEvent { get; set; } //슬롯간 이동 불능시 발생 이벤트

        public Action<PointerEventData, InventorySlot> OnSlotDown { get; set; } //포인터 다운 이벤트
        public Action<PointerEventData, InventorySlot> OnSlotUp { get; set; } //포인터 업 이벤트
        public Action<PointerEventData, InventorySlot> OnSlotClick { get; set; } //포인터 클릭 이벤트
        public Action<PointerEventData, InventorySlot> OnSlotEnter { get; set; } //포인터 엔터 이벤트
        public Action<PointerEventData, InventorySlot> OnSlotExit { get; set; } //포인터 익스트 이벤트

        public SlotItem SelectedItem { get; internal set; } //선택된 아이템
        public SlotItem TargetItem { get; internal set; } //타겟 아이템

        private void Awake()
        {
            //핸들러 리스트에 현재 핸들러 추가
            if( !HandlerList.Contains(this) ) HandlerList.Add(this);
            if (!HandlerDiction.ContainsValue(this)) HandlerDiction.Add(this.name, this);

            //아이템 이동 불가시 탭 이동, 스위칭, 머징 끄기
            if(!movable)
            {
                moveToOtherSlot = false;
                switching = false;
                merging = false;
            }

            //그래픽 레이캐스트 설정
            if ( canvas != null)
                gr = canvas.GetComponent<GraphicRaycaster>();
        
        }


        //아이템 이동 (아이템 -> 빈 슬롯)
        public void Move(SlotItem selectedItem, InventorySlot targetSlot)
        {
            int originIndex = selectedItem.Index;
            targetSlot.slotManager.LastRefreshedTab.ItemTable[targetSlot.Index] = selectedItem;
            selectedItem.Tab[originIndex] = null;

            selectedItem.Index = targetSlot.Index;
            selectedItem.Tab = targetSlot.slotManager.LastRefreshedTab;


            OnItemMoved?.Invoke(selectedItem);
        }

        //아이템 교환 (아이템 <-> 아이템)      //#### 이거 잘 되는지 봐야됨 어렵다
        public void Switch(SlotItem selectedItem,SlotItem targetItem)
        {
            // 스위칭할때 변경사항
            // 1. 내용물(아이템정보) 변경 
            // 2. 인덱스 위치 변경
            // 3. Tab 위치 변경

            int originIndex = selectedItem.Index;       
            int targetIndex = targetItem.Index;

            // 1. 아이템 정보 스위치 ( Itemtable 변경 )
            // 인덱서 사용 public SlotItem this[int itemIndex]
            selectedItem.Tab[originIndex] = targetItem;
            targetItem.Tab[targetIndex] = selectedItem;

            // 2. 인덱스 위치 스위치
            selectedItem.Index = targetIndex;
            targetItem.Index = originIndex;

            // 3. Tab 위치 변경
            InventoryTab originTab = selectedItem.Tab;
            InventoryTab targetTab = targetItem.Tab;

            targetItem.Tab = originTab;
            selectedItem.Tab = targetTab;

            OnItemSwitched?.Invoke(selectedItem, targetItem);

        }

        // 아이템 병합 (같은 아이템 -> 아이템)
        public void Merge(SlotItem selectedItem , SlotItem targetItem)
        {

            if (targetItem.MaxCount > targetItem.Count)
            {
                int valid = targetItem.MaxCount - targetItem.Count;
                if( selectedItem.Count <= valid)        // 한개로 통합
                {
                    targetItem.Count += selectedItem.Count;
                    selectedItem.Tab.Remove(selectedItem);
                }
                else                                    // 두개로 나눔
                {
                    targetItem.Count += valid;
                    selectedItem.Count -= valid;
                }
            }
            else
                Switch(selectedItem, targetItem);

            OnItemMerged?.Invoke(targetItem);
        }

        // 아이템 사용
        public void Use (InventorySlot usedSlot, SlotItem usedItem)
        {
            // 사용아이템일 때 
            if (usedItem is IUsable)        //IUsable 인터페이스를 지원하는지 확인
            {
                IUsable usableItem = usedItem as IUsable;
                //사용 이벤트가 있을 경우 실행
                if(usableItem.Usable)
                {
                    usableItem.UseEvent?.Invoke(usedItem); 
                    OnItemUsed?.Invoke(usedItem);
                }
            }

            // 장비템아이일 때
            else if ( usedItem is IEquipment)
            {
                IEquipment equipment = usedItem as IEquipment;
            
                // 장착
                if(equipment.Usable)
                {
                    if (equipment.TargetSlot.Item == null)
                        Move(usedItem, equipment.TargetSlot);
                    else
                        Switch(usedItem, equipment.TargetSlot.Item);

                    equipment.UseEvent?.Invoke(usedItem);
                    equipment.TargetSlot.slotManager.Refresh(usedItem.Tab);
                    OnItemUsed?.Invoke(usedItem);
                }

            }
            // 소비아이템일 때
        
            else if ( usedItem is IConsum)
            {
                IConsum consum = usedItem as IConsum;

                if( consum.Usable )
                {
                    // 개수 -1
                    usedItem.Count--;
                    if (usedItem.Count < 1) 
                        usedItem.Tab.Remove(usedItem);

                    consum.UseEvent?.Invoke(usedItem);
                    OnItemUsed?.Invoke(usedItem);
                }
            }
        }

        //선택 및 대상 아이템 초기화
        internal void ResetItems ()
        {
            SelectedItem = null;
            TargetItem = null;
            OnEventEnded?.Invoke();
        }

    }
}