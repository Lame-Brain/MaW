using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Sheet_Item_Info : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Panel_Name, Item_Info;

    public void UpdatePanel(Item thisItem)
    {
        Panel_Name.text = thisItem.ItemName();
        Item_Info.text = "";
        if (!thisItem.Identified())
        {
            Item_Info.text += "This item has not yet been identified.\n\n";
            if (thisItem.Type() == "Equip")
            {
                Item_Info.text += "This item can be equipped to the " + thisItem.Slot() + " slot.\n\n";
                if (thisItem.Slot() == "Weapon") Item_Info.text += "You are not sure how much damage this item will deal. Get it identified for more information.\n\n";
                if (thisItem.Slot() != "Weapon") Item_Info.text += "You are not sure how much this item would protect you. Get it identified for more information.\n\n";
            }
            if (thisItem.Type() != "Equip")
            {
                Item_Info.text += "There is no way to tell what this item does. Get it identified for more information.\n\n";
            }
        }
        if (thisItem.Identified())
        {
            if (thisItem.Type() != "Consume" && thisItem.Curse() > 0) Item_Info.text += "This item is CURSED!\n\n";
            if (thisItem.Type() != "Consume" && thisItem.Curse() < 1 && thisItem.Magic() > 0) Item_Info.text += "This item is MAGICAL!\n\n";
            if (thisItem.Type() != "Consume") Item_Info.text += "This item can be equipped to the " + thisItem.Slot() + " slot.\n\n";
            if (thisItem.Min_Damage() > 0 || thisItem.Max_Damage() > 0) Item_Info.text += "This weapon does between " + thisItem.Min_Damage() + " and " + thisItem.Max_Damage() + " damage.\n\n";
            if (thisItem.AC() > 0) Item_Info.text += "This equipment adds " + thisItem.AC() + " Armor.\n\n";

            if (thisItem.Special() == "Fire_Dam") Item_Info.text += "Adds " + thisItem.Magic() + " Fire Damage to every hit.\n\n";
            if (thisItem.Special() == "Ice_Dam") Item_Info.text += "Adds " + thisItem.Magic() + " Ice Damage to every hit.\n\n";
            if (thisItem.Special() == "Shock_Dam") Item_Info.text += "Adds " + thisItem.Magic() + " Shock Damage to every hit.\n\n";
            if (thisItem.Special() == "Undead_Dam") Item_Info.text += "Adds " + thisItem.Magic() + " extra Damage when attacking Undead.\n\n";
            if (thisItem.Special() == "Dragon_Dam") Item_Info.text += "Adds " + thisItem.Magic() + " extra Damage when attacking Dragons.\n\n";
            if (thisItem.Special() == "Were_Dam") Item_Info.text += "Adds " + thisItem.Magic() + " extra Damage when attacking Were Creatures.\n\n";
            if (thisItem.Special() == "Mage_Dam") Item_Info.text += "Adds " + thisItem.Magic() + " extra Damage when attacking Wizards.\n\n";
            if (thisItem.Special() == "STN_HIT") Item_Info.text += "Chance to Magically Stun any target hit. \n\n";
            if (thisItem.Special() == "Cast_Fire1") Item_Info.text += "Adds 1 to 8 points of Fire Damage to every hit.\n\n";
            if (thisItem.Special() == "Cast_Ice1") Item_Info.text += "Adds 1 to 8 points of Ice Damage to every hit.\n\n";
            if (thisItem.Special() == "Cast_Shock1") Item_Info.text += "Adds 1 to 8 points of Shock Damage to every hit.\n\n";
            if (thisItem.Special() == "ADD_LIGHT") Item_Info.text += "Adds " + thisItem.Magic() + " to Light.\n\n";
            if (thisItem.Special() == "ADD_HP") Item_Info.text += "Restores " + thisItem.Magic() + " points of HP.\n\n";
            if (thisItem.Special() == "Cast_Heal1") Item_Info.text += "Restores between 1 and 8 points of HP.\n\n";
            if (thisItem.Special() == "Cast_Heal2") Item_Info.text += "Restores between 2 and 16 points of HP.\n\n";
            if (thisItem.Special() == "Cast_Heal3") Item_Info.text += "Restores between 5 and 25 points of HP.\n\n";
            if (thisItem.Special() == "Cast_Heal4") Item_Info.text += "Restores between 15 and 35 points of HP.\n\n";
            if (thisItem.Special() == "Cast_Cure1") Item_Info.text += "Cures magical Blindness, Weakness, Frailty, and Slowness.\n\n";
            if (thisItem.Special() == "Cast_Cure2") Item_Info.text += "Cures Poison and Stunning, can also restore a Caster's connection to magic.\n\n";
            if (thisItem.Special() == "Cast_Cure3") Item_Info.text += "Cures Magical Paralysis and can restore Stone to Flesh.\n\n";
            if (thisItem.Special() == "Cast_Cure4") Item_Info.text += "Cures all conditions including Death.\n\n";
            if (thisItem.Special() == "STR_MOD") Item_Info.text += "Temporarily increases Strength by " + thisItem.Magic() + ".\n\n";
            if (thisItem.Special() == "DEX_MOD") Item_Info.text += "Temporarily increases Dexterity by " + thisItem.Magic() + ".\n\n";
            if (thisItem.Special() == "FORT_MOD") Item_Info.text += "Temporarily increases Fortitude by " + thisItem.Magic() + ".\n\n";
            if (thisItem.Special() == "IQ_MOD") Item_Info.text += "Temporarily increases IQ by " + thisItem.Magic() + ".\n\n";
            if (thisItem.Special() == "WIS_MOD") Item_Info.text += "Temporarily increases Wisdom by " + thisItem.Magic() + ".\n\n";
            if (thisItem.Special() == "CHRM_MOD") Item_Info.text += "Temporarily increases Charm by " + thisItem.Magic() + ".\n\n";
            if (thisItem.Special() == "DEF_MOD") Item_Info.text += "Temporarily increases Armor by " + thisItem.Magic() + ".\n\n";
            if (thisItem.Special() == "ATTK_MOD") Item_Info.text += "Temporarily increases Attack Bonus by " + thisItem.Magic() + ".\n\n";
            if (thisItem.Special() == "DAM_MOD") Item_Info.text += "Temporarily increases Damage Bonus by " + thisItem.Magic() + ".\n\n";
            if (thisItem.Special() == "REGEN_MOD") Item_Info.text += "Temporarily Regenerates HP every turn.\n\n";
            if (thisItem.Special() == "REGEN") Item_Info.text += "Temporarily Regenerates HP every turn.\n\n";
            if (thisItem.Special() == "Fire_Resist") Item_Info.text += "Temporarily Increases Fire Resistance.\n\n";
            if (thisItem.Special() == "Ice_Resist") Item_Info.text += "Temporarily Increases Ice Resistance.\n\n";
            if (thisItem.Special() == "Shock_Resist") Item_Info.text += "Temporarily Increases Shock Resistance.\n\n";
            if (thisItem.Special() == "Town_Portal") Item_Info.text += "Allows immeadiate return to the last town visited.\n\n";
            if (thisItem.Special() == "Curse_ManaBurn") Item_Info.text += "Curses the Wielder to be cut off from Magic.\n\n";                
            if (thisItem.Special() == "Blind_Curse") Item_Info.text += "Curses the Wielder to be Blind.\n\n";                
            if (thisItem.Special() == "Light_Curse") Item_Info.text += "Absorbs the Party's light, leaving them in darkness.\n\n";                
            if (thisItem.Special().Contains("Extra_Spell")) Item_Info.text += "Gives the Wielder extra spell slots.\n\n";
            if (thisItem.Special() == "AttkX2") Item_Info.text += "Gives an Extra Attack to the wielder.\n\n";
            if (thisItem.Special() == "AttkX5") Item_Info.text += "Gives 5 Extra Attacks to the wielder.\n\n";
            if (thisItem.Special() == "Haste") Item_Info.text += "Empowers the wielder with Haste.\n\n";
            if (thisItem.Special() == "Slow") Item_Info.text += "Inflicts the wielder with Slow.\n\n";
            if (thisItem.Special() == "Cast_Levitate") Item_Info.text += "This scroll allows the Party to float over obstacles and hazards.\n\n";
            if (thisItem.Special() == "Cast_Ascend") Item_Info.text += "If underground, this scroll will move the Party up 1 level.\n\n";
            if (thisItem.Special() == "Add_XP") Item_Info.text += "When used, the user gains "+ thisItem.Magic() + " XP right away.\n\n";
            if (thisItem.Special() == "STR_PERM") Item_Info.text += "Permanently increases Strength by " + thisItem.Magic() + ".\n\n";
            if (thisItem.Special() == "DEX_PERM") Item_Info.text += "Permanently increases Dexterity by " + thisItem.Magic() + ".\n\n";
            if (thisItem.Special() == "FORT_PERM") Item_Info.text += "Permanently increases Fortitude by " + thisItem.Magic() + ".\n\n";
            if (thisItem.Special() == "IQ_PERM") Item_Info.text += "Permanently increases IQ by " + thisItem.Magic() + ".\n\n";
            if (thisItem.Special() == "WIS_PERM") Item_Info.text += "Permanently increases Wisdom by " + thisItem.Magic() + ".\n\n";
            if (thisItem.Special() == "CHRM_PERM") Item_Info.text += "Permanently increases Charm by " + thisItem.Magic() + ".\n\n";

            Item_Info.text += "\nThis item is worth " + thisItem.Value() + " gp.";
        }
    }
}
