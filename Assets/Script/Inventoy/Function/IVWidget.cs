using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IVWidget : MonoBehaviour {
    
	
	public GameObject slotPrefab;               // 오리지널 슬롯
    public List <GameObject> TabList = new List<GameObject>(); // 슬롯을 생성할 탭들을 설정

    
    [Header("WidgetSizeEdit")]
    [Tooltip("초기 x,y 위치 ( 왼쪽 위 대각선 기준 )")]
    [Space(5)]
    public Vector2 initPosition = new Vector2(10, 10);

    [Tooltip("각 슬롯의 크기")]
    public Vector2 slotSize = new Vector2(75, 75);

    [Tooltip("각 슬롯 간의 거리")]
    public Vector2 slotGapDistance = new Vector2(10, 10);

    [Tooltip("가로 세로 슬롯 개수")]
    public Vector2 slotCount = new Vector2(5, 7);

    [Range(0.1f, 1.0f)]
    public float itemSizeRate = 0.8f;           // 아이템 사이즈 비율
    

    private float itemSizeX;					// 아이템 X 사이즈
	private float itemSizeY;					// 아이템 Y 사이즈
	
	private float invenWidth;                   // 인벤 가로 길이
	private float invenHeight;                  // 인벤 세로 길이
    private List<GameObject> allSlot = new List<GameObject>();      // 모든 슬롯이 담겨있는 리스트.      



    private void Awake()
	{
        itemSizeX = slotSize.x * itemSizeRate;
		itemSizeY = slotSize.y * itemSizeRate;
		MakeFrame();                            // 틀생성
    }

	void MakeFrame()
	{
		InvenSizeSetting();			// 인벤토리 Widget 설정

		float makeSlotX = initPosition.x;								// 만들 X 초기 위치
		float makeSlotY =  invenHeight - initPosition.y - slotSize.y;  // 만들 Y 초기 위치
        
		for (int y = 0; y < slotCount.y; y++)					// 가로세로에 슬롯 생성 
		{
			makeSlotX = initPosition.x;								// 다음줄 만들때 다시 x초기 위치
			for (int x = 0; x < slotCount.x; x++)
			{

                /*
                 ################### 슬롯 만드는 부분 삭제 ##########################
				GameObject slot = Instantiate(slotPrefab) as GameObject;    // 가로 x 세로 갯수만큼 슬롯 생성
                slot.GetComponent<InventorySlot>().slotManager = gameObject.GetComponent<SlotManager>();
                SlotSetting(slot,x,y,makeSlotX,makeSlotY);					// 슬롯 Setting
                

				RectTransform itemRect = slot.transform.GetChild(0).GetComponent<RectTransform>();
				SizeSetting(itemRect,itemSizeX,itemSizeY);		// 아이템의 사이즈 조절
				
				allSlot.Add(slot);								// 모든 슬롯 List에 추가
                */

                makeSlotX = makeSlotX + slotGapDistance.x + slotSize.x;	// 다음 칸 만들위치 설정( 이전위치 + 가로길이 + 간격 )

			}

			makeSlotY = makeSlotY - slotGapDistance.y - slotSize.y;		// 다음 칸 만들위치 설정 -( 이전위치 + 가로길이 + 간격 )

		}
    }
	void SlotSetting(GameObject slot,int x, int y,float makeSlotX,float makeSlotY)
	{
		RectTransform slotRect = slot.GetComponent<RectTransform>();

		slot.name = "Slot_X_" + x + "Y_" + y;								// 생성된 슬롯 이름 지정
		slot.transform.SetParent(this.transform);								// 부모 지정
        slotRect.localPosition = new Vector3(makeSlotX, makeSlotY, 0);		// 위치 지정

       

        SizeSetting(slotRect, slotSize.x, slotSize.y);						// 슬롯의 사이즈 조절
	}
	void SizeSetting(RectTransform objectRect,float setSizeX, float setSizeY)
	{
		objectRect.localScale = Vector3.one;                                              // scale 은 1 , 1 , 1 로 설정
		objectRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, setSizeX);   // 가로 사이즈 설정
		objectRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, setSizeY);     // 세로 사이즈 설정
	}
	void InvenSizeSetting()
	{
		invenWidth = (slotCount.x * slotSize.x) + ((slotCount.x + 1) * slotGapDistance.x);        // 인벤 가로 길이
		invenHeight = (slotCount.y * slotSize.y) + ((slotCount.y + 1) * slotGapDistance.y);       // 인벤 가로 길이
		SizeSetting(GetComponent<RectTransform>(), invenWidth, invenHeight);
	}
}
