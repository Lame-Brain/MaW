using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Sheet_Inventory_slot_Selector : MonoBehaviour
{
    public int value;
    public Character_Sheet host;

    public void OnCLick()
    {
        host.Inventory_Item_Clicked(value);
    }
}
