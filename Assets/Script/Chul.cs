using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Chul : MonoBehaviour , IPointerUpHandler , IPointerDownHandler
{

    public Image chulImage;


    private GraphicRaycaster gr;
    private new Transform transform;
    private List<RaycastResult> raycastResult;
    private Vector2 originPosition;

    private void Awake()
    {
        
        gr = GetComponent<GraphicRaycaster>();
        raycastResult = new List<RaycastResult>();

        originPosition = new Vector2(0, 0);
        transform = chulImage.GetComponent<Transform>();

    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up Event");
        transform.position = originPosition;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        gr.Raycast(eventData, raycastResult);
        Debug.Log("Down event name : " + raycastResult[raycastResult.Count - 1].GetType() );
        originPosition = transform.position;
        transform.position = new Vector2(10, 10);
    }
}
