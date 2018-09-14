using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public SlotManager myInventory;

    private void Start()
    {
        myInventory.Refresh(TabManager.GetTab("FirstTab"));
        
    }
}
