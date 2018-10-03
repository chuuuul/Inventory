using System.Collections.Generic;
using System;
using UnityEngine;

public class WidgetToggle : MonoBehaviour
{

    public List<CanvasGroup> canvasGroup;
    public MenuViewer menuviewer;

    protected bool toggle = false;

    private Action InvenOnOff;


    protected void Start()
    {
        InvenOnOff += InvenDisplay;

        InvenOnOff?.Invoke();         // 초기화

    }
    
    public void InvenDisplay()
    {

        for (int i = 0; i < canvasGroup.Count; i++)
        {
            if (toggle == true)        //켜짐
                canvasGroup[i].alpha = 1;
             
            else if (toggle == false)    //꺼짐
            {
                canvasGroup[i].alpha = 0;
                menuviewer.Cancel();
            }

        canvasGroup[i].interactable = toggle;
        canvasGroup[i].blocksRaycasts = toggle;
        }
    }

    public void ToggleSwitch()
    {
        toggle = !toggle;
        InvenOnOff?.Invoke();
    }

}



