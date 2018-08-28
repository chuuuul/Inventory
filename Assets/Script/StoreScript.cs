using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScript : MonoBehaviour {

    private GameObject UICanvas;

    private void Awake()
    {

        UICanvas = GameObject.FindWithTag("InvenUI");

        //UICanvas.SetActive(false);

    }


    private void OnTriggerEnter(Collider other)
    {
        UICanvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        UICanvas.SetActive(false);
    }
}

