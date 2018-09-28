using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoView : ContentViewer
{
    [Space(8f)]
    public Text nameText;
    public Text typeText;
    public Text descText;
    public Text priceText;
    public MenuViewer itemMenuViewer;

    protected override void EventCall() //이벤트 할당 메서드
    {
        for(int i = 0; i < ItemHandler.HandlerList.Count; i++)
        {
            
        }
    }
    protected override void OnDisplay(PointerEventData eventData, InventorySlot slot) //활성화 이벤트
    {
    }
    protected override void OnDisappear(PointerEventData eventData, InventorySlot slot) //비활성화 이벤트
    {
    }
    protected override void DrawContent(InventorySlot slot) //뷰어 내용 그리기 메서드
    {
    }
}
