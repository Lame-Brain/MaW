using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Sheet_Spell_Info : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Panel_Name, Spell_Info;

    public void UpdatePanel(Spell_C thisSpell)
    {
        Panel_Name.text = thisSpell.spellName;
        Spell_Info.text = "";
        if (thisSpell.SpellCLass == "Arcane" && thisSpell.spellSource == "") Spell_Info.text = "This is an Arcane Spell\n\n";
        if (thisSpell.SpellCLass == "Divine" && thisSpell.spellSource == "") Spell_Info.text = "This is a Divine Spell\n\n";
        if (thisSpell.SpellCLass == "Arcane" && thisSpell.spellSource != "") Spell_Info.text = "This is an Arcane Spell that draws on the " + thisSpell.spellSource + " element.\n\n";
        if (thisSpell.SpellCLass == "Divine" && thisSpell.spellSource != "") Spell_Info.text = "This is a Divine Spell that draws on the " + thisSpell.spellSource + " element.\n\n";

        Spell_Info.text += thisSpell.spellDescriptor + "\n\n";

        if (thisSpell.battle) Spell_Info.text += "This spell can be used in Combat \n";
        if (thisSpell.utility) Spell_Info.text += "This spell can be out of Combat \n";

        if (thisSpell.spellAlignment != "") Spell_Info.text += "\nThis spell targets: " + thisSpell.spellAlignment + ", " + thisSpell.spellTarget;
    }
}
