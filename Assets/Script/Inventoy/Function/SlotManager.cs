using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{

    private static List<SlotManager> ManagerList = new List<SlotManager>();



    [Tooltip("아이템 슬롯 리스트 (필요시 등록)")]
    public List<InventorySlot> slotList = new List<InventorySlot>();    // 슬롯 리스트

    [Tooltip("이름순으로 슬롯 정렬")]
    public bool sortByName = true;

    [Space(10f)]
    [Tooltip("아이템 없을 경우 표시 될 아이콘")]
    public Sprite defaultIcon;

    [Tooltip("아이템 이벤트 핸들러")]
    public ItemHandler itemHandler;             

    public InventoryTab LastRefreshedTab { get; set; }        //최근에 새로고침된 탭

    private void Awake()
    {
        if (!ManagerList.Contains(this)) ManagerList.Add(this);
    }

    private void Start()
    {
        SlotSort();     //최초정렬
    }

    // 슬롯 이름 순으로 정렬 및 인덱스 지정
    public void SlotSort()
    {
        
        if (sortByName)
        { 
            slotList.Sort((InventorySlot x, InventorySlot y) => x.name.CompareTo(y.name));
        for (int i = 0; i < slotList.Count; i++)  //####
            slotList[i].Index = i;
        }
    }

    //아이템 테이블 새로고침 ( 탭 아이템 리스트 시각화)
    public void Refresh(InventoryTab tab)
    {
        if (tab.Capacity > slotList.Count)
        {
            Debug.Log("탭의 아이템 수용량이 슬롯 수를 초과합니다. : " + tab.TabName);
            return;
        }

        //슬롯 아이템 및 현재 탭 설정
        for (int i = 0; i < tab.Capacity; i++)
        {
            slotList[i].SetSlot(tab.ItemTable[i]); 
        }

        LastRefreshedTab = tab;

    }

    //모든 아이템 테이블 마지막 탭으로 새로고침
    public static void RefreshAll()
    {
        for (int i = 0; i < ManagerList.Count; i++)
        {
            if (ManagerList[i].LastRefreshedTab != null)     
                ManagerList[i].Refresh(ManagerList[i].LastRefreshedTab);
        }
    }

    //모든 슬롯의 허용 타입 설정
    public void SetSlotType (string type)
    {
        for (int i = 0; i < slotList.Count; i++) 
            slotList[i].allowType = type;
    }



}
