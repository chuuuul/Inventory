using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragImageScript : MonoBehaviour
{


    public Vector2 trackerSize = new Vector2(60,600);             //트래킹 이미지 사이즈
    public Color trackerColor = Color.white;              // 트래킹 이미지 색상

    private Image trackingImage;            // 트래킹 시 이미지
    private GameObject tracker;
    private RectTransform tracker_rt;       // Tracker 의 RectTransfrom
    private SlotScript slot;
    

    // 트래킹 할 이미지 오브젝트 생성
    void Start ()
    {
        tracker = new GameObject("PointerTracker", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        trackingImage = tracker.GetComponent<Image>();
        trackingImage.color = trackerColor;
        trackingImage.raycastTarget = false;

        tracker_rt = tracker.GetComponent<RectTransform>();
        tracker_rt.sizeDelta = trackerSize;
        tracker_rt.gameObject.SetActive(false);
    }
}
