using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Sheet_Spell_slot_Selector : MonoBehaviour
{
    public int value;
    public Character_Sheet_Spell_List_Panel host;

    public void OnCLick()
    {
        host.Spell_Index_Clicked(value);
    }
}
