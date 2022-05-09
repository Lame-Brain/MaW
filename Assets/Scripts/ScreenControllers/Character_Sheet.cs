using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Sheet : MonoBehaviour
{
    public TMPro.TextMeshProUGUI characterName, Level_Class_XP, Strength, Fortitude, Dexterity, IQ, Wisdom, Charm, Init_Bonus, Attack, Attack_Bonus, AC;     
    public GameObject Blessed, Blinded, Weakened, Slowed, Frail, ManaBurn, Poisoned, Stunned, Paralyzed, Stone, Dead, Ash;
    public GameObject Selector, Equip_btn, UnEquip_btn, Exam_btn, Use_btn, Discard_btn, spell_btn1, spell_btn2;
    public TMPro.TextMeshProUGUI[] Inventory_Slot;
    public GameObject Inventory_Panel, Stats_Panel, Item_Info_Panel, Spell_List_Panel;

    private int Selected = -1;
    private Character_C selected_character;
    private Party_Class _party;
    private Color _myGreen = new Color(0.208f, 0.369f, 0.231f, 1);
    private Color _myRed = new Color(0.369f, 0.227f, 0.208f, 1);
    private Color _myGrey = Color.grey;

    public void Update_Sheet(Character_C _who = null)
    {
        if(_who != null)
        {
            selected_character = _who;
            _party = FindObjectOfType<Party_Class>();

            if (_who.Character_Class == Blobber.CharacterClassEnum.Mage || _who.Character_Class == Blobber.CharacterClassEnum.Cleric || _who.Character_Class == Blobber.CharacterClassEnum.Healer)
            { 
                spell_btn1.SetActive(true); 
                spell_btn2.SetActive(true); 
            }
            else
            {
                spell_btn1.SetActive(false);
                spell_btn2.SetActive(false);
            }

            characterName.text = _who.Character_Name;
            Level_Class_XP.text = "Level " + _who.XP_Level + " " + _who.Character_Class + " with " + _who.XP + " XP.";

            if (_who.Strength._valueModifier == 0) Strength.color = Color.black;
            if (_who.Strength._valueModifier > 0) Strength.color = Color.green;
            if (_who.Strength._valueModifier < 0) Strength.color = Color.red;
            Strength.text = "[Strength] " + _who.Strength.Value();

            if (_who.Fortitude._valueModifier == 0) Fortitude.color = Color.black;
            if (_who.Fortitude._valueModifier > 0) Fortitude.color = Color.green;
            if (_who.Fortitude._valueModifier < 0) Fortitude.color = Color.red;
            Fortitude.text = "[Fortitude] " + _who.Fortitude.Value();

            if (_who.Dexterity._valueModifier == 0) Dexterity.color = Color.black;
            if (_who.Dexterity._valueModifier > 0) Dexterity.color = Color.green;
            if (_who.Dexterity._valueModifier < 0) Dexterity.color = Color.red;
            Dexterity.text = "[Dexterity] " + _who.Dexterity.Value();

            if (_who.IQ._valueModifier == 0) IQ.color = Color.black;
            if (_who.IQ._valueModifier > 0) IQ.color = Color.green;
            if (_who.IQ._valueModifier < 0) IQ.color = Color.red;
            IQ.text = "[IQ] " + _who.IQ.Value();

            if (_who.Wisdom._valueModifier == 0) Wisdom.color = Color.black;
            if (_who.Wisdom._valueModifier > 0) Wisdom.color = Color.green;
            if (_who.Wisdom._valueModifier < 0) Wisdom.color = Color.red;
            Wisdom.text = "[Wisdom] " + _who.Wisdom.Value();

            if (_who.Charm._valueModifier == 0) Charm.color = Color.black;
            if (_who.Charm._valueModifier > 0) Charm.color = Color.green;
            if (_who.Charm._valueModifier < 0) Charm.color = Color.red;
            Charm.text = "[Charm] " + _who.Charm.Value();

            Init_Bonus.text = "[Init] ";
            if (_who.Init_Bonus > 0) Init_Bonus.text += "+";
            Init_Bonus.text += _who.Init_Bonus.ToString();

            Attack.text = "[Attack Bonus] ";
            if (_who.Attack_Bonus > 0) Attack.text += "+";
            Attack.text += _who.Init_Bonus.ToString();

            Attack_Bonus.text = "[Attack Bonus] " + _who.Min_Damage + " - " + _who.Max_Damage;
            if (_who.Num_Attacks > 1) Attack_Bonus.text += " (x" + _who.Num_Attacks + ")";

            AC.text = "[Armor] " + _who.AC;

            Blessed.SetActive(_who.Blessed); Blinded.SetActive(_who.Blind); Weakened.SetActive(_who.Weak); Slowed.SetActive(_who.Slow); Frail.SetActive(_who.Frail); ManaBurn.SetActive(_who.ManaBurn);
            Poisoned.SetActive(_who.Poison); Stunned.SetActive(_who.Stun); Paralyzed.SetActive(_who.Paralyze); Stone.SetActive(_who.Stone); Dead.SetActive(_who.Dead); Ash.SetActive(_who.Ash);

            foreach (TMPro.TextMeshProUGUI _i in Inventory_Slot) _i.gameObject.SetActive(false);

            if (_who.Weapon_Slot != null)
            {
                Inventory_Slot[0].gameObject.SetActive(true);
                Inventory_Slot[0].color = Color.black;
                if (_who.Weapon_Slot.Magic() > 0) Inventory_Slot[0].color = _myGreen;
                if (_who.Weapon_Slot.Curse() > 0) Inventory_Slot[0].color = _myRed;
                Inventory_Slot[0].text = "[E] " + _who.Weapon_Slot.ItemName();
                Inventory_Slot[0].GetComponent<Character_Sheet_Inventory_slot_Selector>().value = 0;
            }

            if(_who.Shield_Slot != null)
            {
                Inventory_Slot[1].gameObject.SetActive(true);
                Inventory_Slot[1].color = Color.black;
                if (_who.Shield_Slot.Magic() > 0) Inventory_Slot[1].color = _myGreen;
                if (_who.Shield_Slot.Curse() > 0) Inventory_Slot[1].color = _myRed;
                Inventory_Slot[1].text = "[E] " + _who.Shield_Slot.ItemName();
                Inventory_Slot[0].GetComponent<Character_Sheet_Inventory_slot_Selector>().value = 1;
            }

            if (_who.Armor_Slot != null)
            {
                Inventory_Slot[2].gameObject.SetActive(true);
                Inventory_Slot[2].color = Color.black;
                if (_who.Armor_Slot.Magic() > 0) Inventory_Slot[2].color = _myGreen;
                if (_who.Armor_Slot.Curse() > 0) Inventory_Slot[2].color = _myRed;
                Inventory_Slot[2].text = "[E] " + _who.Armor_Slot.ItemName();
                Inventory_Slot[0].GetComponent<Character_Sheet_Inventory_slot_Selector>().value = 2;
            }

            if (_who.Head_Slot != null)
            {
                Inventory_Slot[3].gameObject.SetActive(true);
                Inventory_Slot[3].color = Color.black;
                if (_who.Head_Slot.Magic() > 0) Inventory_Slot[3].color = _myGreen;
                if (_who.Head_Slot.Curse() > 0) Inventory_Slot[3].color = _myRed;
                Inventory_Slot[3].text = "[E] " + _who.Head_Slot.ItemName();
                Inventory_Slot[0].GetComponent<Character_Sheet_Inventory_slot_Selector>().value = 3;
            }

            if (_who.Neck_Slot != null)
            {
                Inventory_Slot[4].gameObject.SetActive(true);
                Inventory_Slot[4].color = Color.black;
                if (_who.Neck_Slot.Magic() > 0) Inventory_Slot[4].color = _myGreen;
                if (_who.Neck_Slot.Curse() > 0) Inventory_Slot[4].color = _myRed;
                Inventory_Slot[4].text = "[E] " + _who.Neck_Slot.ItemName();
                Inventory_Slot[0].GetComponent<Character_Sheet_Inventory_slot_Selector>().value = 4;
            }

            if (_who.RightFinger_Slot != null)
            {
                Inventory_Slot[5].gameObject.SetActive(true);
                Inventory_Slot[5].color = Color.black;
                if (_who.RightFinger_Slot.Magic() > 0) Inventory_Slot[5].color = _myGreen;
                if (_who.RightFinger_Slot.Curse() > 0) Inventory_Slot[5].color = _myRed;
                Inventory_Slot[5].text = "[E] " + _who.RightFinger_Slot.ItemName();
                Inventory_Slot[0].GetComponent<Character_Sheet_Inventory_slot_Selector>().value = 5;
            }

            if (_who.LeftFinger_Slot != null)
            {
                Inventory_Slot[6].gameObject.SetActive(true);
                Inventory_Slot[6].color = Color.black;
                if (_who.LeftFinger_Slot.Magic() > 0) Inventory_Slot[6].color = _myGreen;
                if (_who.LeftFinger_Slot.Curse() > 0) Inventory_Slot[6].color = _myRed;
                Inventory_Slot[6].text = "[E] " + _who.LeftFinger_Slot.ItemName();
                Inventory_Slot[0].GetComponent<Character_Sheet_Inventory_slot_Selector>().value = 6;
            }

            if (_who.Cloak_Slot != null)
            {
                Inventory_Slot[7].gameObject.SetActive(true);
                Inventory_Slot[7].color = Color.black;
                if (_who.Cloak_Slot.Magic() > 0) Inventory_Slot[7].color = _myGreen;
                if (_who.Cloak_Slot.Curse() > 0) Inventory_Slot[7].color = _myRed;
                Inventory_Slot[7].text = "[E] " + _who.Cloak_Slot.ItemName();
                Inventory_Slot[0].GetComponent<Character_Sheet_Inventory_slot_Selector>().value = 7;
            }

            for (int _i = 0; _i < _party.Bag.Length; _i++)
            {                
                if (_party.Bag[_i] != null)
                {
                    Inventory_Slot[_i + 8].gameObject.SetActive(true);
                    Inventory_Slot[_i + 8].color = Color.black;
                    Inventory_Slot[_i + 8].GetComponent<Character_Sheet_Inventory_slot_Selector>().value = _i + 8;
                    if (_party.Bag[_i].Type() == "Equip")
                    {
                        if (_party.Bag[_i].Identified() && _party.Bag[_i].Curse() > 0) Inventory_Slot[_i + 8].color = _myRed;
                        if (_party.Bag[_i].Identified() && _party.Bag[_i].Curse() < 1 && _party.Bag[_i].Magic() > 0) Inventory_Slot[_i + 8].color = _myGreen;
                        if (!_who.CanEquipThis(_party.Bag[_i])) Inventory_Slot[_i + 8].color = _myGrey;
                    }
                    Inventory_Slot[_i + 8].text = _party.Bag[_i].ItemName();
                }
            }

            if(Selected == -1)
            {
                Selector.SetActive(false); Equip_btn.SetActive(false); UnEquip_btn.SetActive(false); Exam_btn.SetActive(true); Use_btn.SetActive(false); Discard_btn.SetActive(true);
            }

            if (Selected > -1)
            {
                Debug.Log("Selected: " + Selected);

                Selector.SetActive(true);
                Selector.transform.SetParent(Inventory_Slot[Selected].transform);
                Blobber.RectTransformExtensions.SetTop(Selector.GetComponent<RectTransform>(), 0);
                Blobber.RectTransformExtensions.SetLeft(Selector.GetComponent<RectTransform>(), 0);
                Blobber.RectTransformExtensions.SetBottom(Selector.GetComponent<RectTransform>(), 0);
                Blobber.RectTransformExtensions.SetRight(Selector.GetComponent<RectTransform>(), 0);

                if(Selected > -1 && Selected < 8)
                {
                    Equip_btn.SetActive(false);
                    UnEquip_btn.SetActive(true);
                    Use_btn.SetActive(false);
                }


                if(Selected > 7)
                {

                    if(_party.Bag[Selected - 8].Type() == "Equip")
                    {
                        Equip_btn.SetActive(true);
                        UnEquip_btn.SetActive(false);
                        Use_btn.SetActive(false);
                    }

                    if (_party.Bag[Selected - 8].Type() == "Consume")
                    {
                        Equip_btn.SetActive(false);
                        UnEquip_btn.SetActive(false);
                        Use_btn.SetActive(true);
                    }

                    if (_party.Bag[Selected - 8].Type() == "Item")
                    {
                        Equip_btn.SetActive(false);
                        UnEquip_btn.SetActive(false);
                        Use_btn.SetActive(false);
                    }
                }
                
            }

        }
    }

    public void Inventory_Item_Clicked(int v)
    {
        Selected = v;
        Update_Sheet(selected_character);
    }

    public void Open_Inventory_Panel()
    {
        Stats_Panel.SetActive(false);
        Inventory_Panel.SetActive(true);
    }

    public void Open_Stats_Panel()
    {
        Stats_Panel.SetActive(true);
        Inventory_Panel.SetActive(false);
    }

    public void Open_Item_Info_Panel()
    {
        Item _item = null;
        if (Selected == 0) _item = selected_character.Weapon_Slot;
        if (Selected == 1) _item = selected_character.Armor_Slot;
        if (Selected == 2) _item = selected_character.Shield_Slot;
        if (Selected == 3) _item = selected_character.Head_Slot;
        if (Selected == 4) _item = selected_character.Neck_Slot;
        if (Selected == 5) _item = selected_character.RightFinger_Slot;
        if (Selected == 6) _item = selected_character.LeftFinger_Slot;
        if (Selected == 7) _item = selected_character.Cloak_Slot;
        if (Selected > 7) _item = _party.Bag[Selected - 8];

        Item_Info_Panel.SetActive(true);
        Item_Info_Panel.GetComponent<Character_Sheet_Item_Info>().UpdatePanel(_item);
    }

    public void Spell_Button_Clicked()
    {
        Spell_List_Panel.SetActive(true);
        Spell_List_Panel.GetComponent<Character_Sheet_Spell_List_Panel>().Update_Spell_List(selected_character);
    }

    public void Right_Button_Clicked()
    {
        int _partyMember = -1;
        //find current party member
        for (int i = 0; i < _party.Party.Length; i++) if (selected_character.ID == _party.Party[i].ID) _partyMember = i;

        if (_partyMember >= 0) //only advance if _partyMember is a valid number
        {
            _partyMember++;
            if (_partyMember >= _party.Party.Length) _partyMember = 0;
            Update_Sheet(_party.Party[_partyMember]);
        }
    }

    public void Left_Button_Clicked()
    {
        int _partyMember = -1;
        //find current party member
        for (int i = 0; i < _party.Party.Length; i++) if (selected_character.ID == _party.Party[i].ID) _partyMember = i;

        if (_partyMember >= 0) //only advance if _partyMember is a valid number
        {
            _partyMember--;
            if (_partyMember < 0) _partyMember = _party.Party.Length -1;
            Update_Sheet(_party.Party[_partyMember]);
        }
    }
}
