using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Sheet : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Name, Stats, Weapon_Slot, Armor_Slot, Shield_Slot, RFinger_Slot, LFinger_Slot, Helm_Slot, Neck_Slot, Cloak_Slot;
    public GameObject Blessed, Blinded, Weakened, Slowed, Frail, ManaBurn, Poisoned, Stunned, Paralyzed, Stone, Dead, Ash;


    public void Update_Sheet(Character_C _who = null)
    {
        if(_who != null)
        {
            Name.text = _who.Character_Name;
            Stats.text = "Level " + _who.XP_Level + " " + _who.Character_Class + " with " + _who.XP + " XP\n" +
                "\n" +
                "[Strength] " + _who.Strength.Value() + "\n" +
                "[Dexterity] " + _who.Dexterity.Value() + "\n" +
                "[Fortitude] " + _who.Fortitude.Value() + "\n" +
                "[IQ] " + _who.IQ.Value() + "\n" +
                "[Wisdom] " + _who.Wisdom.Value() + "\n" +
                "[Charm] " + _who.Charm.Value() + "\n" +
                "[Init Bonus]" + (_who.Init_Bonus > 0 ? " +" : " ") + _who.Init_Bonus + "\n" +
                "[Attack]" + (_who.Attack_Bonus > 0 ? " +" : " ") + _who.Attack_Bonus + "\n" +
                _who.Min_Damage + " to " + _who.Max_Damage + " ( x" + _who.Num_Attacks + " )\n" +
                "[AC] " + _who.AC;
            Weapon_Slot.text = _who.Weapon_Slot == null ? "Weapon Slot" : _who.Weapon_Slot.ItemName();
            Armor_Slot.text = _who.Armor_Slot == null ? "Armor Slot" : _who.Armor_Slot.ItemName();
            Shield_Slot.text = _who.Shield_Slot == null ? "Shield Slot" : _who.Shield_Slot.ItemName();
            RFinger_Slot.text = _who.RightFinger_Slot == null ? "Right Finger Slot" : _who.RightFinger_Slot.ItemName();
            LFinger_Slot.text = _who.LeftFinger_Slot == null ? "Left Finger Slot" : _who.LeftFinger_Slot.ItemName();
            Helm_Slot.text = _who.Head_Slot == null ? "Helm Slot" : _who.Head_Slot.ItemName();
            Neck_Slot.text = _who.Neck_Slot == null ? "Neck Slot" : _who.Neck_Slot.ItemName();
            Cloak_Slot.text = _who.Cloak_Slot == null ? "Cloak Slot" : _who.Cloak_Slot.ItemName();

            Blessed.SetActive(_who.Blessed); Blinded.SetActive(_who.Blind); Weakened.SetActive(_who.Weak); Slowed.SetActive(_who.Slow); Frail.SetActive(_who.Frail); 
            ManaBurn.SetActive(_who.ManaBurn); Poisoned.SetActive(_who.Poison); Stunned.SetActive(_who.Stun); Paralyzed.SetActive(_who.Paralyze);
            Stone.SetActive(_who.Stone); Dead.SetActive(_who.Dead); Ash.SetActive(_who.Ash);
        }
    }
}
