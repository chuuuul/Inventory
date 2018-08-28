using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CircleDragScript : MonoBehaviour , IBeginDragHandler, IEndDragHandler, IDragHandler
{
	public static Vector2 defaultposition;			


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}



	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		defaultposition = this.transform.position;
	}

	void IDragHandler.OnDrag(PointerEventData eventData)
	{
		Vector2 currentPostion = Input.mousePosition;
		this.transform.position = currentPostion;

	}

	void IEndDragHandler.OnEndDrag(PointerEventData eventData)
	{
		this.transform.position = defaultposition;

	}


}
