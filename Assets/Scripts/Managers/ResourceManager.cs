using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;
    public static GameStateManager GAME;
    public static MagicManager MAGIC;
    public static List<Item_Class> MASTER_ITEM_LIST = new List<Item_Class>();
    public static List<Spell_C> MASTER_SPELL_LIST = new List<Spell_C>();


    public TextAsset item_list, spell_list;
    public static TextAsset ItemList_csv;    
    public static TextAsset SpellList_csv;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        ItemList_csv = item_list;
        SpellList_csv = spell_list;
        InitItems();

        GAME = this.gameObject.GetComponent<GameStateManager>();
        MAGIC = this.gameObject.GetComponent<MagicManager>();
    }

    public static void InitItems()
    {
        MASTER_ITEM_LIST.Clear();
        string[] _allLines = Regex.Split(ItemList_csv.text, "\n");
        foreach (string s in _allLines)
        {
            string[] _splitData = s.Split(',');
            Item_Class _item = new Item_Class();
            _item.Index = int.Parse(_splitData[0]);
            _item.Level = int.Parse(_splitData[1]);
            _item.id_Name = _splitData[2];
            _item.unID_Name = _splitData[3];
            _item.Value = int.Parse(_splitData[4]);
            _item.Type = _splitData[5];
            _item.SubType = _splitData[6];
            _item.Slot = _splitData[7];
            _item.Special = _splitData[8];
            _item.Curse = int.Parse(_splitData[9]);
            _item.Magic = int.Parse(_splitData[10]);
            _item.Min_Damage = int.Parse(_splitData[11]);
            _item.Max_Damage = int.Parse(_splitData[12]);
            _item.AC = int.Parse(_splitData[13]);

            MASTER_ITEM_LIST.Add(_item);
        }

        MASTER_SPELL_LIST.Clear();
        _allLines = Regex.Split(SpellList_csv.text, "\n");
        foreach(string s in _allLines)
        {
            string[] _splitData = s.Split(',');
            Spell_C _spell = new Spell_C();
            _spell.spellIndex = int.Parse(_splitData[0]);
            _spell.spellID = _splitData[1];
            _spell.spellName = _splitData[2];
            _spell.SpellCLass = _splitData[3];
            _spell.battle = (_splitData[4] == "yes" ? true : false);
            _spell.utility = (_splitData[5] == "yes" ? true : false);
            _spell.spellCircle = int.Parse(_splitData[6]);
            _spell.spellSource = _splitData[7];
            _spell.spellAlignment = _splitData[8];
            _spell.spellTarget = _splitData[9];
            _spell.spellMin = int.Parse(_splitData[10]);
            _spell.spellMax = int.Parse(_splitData[11]);
            _spell.spellMod = float.Parse(_splitData[12]);
            _spell.spellEffectType = _splitData[13];
            _spell.spellDescriptor = _splitData[14];
            _spell.spellDescriptor = _spell.spellDescriptor.Replace("|", ",");

            MASTER_SPELL_LIST.Add(_spell);
        }
    }

    public static Party_Class FindParty()
    {
        return FindObjectOfType<Party_Class>();
    }
}
