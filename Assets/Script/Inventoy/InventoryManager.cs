using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public SlotManager invenSlotManager;
    public SlotManager shopSlotManager;
    public Scrollbar shopScrollbar;

    public List<GameObject> InvenTabList = new List<GameObject>();
    public List<GameObject> ShopTabList = new List<GameObject>();

    public static int money = 1000;                             
    public static List<Text> moneyTextList = new List<Text>();
    public Text moneyText;
   

    private void Start()
    {
        if (moneyText == null)
        {
            Debug.Log("Inventory Manager에 moneytext 를 추가하세요");
        }
        moneyTextList.Add(moneyText);
        MoneyRefresh();

        MakeShop();
        // 처음에 보여질 탭 선택
        TabChangeWithClick(InvenTabList[0]);
        TabChangeWithClick(ShopTabList[0]);
    }


    public static void MoneyRefresh ()
    {
        for (int i = 0; i < moneyTextList.Count; i++)
            moneyTextList[i].text = money.ToString();
    }


    // 클릭해서 탭 변경하기
    public void TabChangeWithClick (GameObject tabObject)
    {

        List<GameObject> TabList = new List<GameObject>();


        // 변경된 탭으로 아이템창 변경 
        // 클릭한탭이 인벤토리 일 때   
        if (InvenTabList.Contains(tabObject))
        {
            TabList = InvenTabList;
            invenSlotManager.Refresh(TabManager.GetTab(tabObject.name));
        }
        // 클릭한탭이 상점탭 일 때
        else if (ShopTabList.Contains(tabObject))
        {
            if (shopScrollbar != null)
                shopScrollbar.value = 1;                //상점 탭 변경시 상점스크롤 맨위로 올려줌
            else
                Debug.Log("Inventory Manager에서 Shop Scroll을 캐싱하지 않았습니다.");

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

        
        ShopFirstTab.Add(ItemData.ConsumItemClone(0));
        ShopFirstTab.Add(ItemData.ConsumItemClone(1));

        ShopSecondTab.Add( ItemData.EquipmentItemClone(0) );
        ShopSecondTab.Add( ItemData.EquipmentItemClone(1) );
        ShopSecondTab.Add( ItemData.EquipmentItemClone(2) );
        ShopSecondTab.Add( ItemData.CommonItemClone(0) );
        ShopSecondTab.Add( ItemData.CommonItemClone(1) );


        InventoryTab aa = TabManager.GetTab("InvenFirstTab");
        
        aa.Add(ItemData.ConsumItemClone(0), 19);
        aa.Add(ItemData.EquipmentItemClone(0), 13);
        

    }
}
 