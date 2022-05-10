using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party_Class : MonoBehaviour
{
    public List<string> Mem = new List<string>();
    public int Geld, Light;    
    public int Time = 0;
    public int xLoc, yLoc, facing;
    public int detect_loc, detect_dir, floating;

    public int BAGSIZE = 25;
    public Item[] Bag;

    public Character_C[] Party = new Character_C[6];

    private PartyManager _partyObj;    

    private void Awake()
    {
        _partyObj = FindObjectOfType<PartyManager>();

        Bag = new Item[BAGSIZE];
        for (int i = 0; i < BAGSIZE; i++) Bag[i] = null;
    }
    private void Start()
    {
        //Get Party Members
        for (int i = 0; i < 6; i++) Party[i] = null;
        foreach (Character_C _c in Blobber.Roster.ROSTER) if (_c.Party_Slot > -1) Party[_c.Party_Slot] = _c;        
    }

    public void SortBag()
    {
        List<Item> _bag_list = new List<Item>();        
        for (int i = 0; i < Bag.Length; i++)
        {
            if (Bag[i] != null) _bag_list.Add(Bag[i]);
            Bag[i] = null;
        }

        int _num = (_bag_list.Count < BAGSIZE ? _bag_list.Count : BAGSIZE);

        for (int i = 0; i < _num; i++) Bag[i] = null;
    }

    public bool AddToBag(Item _item)
    {
        SortBag();
        for (int i = 0; i < BAGSIZE; i++)
            if(Bag[i] == null)
            {
                Bag[i] = _item;
                return true;
            }
        return false;
    }
}
