using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public SlotManager invenSlotManager;
    public SlotManager shopSlotManager;

    public List<GameObject> InvenTabList = new List<GameObject>();
    public List<GameObject> ShopTabList = new List<GameObject>();

    private void Start()
    {
        MakeShop();
        // 처음에 보여질 탭 선택
        TabChangeWithClick(InvenTabList[0]);
        TabChangeWithClick(ShopTabList[0]);
    }

    // 클릭해서 탭 변경하기
    public void TabChangeWithClick (GameObject tabObject)
    {

        List<GameObject> TabList = new List<GameObject>();


        // tabObject가 어느 리스트에 속해있는지 검출 ( Inven의 Tab 인지 Shop의 Tab인지 검출)
        // 변경된 탭으로 아이템창 변경
        if (InvenTabList.Contains(tabObject))
        {
            TabList = InvenTabList;
            invenSlotManager.Refresh(TabManager.GetTab(tabObject.name));
        }
        else if (ShopTabList.Contains(tabObject))
        {
            TabList = ShopTabList;
            shopSlotManager.Refresh(TabManager.GetTab(tabObject.name));
        }
        else
        { 
            Debug.Log("탭 리스트에 추가 되어있지 않는 탭입니다. 탭 리스트에 추가하세요");
            return;
        }


        //클릭시 색깔 바꾸기
        for ( int i = 0; i < TabList.Count; i++)
        {
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
        }
    }

    // 마우스 아이템 Drag 중 탭 변경
    public void TabChangeWithMouseOn(GameObject TabObject)
    { 
        if (invenSlotManager.itemHandler.SelectedItem != null)
            TabChangeWithClick(TabObject);
    }

    private void MakeShop()
    {
        InventoryTab ShopFirstTab = TabManager.GetTab("ShopFirstTab");
        InventoryTab ShopSecondTab = TabManager.GetTab("ShopSecondTab");

        ShopFirstTab.Add( ItemData.consumItemList[0], 0 );
        ShopFirstTab.Add( ItemData.consumItemList[1], 1 );

        ShopSecondTab.Add( ItemData.equipmentItemList[0], 0 );
        ShopSecondTab.Add( ItemData.equipmentItemList[1], 1 );
        ShopSecondTab.Add( ItemData.equipmentItemList[2], 2 );
        ShopSecondTab.Add( ItemData.commonItemList[0], 3 );
        ShopSecondTab.Add( ItemData.commonItemList[1], 4 );

        InventoryTab aa = TabManager.GetTab("InvenFirstTab");
        aa.Add(ItemData.consumItemList[0],19);


    }
}
 