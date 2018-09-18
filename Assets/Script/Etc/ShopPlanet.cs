using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPlanet : MonoBehaviour
{
    
    public GameObject shopCanvas;

    public float rotateSpeed = 0.5f;

    private void Update()
    {

    }

    private void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * rotateSpeed;
        if (!shopCanvas)
        {
            Debug.Log("Shop Planet에 Shop Canvas 가 등록되지 않았습니다.");
            return;
        }
        shopCanvas.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {

        //shopCanvas.enabled = false;
        shopCanvas.SetActive (true);

    }

    private void OnTriggerExit(Collider other)
    {
        //shopCanvas.enabled = true;
        shopCanvas.SetActive(false);
    }



}
