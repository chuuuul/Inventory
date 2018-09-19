using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    
    public Canvas shopUI;

    public float rotateSpeed = 0.5f;

    private CanvasGroup shopUIGroup;

    private void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * rotateSpeed;
        
        if (shopUI == null)
        {
            Debug.Log("Shop UI 가 지정되지 않았습니다");
            return;
        }

        shopUIGroup = shopUI.GetComponent<CanvasGroup>();
        ShopToggle(false);
    }

    // toggle : 1 일 때 상점 on
    private void ShopToggle( bool toggle )
    {
        if (toggle)
            shopUIGroup.alpha = 1;
        else
            shopUIGroup.alpha = 0;

        shopUIGroup.interactable = toggle;
        shopUIGroup.blocksRaycasts = toggle;
    }


    // 가까우면 상점 키고
    private void OnTriggerEnter(Collider other)
    {
        ShopToggle(true);
    }

    // 멀어지면 상점 끄고
    private void OnTriggerExit(Collider other)
    {
        ShopToggle(false);
    }



}
