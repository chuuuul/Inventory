using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SlotScript : MonoBehaviour,IPointerUpHandler,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler {

    public Stack<ItemScript> itemStack; // 아이템을 넣을 스택
    
    public Sprite blankItemImage;       // 빈 이미지
    public float fontSizeRate = 0.3f;   // 글자 사이즈 비율 ( 슬롯크기와 비례 )

    internal ItemHandler itemHandler;

    private Text text;                  // 개수 표현 할 텍스트
    private bool isItemBlank;            // 아이템이 비어있는지 // true : 비어있음 // false : 들어있음

    [HideInInspector] public Image itemImage;   // 들어갈 아이템의 이미지
    [HideInInspector] public int itemCode = 0 ; // 아이템 코드 저장


    public bool IsItemBlank_F() { return isItemBlank; }


    void Start()
    {
        setSlot();
        itemHandler = 

    }

    public void MakeTarget ()
    {
        

    }


    // 기본창 세팅
    private void setSlot()
    {
        itemStack = new Stack<ItemScript>();          // 비어있는 스택으로 초기화
        itemImage = transform.GetChild(0).GetComponent<Image>();    // 0번째 자식이 이미지 오브젝트이고
        text = this.transform.GetChild(1).GetComponent<Text>();     // 1번째 자식이 Text 오브젝트이다.

        text.text = "";                             // 처음엔 공백

        // 글자크기를 슬롯 크기에 따라 변경
        float size = this.GetComponent<RectTransform>().sizeDelta.x;
        text.fontSize = (int)(size * fontSizeRate);

        isItemBlank = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("ww");
        itemHandler.OnSlotUp.Invoke(eventData, this);
        
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("QQ");

        }


    }


    // 아이템 사용
    public void UseItem(ItemScript item)
    {
        if (isItemBlank == true)           //비어있으면 아무것도 하지않음
            return;

        if (itemStack.Count == 1)           // 1개 남아 있으면 칸을 비워버린다.
        {
            itemStack.Clear();
            UpdateInfo(true,blankItemImage,0);
            return;
        }

        itemStack.Pop();
        UpdateInfo(false, item.ItemSprite, item.itemCode);

          
    }

    // 아이템 획득
    public void GetItem(ItemScript item)
    {
        itemStack.Push(item);
        itemCode = item.itemCode;
        UpdateInfo(false, item.ItemSprite, item.itemCode);
    }

    // 정보 업데이트
    void UpdateInfo(bool isItemBlank_Input, Sprite sprite_Input, int itemCode)
    {
        isItemBlank = isItemBlank_Input;                // 비었는지를 입력받은것으로 넣고
        itemImage.sprite = sprite_Input;                // 이미지를 입력받은 이미지 넣는다
        if (itemStack.Count > 1)
            text.text = itemStack.Count.ToString();     // 해당 개수가 1보다 많다면 Text 추가
        else
        { 
            text.text = "";                             // 1 개라면 숫자 Text 출력 안함
            itemCode = 0;
        }
        // ItemIO.saveDate();                           // 인벤토리에 변동생겼으니 저장?
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("ZZZ");
    }
}
