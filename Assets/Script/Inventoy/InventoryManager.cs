using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public SlotManager slotManager;
    

    public List<GameObject> TabList = new List<GameObject>();


    private void Start()
    {
        slotManager.Refresh(TabManager.GetTab("FirstTab"));
    }


    // 클릭해서 탭 변경하기
    public void TabChangeWithClick (GameObject tabObject)
    {
        for ( int i = 0; i < TabList.Count; i++)
        {
            //클릭시 색깔 바꾸기
            if(tabObject != TabList[i].gameObject)
            {
                TabList[i].GetComponent<Button>().interactable = true;
                TabList[i].GetComponent<Image>().color = new Color32(240, 240, 240, 255);
            }
            else
            {
                TabList[i].GetComponent<Button>().interactable = false;
                TabList[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            // 변경된 탭으로 아이템창 변경
            slotManager.Refresh(TabManager.GetTab(tabObject.name));
        }
    }

    // 마우스 올려서 탭 변경
    public void TabChangeWithMouseOn(GameObject TabObject)
    { 
        if (slotManager.itemHandler.SelectedItem != null)
            TabChangeWithClick(TabObject);
    }

    private void MakeShop()
    {
        
        //Consum 


    }
}
 