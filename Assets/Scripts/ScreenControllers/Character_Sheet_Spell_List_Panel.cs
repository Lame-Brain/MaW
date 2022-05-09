using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Sheet_Spell_List_Panel : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text_line;
    public Transform root;
    public GameObject spell_info_panel;

    private bool head1, head2, head3, head4, head5;

    public void Update_Spell_List(Character_C _who)
    {
        foreach (GameObject _t in GameObject.FindGameObjectsWithTag("Text_Line")) if (_t.activeInHierarchy) Destroy(_t);

        //determine heading status
        head1 = false; head2 = false; head3 = false; head4 = false; head5 = false;

        //print out spells
        GameObject _go = null;
        int _numSpells = ResourceManager.MASTER_SPELL_LIST.Count;
        if (_who.SpellBook.Length < _numSpells) _numSpells = _who.SpellBook.Length;

        Debug.Log("Number of spells in book: " + _who.SpellBook.Length);
        Debug.Log("Number of spells in list: " + ResourceManager.MASTER_SPELL_LIST.Count);

                
        for (int i = 0; i < _numSpells; i++)
        {
            if(_who.SpellBook[i] && ResourceManager.MASTER_SPELL_LIST[i].spellCircle == 1 && !head1)
            {
                _go = Instantiate(text_line.gameObject, root);
                _go.SetActive(true);
                _go.GetComponent<TMPro.TextMeshProUGUI>().text = "-- Spell Circle 1 --";
                head1 = true;
            }

            if(_who.SpellBook[i] && ResourceManager.MASTER_SPELL_LIST[i].spellCircle == 2 && !head2)
            {
                _go = Instantiate(text_line.gameObject, root);
                _go.SetActive(true);
                _go.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                _go = Instantiate(text_line.gameObject, root);
                _go.SetActive(true);
                _go.GetComponent<TMPro.TextMeshProUGUI>().text = "-- Spell Circle 2 --";
                head2 = true;
            }

            if(_who.SpellBook[i] && ResourceManager.MASTER_SPELL_LIST[i].spellCircle == 3 && !head3)
            {
                _go = Instantiate(text_line.gameObject, root);
                _go.SetActive(true);
                _go.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                _go = Instantiate(text_line.gameObject, root);
                _go.SetActive(true);
                _go.GetComponent<TMPro.TextMeshProUGUI>().text = "-- Spell Circle 3 --";
                head3 = true;
            }

            if(_who.SpellBook[i] && ResourceManager.MASTER_SPELL_LIST[i].spellCircle == 4 && !head4)
            {
                _go = Instantiate(text_line.gameObject, root);
                _go.SetActive(true);
                _go.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                _go = Instantiate(text_line.gameObject, root);
                _go.SetActive(true);
                _go.GetComponent<TMPro.TextMeshProUGUI>().text = "-- Spell Circle 4 --";
                head4 = true;
            }

            if(_who.SpellBook[i] && ResourceManager.MASTER_SPELL_LIST[i].spellCircle == 5 && !head5)
            {
                _go = Instantiate(text_line.gameObject, root);
                _go.SetActive(true);
                _go.GetComponent<TMPro.TextMeshProUGUI>().text = "";
                _go = Instantiate(text_line.gameObject, root);
                _go.SetActive(true);
                _go.GetComponent<TMPro.TextMeshProUGUI>().text = "-- Spell Circle 5 --";
                head5 = true;
            }


            if (_who.SpellBook[i])
            {
                _go = Instantiate(text_line.gameObject, root);
                _go.SetActive(true);
                _go.GetComponent<TMPro.TextMeshProUGUI>().text = ResourceManager.MASTER_SPELL_LIST[i].spellName;
                _go.GetComponent<Character_Sheet_Spell_slot_Selector>().value = i;
            }
        }
    }

    public void Spell_Index_Clicked(int value)
    {
        spell_info_panel.SetActive(true);
        spell_info_panel.GetComponent<Character_Sheet_Spell_Info>().UpdatePanel(ResourceManager.MASTER_SPELL_LIST[value]);
    }
}
