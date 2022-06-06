using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public int item_Class_ID;
    public bool identified, equipped;

    public Item(int _itm, bool _id = false, bool _eq = false)
    {
        this.item_Class_ID = _itm;
        this.identified = _id;
        this.equipped = _eq;
    }

    public int Index() { return ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].Index; }
    public int Level() { return ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].Level; }
    public int Value() { return ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].Value; }
    public int Curse() { return ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].Curse; }
    public int Magic() { return ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].Magic; }
    public int Min_Damage() { return ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].Min_Damage; }
    public int Max_Damage() { return ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].Max_Damage; }
    public int AC() { return ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].AC; }
    public string ID_Name() { return ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].id_Name; }
    public string UNID_Name() { return ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].unID_Name; }
    public string Type() { return ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].Type; }
    public string SubType() { return ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].SubType; }
    public string Slot() { return ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].Slot; }
    public string Special() { return ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].Special; }
    public bool Identified() { return this.identified; }
    public bool Equipped() { return this.equipped; }
    public bool Utility() { return (ResourceManager.MASTER_ITEM_LIST[this.item_Class_ID].Utility == 0 ? false : true); }
    public string ItemName()
    {
        if (Identified()) return ID_Name();
        return UNID_Name();
    }
}
