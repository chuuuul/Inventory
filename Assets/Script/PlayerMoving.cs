using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float speed = 5.0f;
    
    private void Update()
	{
        MovePlayer();




        //if (TabManager.GetTab(0).ItemTable[19] is Consum)
        //    Debug.Log(((Consum)TabManager.GetTab(0).ItemTable[19]).SellPrice);
        //    //Debug.Log("consum");

        //if (TabManager.GetTab(0).ItemTable[19] is CommonItem)
        //    Debug.Log("common");
        ////Last Cv = new Last();
        ////Cv.AAA();
        ////Debug.Log(Cv.Must);
        ////Debug.Log(Cv.Have);
        ////Debug.Log(Cv.Hello);
        ////Debug.Log(Cv.aa);
        ////Debug.Log(Cv.bb);
        ////Debug.Log("----------");

        ////Hi QQ = Cv as Hi;

        ////Debug.Log(QQ.Hello);
        ////Debug.Log(QQ.Have);
        ////Debug.Log(QQ.aa);
        ////Debug.Log(QQ.bb);

    }

	void MovePlayer()
	{
		float inputX, inputY;

		inputX = Input.GetAxis("Horizontal");
		inputY = Input.GetAxis("Vertical");
		
		
		GetComponent<Rigidbody>().velocity = new Vector3(inputX, inputY, 0) * speed;

	}

}



public class Last : Hi,IHi
{
    public int Must { get; set; }
    public int Have { get; set; }
    public int Hello = 111;

    public void AAA()
    {
        Must = 1;
        Have = 2;
    }

}

public class Hi 
{
    public int Must { get; set; }
    public int Have { get; set; }
    public int Hello;

    public int aa = 3;
    public int bb = 4;


}

public interface IHi
{

    int Must {get; set;}
    int Have { get; set; }
}
