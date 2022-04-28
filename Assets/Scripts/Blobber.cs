using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blobber
{
    public enum CharacterClassEnum { none, Warrior, Knight, Assassin, Rogue, Cleric, Healer, Mage }
    public enum ElementEnum { Physical, Fire, Ice, Shock, Arcane, Darkness }

    //name = "Fighter";
    //description = "A toe-to-toe combatant, skilled in all types of weapons and armor. Fighters have lots of health and as their level improves they can attack multiple times which increases their attack damage output. Strength is their most valued attribute.";


    public static class Roster
    {
        public static List<Character_C> ROSTER = new List<Character_C>();
        
        public static void Init()
        {
            ROSTER.Clear();
            ROSTER.Add(new Character_C());
        }
    }

    public static class Resource_Manager
    {
        public static List<Item_Class> MASTER_ITEM_LIST = new List<Item_Class>();
        
        public static void InitItems()
        {
            MASTER_ITEM_LIST.Clear();
            MASTER_ITEM_LIST.Add(new Item_Class());
        }
    }

}

