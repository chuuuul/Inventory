﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float speed = 5.0f;

    private void Update()
	{

        MovePlayer();
    }

	void MovePlayer()
	{
		float inputX, inputY;

		inputX = Input.GetAxis("Horizontal");
		inputY = Input.GetAxis("Vertical");
		
		
		GetComponent<Rigidbody>().velocity = new Vector3(inputX, inputY, 0) * speed;

	}

    private void LateUpdate()
    {
        foreach (var i in TabManager.GetTab("ItemTable").ItemTable)
        {
            if (i == null)
                return;
            Debug.Log("table  name : " + i.Name);
        }
    }

}
