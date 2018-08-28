using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour {

   /*
   아이템코드
   1: HP
   2: MP
   */

    public int min = 1;
    public int max = 3;
    public int itemCode;            // 아이템의 고유 코드
    public Sprite ItemSprite;       // 아이템의 이미지

    void GetItem()
    {
        bool isBlank = IVManager.invenManager.GetItem(min, max, this);
        if(isBlank == false)                        // 가득 차있다면 ( 인벤메니저에서 확인 )
            Debug.Log("가득 찼습니다.");              // 메세지만 띄우고
        else
        {
            gameObject.SetActive(false);            // 공간이 있다면, 아이템 먹는다 (비활성).
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player")       // 플레이이어일 때 만 실행
            return;
        GetItem();

    }

}
