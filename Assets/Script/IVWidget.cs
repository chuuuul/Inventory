using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IVWidget : MonoBehaviour {
    
	
	public GameObject slotPrefab;               // 오리지널 슬롯


    public float initGapX = 10;					// 맨 처음 X 위치
	public float initGapY = 10;					// 맨 처음 Y 위치
	public float slotSizeX = 75;                // 슬롯 Y사이즈
	public float slotSizeY = 75;                // 슬롯 X사이즈
	public float slotGapX = 10;		            // 슬롯 X 간격
	public float slotGapY = 10;                 // 슬롯 Y 간격
	public int slotCountX = 5;                  // 슬롯 가로 개수
	public int slotCountY = 7;                  // 슬롯 세로 개수
	public float itemSizeRate = 0.8f;			// 아이템 사이즈 비율


	private float itemSizeX;					// 아이템 X 사이즈
	private float itemSizeY;					// 아이템 Y 사이즈
	
	private float invenWidth;                   // 인벤 가로 길이
	private float invenHeight;                  // 인벤 세로 길이
    private List<GameObject> allSlot = new List<GameObject>();      // 모든 슬롯이 담겨있는 리스트.      

    void Start()
	{
        itemSizeX = slotSizeX * itemSizeRate;
		itemSizeY = slotSizeY * itemSizeRate;
		
		MakeFrame();                            // 틀생성
        IVManager.invenManager.allSlot = this.allSlot;
    }

	void MakeFrame()
	{
		InvenSizeSetting();			// 인벤토리 Widget 설정

		float makeSlotX = initGapX;								// 만들 X 초기 위치
		float makeSlotY =  invenHeight - initGapY - slotSizeY;  // 만들 Y 초기 위치
        
		for (int y = 0; y < slotCountY; y++)					// 가로세로에 슬롯 생성 
		{
			makeSlotX = initGapX;								// 다음줄 만들때 다시 x초기 위치
			for (int x = 0; x < slotCountX; x++)
			{
				GameObject slot = Instantiate(slotPrefab) as GameObject;	// 가로 x 세로 갯수만큼 슬롯 생성
				SlotSetting(slot,x,y,makeSlotX,makeSlotY);					// 슬롯 Setting

				RectTransform itemRect = slot.transform.GetChild(0).GetComponent<RectTransform>();
				SizeSetting(itemRect,itemSizeX,itemSizeY);		// 아이템의 사이즈 조절
				
				allSlot.Add(slot);								// 모든 슬롯 List에 추가

				makeSlotX = makeSlotX + slotGapX + slotSizeX;	// 다음 칸 만들위치 설정( 이전위치 + 가로길이 + 간격 )

			}

			makeSlotY = makeSlotY - slotGapY - slotSizeY;		// 다음 칸 만들위치 설정 -( 이전위치 + 가로길이 + 간격 )

		}//IVManager.invenManager.blankSlotCount = allSlot.Count;							// 모든 칸 비어있다.
    }
	void SlotSetting(GameObject slot,int x, int y,float makeSlotX,float makeSlotY)
	{
		RectTransform slotRect = slot.GetComponent<RectTransform>();
		slot.name = "Slot_X_" + x + "Y_" + y;								// 생성된 슬롯 이름 지정
		slot.transform.SetParent(this.transform);								// 부모 지정
		slotRect.localPosition = new Vector3(makeSlotX, makeSlotY, 0);		// 위치 지정

		SizeSetting(slotRect, slotSizeX, slotSizeY);						// 슬롯의 사이즈 조절
	}
	void SizeSetting(RectTransform objectRect,float setSizeX, float setSizeY)
	{
		objectRect.localScale = Vector3.one;                                              // scale 은 1 , 1 , 1 로 설정
		objectRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, setSizeX);   // 가로 사이즈 설정
		objectRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, setSizeY);     // 세로 사이즈 설정
	}
	void InvenSizeSetting()
	{
		invenWidth = (slotCountX * slotSizeX) + ((slotCountX + 1) * slotGapX);        // 인벤 가로 길이
		invenHeight = (slotCountY * slotSizeY) + ((slotCountY + 1) * slotGapY);       // 인벤 가로 길이
		SizeSetting(GetComponent<RectTransform>(), invenWidth, invenHeight);
	}
}
