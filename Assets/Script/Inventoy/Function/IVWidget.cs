using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class IVWidget : MonoBehaviour {

    [Serializable]
    public class WidgetProperty
    {
        public GameObject slotPrefab;               // 오리지널 슬롯

        [Header("WidgetSizeEdit")]
        [Tooltip("왼쪽 위 모서리에서 띄어질 간격")]
        [Space(5)]
        public Vector2 firstPosition = new Vector2();

        
        [Tooltip("각 슬롯 간의 거리")]
        public Vector2 slotGapDistance = new Vector2();

        [Tooltip("가로 세로 슬롯 개수")]
        public Vector2 slotCount = new Vector2();

        [Tooltip("etc 추가 길이 ( ex 상점 Title 영역 / 돈 표시 영역 합한 크기")]
        public Vector2 etcSize = new Vector2();

        [Tooltip("가로 세로 최소 아이템개수 (길이로 계산하여 Widget 최소크기 지정)")]
        public Vector2 leastItemCount = new Vector2();
        [HideInInspector]
        public Vector2 invenSize;                   // 인벤 가로 길이


        [HideInInspector]
        public Vector2 slotSize;                    // 프레펩 슬롯 사이즈

    }


    public List<WidgetProperty> widgetList = new List<WidgetProperty>();

    private RectTransform objectRect;


    private void Awake()
	{
        objectRect = GetComponent<RectTransform>();

        for (int i = 0; i < widgetList.Count; i++)
        {
            
            if (widgetList[i].slotPrefab == null)
                Debug.Log("slot prefab을 등록하세요");
            
            // 슬롯 프레펩 사이즈 가져옴
            widgetList[i].slotSize = widgetList[i].slotPrefab.GetComponent<RectTransform>().sizeDelta;

            InvenSizeSetting(i);			// 인벤토리 Widget 설정
        }

        
    }

    private void Start()
    {
        MakeFrame(0);    // 틀생성
    }


    private void InvenSizeSetting( int index )
    {
        // 최소 크기 지정
        if (widgetList[index].slotCount.x < widgetList[index].leastItemCount.x)
            widgetList[index].slotCount.x = widgetList[index].leastItemCount.x;

        if (widgetList[index].slotCount.y < widgetList[index].leastItemCount.y)
            widgetList[index].slotCount.y = widgetList[index].leastItemCount.y;


        // 인벤 가로 길이
        // {가로 슬롯수 x 슬롯 사이즈} + {(가로 슬롯수 - 1) x 슬롯 간격} + { 가로 처음값 x 2 } + etc가로
        widgetList[index].invenSize.x = (widgetList[index].slotCount.x * widgetList[index].slotSize.x) 
                                    + ( (widgetList[index].slotCount.x - 1 ) * widgetList[index].slotGapDistance.x ) 
                                    + widgetList[index].firstPosition.x * 2 + widgetList[index].etcSize.x;

        // 인벤 가로 길이
        widgetList[index].invenSize.y = (widgetList[index].slotCount.y * widgetList[index].slotSize.y) 
                                    + ((widgetList[index].slotCount.y - 1) * widgetList[index].slotGapDistance.y) 
                                    + widgetList[index].firstPosition.y * 2 + widgetList[index].etcSize.y;      
    }

    public void MakeFrame(int index)
    {
        objectRect.localScale = Vector3.one;                                              // scale 은 1 , 1 , 1 로 설정
        objectRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, widgetList[index].invenSize.x);   // 가로 사이즈 설정
        objectRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, widgetList[index].invenSize.y);     // 세로 사이즈 설정
        //size delta로 구현하면?

    }

}
