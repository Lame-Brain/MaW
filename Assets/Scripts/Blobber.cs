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

    public static class RectTransformExtensions
    {
        public static void SetLeft(this RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        }

        public static void SetRight(this RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
        }

        public static void SetTop(this RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
        }

        public static void SetBottom(this RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }
    }
}

