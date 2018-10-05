using System.Collections.Generic;
using System;
using UnityEngine;

namespace Inventory
{
    //Widget이 켜졋다 꺼졋다 하는 기능
    public class WidgetToggle : MonoBehaviour
    {

        public List<CanvasGroup> canvasGroup;

        //같이 사라질 menuViewer
        public MenuViewer menuViewer;

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
                    if (menuViewer != null)
                        menuViewer.Cancel();

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
}


