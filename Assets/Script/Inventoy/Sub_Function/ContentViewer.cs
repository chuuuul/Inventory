using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// 상속하여 이벤트 및 내용 처리
/// </summary>

[RequireComponent (typeof(CanvasGroup))]
public abstract class ContentViewer : MonoBehaviour {

    public enum ViewerAnchor { TopLeft, TopRight, BottomLeft, BottomRight }
    public enum ViewerStandard {  Slot, Cursor }

    [Header("Viewer Setting")]
    public RectTransform viewer; //뷰어 (이동용)
    public CanvasGroup group;   // 캔버스 그룹 ( 페이드용도 )

    [Space(8f)]
    public ViewerAnchor anchor = ViewerAnchor.TopLeft;      //뷰어 정렬
    public ViewerStandard standard = ViewerStandard.Slot;   //뷰어 위치 기준

    [Space(8f)]
    public float displayDelay = 0.5f;   // 커서를 올리고 뷰어가 나타나는 시간
    public float disappearDelay = 0.2f; // 포인터가 나갈 때 뷰어가 사라지는 시간
    public float fadeDuration = 0.1f;   // 캔버스 그룹이 페이드 되는 시간
    public bool blockRaycast = false;

    public bool isEnabled { get; private set; } = false;
    private IEnumerator displayViewer;
    private IEnumerator disappearViewer;
    private Vector3 viewerPos;

    private void Awake()
    {
        if (viewer == null ) viewer = GetComponent<RectTransform>();
        if (group == null) group = GetComponent<CanvasGroup>();
        if (!viewer.gameObject.activeSelf) viewer.gameObject.SetActive(true);   //뷰어 활성화

        //캔버스 그룹 초기 설정
        group.alpha = 0;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

    private void Start()
    {
        EventCall();
    }

    //뷰어 활성화
    public void ViewerEnable(InventorySlot slot)
    {
        if (disappearViewer == null)              // #### disappearViewer == 0이랑 같은 문법인가??
            StopCoroutine(disappearViewer);
        displayViewer = DisplayViewer(slot);
        StartCoroutine(displayViewer);
    }

    IEnumerator DisplayViewer(InventorySlot slot)
    {
        yield return null;

    }


    //추상 메서드
    protected abstract void EventCall(); //이벤트 할당 메서드
    protected abstract void OnDisplay(PointerEventData eventData, InventorySlot slot); //활성화 이벤트
    protected abstract void OnDisappear(PointerEventData eventData, InventorySlot slot); //비활성화 이벤트
    protected abstract void DrawContent(InventorySlot slot); //뷰어 내용 그리기 메서드
}
