using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;
    public static List<Item_Class> MASTER_ITEM_LIST = new List<Item_Class>();

    public static TextAsset ItemList_csv;

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
        }
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
    }

}
