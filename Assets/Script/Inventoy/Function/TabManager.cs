using System;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    [Serializable]
    public class TabProperty
    {
        public string tabName;          // 탭 이름
        public int capacity;            // 탭 아이템 수용량
    }

    [Tooltip("탭 리스트 (최소 하나 이상의 탭 필요)")]
    public List<TabProperty> tabList = new List<TabProperty>();

    private static List<string> tabNameList = new List<string>();   // 탭 이름 리스트
    private static Dictionary<string, InventoryTab> tabDistionary 
                        = new Dictionary<string, InventoryTab>();   // 탭 딕셔너리


    private void Awake()
    {
        for (int i = 0; i < tabList.Count; i++)
        {
            tabNameList.Add(tabList[i].tabName);
            // 탭 이름 리스트, 딕셔너리에 탭 추가
            tabDistionary.Add(tabList[i].tabName, new InventoryTab(tabList[i].tabName, tabList[i].capacity));
            
        }
    }


    // 탭 이름으로 탭 접근
    public static InventoryTab GetTab(string tabName)          
    {
        if (tabNameList.Contains(tabName))
            return tabDistionary[tabName];
        else
        {
            Debug.LogError("인벤토리 탭 : 정의되지 않은 인벤토리 탭입니다.");
            return null;
        }
    }

    // 인덱스로 탭 접근
    public static InventoryTab GetTab(int index)
    {
        if ( (index >= 0) && (index <= tabNameList.Count) )
            return tabDistionary[tabNameList[index]];
        else
        {
            Debug.LogError("인벤토리 탭 : 인덱스 범위를 초과하였습니다.");
            return null;
        }
    }
}

public class InventoryTab
{
    public string TabName { get; }                  // 탭 이름
    public int Capacity { get; private set; }       // 최대 수용량

    public List<SlotItem> ItemTable { get; private set; }   // 아이템 리스트




    // 생성자 탭 이름과 수용량 설정
    public InventoryTab(string tabName, int capacity)
    {
        TabName = tabName;
        Capacity = capacity;

        ItemTable = new List<SlotItem>(Capacity);
        for (int i = 0; i < Capacity; i++)
        {
            ItemTable.Add(null);
        }
    }


    // 아이템 인덱스로 아이템 접근 및 설정 (인덱서)
    public SlotItem this[int itemIndex]             
    {
        get { return ItemTable[itemIndex]; }
        set { ItemTable[itemIndex] = value; }
    }
    
    // 아이템 ID로 아이템 접근 (인덱서)     // 한가지만 반환
    public SlotItem this[string id]
    {
        get
        {
            for (int i = 0; i < ItemTable.Count; i++)
                if ((ItemTable[i] != null) && (id == ItemTable[i].Id))
                    return ItemTable[i];
            return null;
        }
    }

    //아이템 이름으로 아이템 반환 // List로 모두 반환
    public SlotItem[] GetItemByName(string itemName)
    {
        List<SlotItem> items = new List <SlotItem>();

        for (int i = 0; i < ItemTable.Count; i++)
        {
            
            if ((ItemTable[i] != null) && (itemName == ItemTable[i].Name)  )
            {
                items.Add(ItemTable[i]);
            }


        }
        return items.ToArray();

    }
    


    // 모든 아이템 리스트로 반환  // List로 모두 반환     // 아이템 저장용 
    public SlotItem[] GetItemAll()
    {
        List<SlotItem> Items = new List<SlotItem>();

        for (int i = 0; i < ItemTable.Count; i++)
        {
            if ((ItemTable[i] != null))
                Items.Add(ItemTable[i]);

        }
        return Items.ToArray();
    }

    
    //현재 아이템 개수
    public int Count
    {

        get
        {
            int count=0;
            for( int i = 0; i < ItemTable.Count; i++)
            {
                if (ItemTable[i] != null)
                    count++;
            }
            return count;
        }

    }

    // 비었는지 반환 true => 비었다
    public bool IsEmpty
    {
        get { return (Count == 0); }
    }

    //가득찼는지 여부 반환
    public bool IsFull
    {
        get { return(Count == Capacity); }
    }

    public int GetBlankIndex()
    {
        int index = 0;
        for(index = 0; index < ItemTable.Count; index++)
            if (ItemTable[index] == null)
                break;

        return index;
    }

    //리스트에 아이템 추가 (자동 병합)
    public void Add ( SlotItem item ,bool autoMerge = true, Action addFailEvent = null )
    {
        if ( item.MaxCount > 1 && autoMerge)
        {
            // targetItem : 합칠 대상의 인벤토리 // item : 추가할 아이템 정보
            SlotItem[] targetItems = GetItemByName(item.Name);

            //자동병합      
            for (int i = 0; i < targetItems.Length; i++ )
            {
                if (targetItems[i].MaxCount> targetItems[i].Count)        // 최대개수보다 적을 때 병합.
                {

                    int canAddAmount = targetItems[i].MaxCount - targetItems[i].Count;

                    // 한 슬롯에 다 병합 할 수 있으면 한번에 다 병합
                    if ( canAddAmount > item.Count)
                    {
                        targetItems[i].Count = targetItems[i].Count + item.Count;
                        item.Count = 0;
                    }
                    // 한번에 못하면 canAddAmount 만큼 병합
                    else
                    {

                        targetItems[i].Count = targetItems[i].Count + canAddAmount;
                        item.Count = item.Count - canAddAmount;
                    }
                        
                }
                if (item.Count <= 0)
                    return;
            }
        }

        // 병합할대상이 없을 때 아이템 추가 //#### 문제점 새로운 슬롯에 7개를먹으면???
        if (!IsFull)
        {
            item.Tab = this;
            int index = GetBlankIndex();
            ItemTable[index] = item;
            item.Index = index;
        }
        else
            addFailEvent?.Invoke();
    }
    //리스트에 아이템 추가 (인덱스 직접 설정, 저장소 불러오기 용도)
    public void Add( SlotItem item, int index)
    {
        item.Tab = this;
        ItemTable[index] = item;
        item.Index = index;
    }

    //리스트에서 아이템 제거
    public void Remove( SlotItem item)
    {
        if (ItemTable.Contains(item))
        {
            ItemTable[item.Index] = null;
        }
    }
    
    //리스트 확장
    public void Extend ( int capacity)
    {
        ItemTable.Capacity += capacity;

        for(int i = 0; i < capacity; i++)
        {
            ItemTable.Add(null);
        }
        Capacity += capacity;

    }


}

