using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IVManager : MonoBehaviour {


    public static IVManager invenManager = null;        // 싱글턴
    public GameObject dragImageObject;                  // 드래그 했을 때 따라올 이미지 오브젝트

    [HideInInspector] public List<GameObject> allSlot;  // 모든 슬롯이 담겨있는 리스트.
    
    
    void Awake ()
    {
        if (invenManager == null)
            invenManager = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        dragImageObject.SetActive(false);
    }

    int FindBlankSlot()         // 빈 슬롯 찾는 함수 // return -1 : 빈칸 없음
    {
        for (int i = 0; i < allSlot.Count; i++)
        {
            if (allSlot[i].GetComponent<SlotScript>().itemCode == 0)
            {
                return i;
            }
        }
        return -1;
    }


    // 아이템 획득 함수 // 인벤이 꽉참 : return false
    public bool GetItem(int min , int max , ItemScript itemscript)          
    {
        int slotCount = allSlot.Count;               // 전체 슬롯 개수

        // 전체 슬롯 탐색
        for (int i = 0; i < slotCount; i++)         
        {
            SlotScript slotScript = allSlot[i].GetComponent<SlotScript>();

            if (slotScript.IsItemBlank_F() == true) continue;

            // 아이템 코드가 같고 //최대치보다 작을 때 // 해당 슬롯에 아이템 중첩
            else if ((itemscript.itemCode == slotScript.itemCode) && (slotScript.itemStack.Count < itemscript.max))
            {
                slotScript.GetItem(itemscript);    
                return true;                     
            }
        }

        // 전체 슬롯을 탐색하고나서
        // 빈칸을 찾고 없으면 false retunr
        int blankSlotNum = FindBlankSlot();
        if (blankSlotNum == -1)                
            return false;                       
        else                                    
        { 
            SlotScript slotScript = allSlot[blankSlotNum].GetComponent<SlotScript>();
            slotScript.GetItem(itemscript);     
            return true;                       
        }
        

    }

    

}
