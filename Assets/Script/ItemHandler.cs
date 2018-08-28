using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ItemHandler : MonoBehaviour {

    public static List<ItemHandler> ItemhandlerList = new List<ItemHandler>() ;

    public Canvas canvas;
    internal GraphicRaycaster gr;


    public Action<PointerEventData, SlotScript> OnSlotUp { get; set; } //포인터 업 이벤트

    private void Awake()
    {
        if ( canvas != null)
            gr = canvas.GetComponent<GraphicRaycaster>();

    }


    public void makeTarget()
    {


    }


}
