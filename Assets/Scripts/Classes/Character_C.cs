using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blobber;

public class Character_C
{
    public int ID;
    public string Character_Name;
    public CharacterClassEnum Character_Class;
    public int Party_Slot;
    public Attribute_C Strength;
    public Attribute_C Dexterity;
    public Attribute_C Fortitude;
    public Attribute_C IQ;
    public Attribute_C Wisdom;
    public Attribute_C Charm;
    public int XP_Level, XP, XP_NNL;
    public int HP, HP_Max;
    public int Init_Bonus, Num_Attacks, Attack_Bonus, Damage_Bonus, Min_Damage, Max_Damage;
    public int Dodge, Crit, AC, Regen;
    public int Fire_Resist, Ice_Resist, Shock_Resist, Magic_Resist;
    public List<Effect_C> effect_list = new List<Effect_C>();
    public Item Head_Slot = null;
    public Item Neck_Slot = null;
    public Item Cloak_Slot = null;
    public Item RightFinger_Slot = null;
    public Item LeftFinger_Slot = null;
    public Item Armor_Slot = null;
    public Item Shield_Slot = null;
    public Item Weapon_Slot = null;
    public int[] Spell_Slot = new int[5];
    public int[] Bonus_Slot = new int[5];
    public int[] Spells_Cast = new int[5];
    public bool[] SpellBook = new bool[100];
    public bool Blind,
                Weak,
                Slow,
                Frail,
                ManaBurn,
                Poison,
                Stun,
                Paralyze,
                Stone,
                Dead,
                Ash,
                Blessed,
                Medium_Equipped,
                Heavy_Equipped,
                Caster,
                Skirmisher;

    public Character_C(string _name = "New Character", CharacterClassEnum _class = CharacterClassEnum.none, int _str = 9, int _dex = 9, int _fort = 9, int _iq = 9, int _wis = 9, int _chrm = 9, int _level = 1)
    {
        this.ID = 0; for (int i = 0; i < Roster.ROSTER.Count; i++) if (Roster.ROSTER[i].ID == this.ID) this.ID = Roster.ROSTER[i].ID + 1;
        this.Character_Name = _name;
        this.Character_Class = _class;
        this.Party_Slot = -1;
        this.Strength = new Attribute_C(_str);
        this.Dexterity = new Attribute_C(_dex);
        this.Fortitude = new Attribute_C(_fort);
        this.IQ = new Attribute_C(_iq);
        this.Wisdom = new Attribute_C(_wis);
        this.Charm = new Attribute_C(_chrm);
        this.XP_Level = _level; this.XP = 0;
        this.Head_Slot = null;
        this.Neck_Slot = null;
        this.Cloak_Slot = null;
        this.RightFinger_Slot = null;
        this.LeftFinger_Slot = null;
        this.Armor_Slot = null;
        this.Shield_Slot = null;
        this.Weapon_Slot = null;
        DefaultSpells();
        this.effect_list.Clear();        
        UpdateDerivedStats();
        this.HP = this.HP_Max;
    }

    public void UpdateDerivedStats()
    {
        //XP NNL
        this.XP_NNL = 250;
        if(this.XP_Level > 1) this.XP_NNL = (int)(this.XP_NNL * 1.5f) + 42 + (XP_Level * 75);

        //Health
        if (this.Character_Class == CharacterClassEnum.none) this.HP_Max = 5 * XP_Level;
        if (this.Character_Class == CharacterClassEnum.Warrior) this.HP_Max = (10 + this.Fortitude.Mod()) * this.XP_Level;
        if (this.Character_Class == CharacterClassEnum.Knight) this.HP_Max = (12 + this.Fortitude.Mod()) * this.XP_Level;
        if (this.Character_Class == CharacterClassEnum.Assassin) this.HP_Max = (8 + this.Fortitude.Mod()) * this.XP_Level;
        if (this.Character_Class == CharacterClassEnum.Rogue) this.HP_Max = (6 + this.Fortitude.Mod()) * this.XP_Level;
        if (this.Character_Class == CharacterClassEnum.Cleric) this.HP_Max = (8 + this.Fortitude.Mod()) * this.XP_Level;
        if (this.Character_Class == CharacterClassEnum.Healer) this.HP_Max = (4 + this.Fortitude.Mod()) * this.XP_Level;
        if (this.Character_Class == CharacterClassEnum.Mage) this.HP_Max = (4 + this.Fortitude.Mod()) *this.XP_Level;
        if (this.HP > this.HP_Max) this.HP = this.HP_Max;

        //Calculate Armor Flags
        this.Medium_Equipped = false; this.Heavy_Equipped = false;
        if (this.Head_Slot != null && this.Head_Slot.SubType() == "Medium") this.Medium_Equipped = true;
        if (this.Armor_Slot != null && this.Armor_Slot.SubType() == "Medium") this.Medium_Equipped = true;
        if (this.Shield_Slot != null && this.Shield_Slot.SubType() == "Medium") this.Medium_Equipped = true;
        if (this.Head_Slot != null && this.Head_Slot.SubType() == "Heavy") this.Heavy_Equipped = true;
        if (this.Armor_Slot != null && this.Armor_Slot.SubType() == "Heavy") this.Heavy_Equipped = true;
        if (this.Shield_Slot != null && this.Shield_Slot.SubType() == "Heavy") this.Heavy_Equipped = true;
        //Calculate Skirmisher flag
        this.Skirmisher = false;
        if (this.Character_Class == CharacterClassEnum.Rogue || this.Character_Class == CharacterClassEnum.Assassin) this.Skirmisher = true;
        //Calculate Caster flag
        this.Caster = false;
        if (this.Character_Class == CharacterClassEnum.Mage || this.Character_Class == CharacterClassEnum.Healer || this.Character_Class == CharacterClassEnum.Cleric) this.Caster = true;

        //NumAttacks
        this.Num_Attacks = 1;
        if(this.Character_Class == CharacterClassEnum.Knight)
        {
            if (this.XP_Level > 6) this.Num_Attacks = 2;
            if (this.XP_Level > 12) this.Num_Attacks = 3;
            if (this.XP_Level > 18) this.Num_Attacks = 4;
            if (this.XP_Level > 20) this.Num_Attacks = this.XP_NNL - 16;
        }
        if(this.Character_Class == CharacterClassEnum.Warrior)
        {
            if (this.XP_Level > 5) this.Num_Attacks = 2;
            if (this.XP_Level > 10) this.Num_Attacks = 3;
            if (this.XP_Level > 15) this.Num_Attacks = 4;
            if (this.XP_Level > 20) this.Num_Attacks = this.XP_NNL - 15;
        }
        if(this.Character_Class == CharacterClassEnum.Assassin)
        {
            if (this.XP_Level > 10) this.Num_Attacks = 2;
            if (this.XP_Level > 15) this.Num_Attacks = 3;
            if (this.XP_Level > 20) this.Num_Attacks = this.XP_NNL - 17;
        }
        if(this.Character_Class == CharacterClassEnum.Rogue)
        {
            if (this.XP_Level > 12) this.Num_Attacks = 2;
            if (this.XP_Level > 18) this.Num_Attacks = 3;
        }
        if(this.Character_Class == CharacterClassEnum.Cleric)
        {
            if (this.XP_Level > 15) this.Num_Attacks = 2;
        }

        //Battle Stats
        this.Init_Bonus = this.Dexterity.Mod();
        this.Attack_Bonus = this.Dexterity.Mod();
        this.Damage_Bonus = this.Strength.Mod();
        this.Min_Damage = 1;
        this.Max_Damage = 1;
        if(this.Weapon_Slot != null)
        {
            this.Min_Damage = this.Weapon_Slot.Min_Damage();
            this.Max_Damage = this.Weapon_Slot.Max_Damage();
        }
        this.Dodge = 0;
        if (this.Dexterity.Mod() > 0) this.Dodge = Dexterity.Mod();
        if (!this.Medium_Equipped && !this.Heavy_Equipped) this.Dodge += 2;
        if (!this.Medium_Equipped && this.Heavy_Equipped) this.Dodge++;
        if (this.Medium_Equipped && !this.Heavy_Equipped) this.Dodge++;
        if (this.Skirmisher) this.Dodge++;        
        this.Crit = 0;
        this.AC = 10;
        if (!this.Medium_Equipped && !this.Heavy_Equipped) this.AC += Dexterity.Mod();
        if (this.Medium_Equipped && !this.Heavy_Equipped) this.AC += (int)Dexterity.Mod() / 2;
        if (this.Head_Slot != null) this.AC += this.Head_Slot.AC();
        if (this.Neck_Slot != null) this.AC += this.Neck_Slot.AC();
        if (this.Cloak_Slot != null) this.AC += this.Cloak_Slot.AC();
        if (this.RightFinger_Slot != null) this.AC += this.RightFinger_Slot.AC();
        if (this.LeftFinger_Slot != null) this.AC += this.LeftFinger_Slot.AC();
        if (this.Armor_Slot != null) this.AC += this.Armor_Slot.AC();
        if (this.Shield_Slot != null) this.AC += this.Shield_Slot.AC();
        if (this.Weapon_Slot != null) this.AC += this.Weapon_Slot.AC();

        //Regen and Resist
        this.Regen = 0;
        this.Fire_Resist = 0;
        this.Ice_Resist = 0;
        this.Shock_Resist = 0;
        this.Magic_Resist = 0;

        //Flag reset
        this.Blind = false;
        this.Weak = false;
        this.Slow = false;
        this.Frail = false;
        this.ManaBurn = false;
        this.Poison = false;
        this.Stun = false;
        this.Paralyze = false;
        this.Blessed = false;

        //Apply active effects
        foreach (Effect_C _fx in effect_list)
        {
            int _acc = 0, _dam = 0, _init = 0, _ac = 0;
            if(_fx.effect_name == "Blind" && _fx.effect_time != 0)
            {
                this.Blind = true;
                if (_acc < _fx.value) _acc = _fx.value;
            }
            if(_fx.effect_name == "Weak" && _fx.effect_time != 0)
            {
                this.Weak = true;
                if (_dam < _fx.value) _dam = _fx.value;
            }
            if(_fx.effect_name == "Slow" && _fx.effect_time != 0)
            {
                this.Slow = true;
                if (_init < _fx.value) _init = _fx.value;
                if (this.Num_Attacks > 1) this.Num_Attacks = 1;
            }
            if(_fx.effect_name == "Frail" && _fx.effect_time != 0)
            {
                this.Frail = true;
                if (_ac < _fx.value) _ac = _fx.value;
            }
            this.Attack_Bonus -= _acc; this.Damage_Bonus -= _dam; this.Init_Bonus -= _init; this.AC -= _ac;
            if (_fx.effect_name == "ManaBurn" && _fx.effect_time != 0) this.ManaBurn = true;
            if (_fx.effect_name == "Poison" && _fx.effect_time != 0) this.Poison = true;
            if (_fx.effect_name == "Stun" && _fx.effect_time != 0)
            {
                this.Stun = true;
                this.Dodge = 0;
            }
            if (_fx.effect_name == "Paralyze" && _fx.effect_time != 0) this.Paralyze = true;
            if (_fx.effect_name == "Bless" && _fx.effect_time != 0) this.Blessed = true;
            if (_fx.effect_name == "Reduce_Fire_Resist" && _fx.effect_time != 0) this.Fire_Resist--;
            if (_fx.effect_name == "Reduce_Ice_Resist" && _fx.effect_time != 0) this.Ice_Resist--;
            if (_fx.effect_name == "Reduce_Shock_Resist" && _fx.effect_time != 0) this.Shock_Resist--;
            if (_fx.effect_name == "Reduce_Magic_Resist" && _fx.effect_time != 0) this.Magic_Resist--;
            if (_fx.effect_name == "Increase_Fire_Resist" && _fx.effect_time != 0) this.Fire_Resist++;
            if (_fx.effect_name == "Increase_Ice_Resist" && _fx.effect_time != 0) this.Ice_Resist++;
            if (_fx.effect_name == "Increase_Shock_Resist" && _fx.effect_time != 0) this.Shock_Resist++;
            if (_fx.effect_name == "Increase_Magic_Resist" && _fx.effect_time != 0) this.Magic_Resist++;
            if (this.Fire_Resist < -5) this.Fire_Resist = -5; if (this.Fire_Resist > 5) this.Fire_Resist = 5;
            if (this.Ice_Resist < -5) this.Ice_Resist = -5; if (this.Ice_Resist > 5) this.Ice_Resist = 5;
            if (this.Shock_Resist < -5) this.Shock_Resist = -5; if (this.Shock_Resist > 5) this.Shock_Resist = 5;
            if (this.Magic_Resist < -5) this.Magic_Resist = -5; if (this.Magic_Resist > 5) this.Magic_Resist = 5;
        }
    }

    public void DefaultSpells()
    {
        for (int i = 0; i < 5; i++)
        {
            this.Spell_Slot[i] = 0;
            this.Bonus_Slot[i] = 0;
        }

        if (this.Character_Class == CharacterClassEnum.Knight || this.Character_Class == CharacterClassEnum.Warrior || this.Character_Class == CharacterClassEnum.Assassin || this.Character_Class == CharacterClassEnum.Rogue)
        { 
            for (int i = 0; i < 5; i++) this.Spells_Cast[i] = 0;
            for (int i = 0; i < 100; i++)
                this.SpellBook[i] = false;
        }

        //Spell Slots
        if (this.Character_Class == CharacterClassEnum.Mage)
        {
            if (this.XP_Level == 01) this.Spell_Slot[0] = 01 + this.IQ.Mod(); this.Spell_Slot[1] = 0; this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 02) this.Spell_Slot[0] = 02 + this.IQ.Mod(); this.Spell_Slot[1] = 0; this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 03) this.Spell_Slot[0] = 03 + this.IQ.Mod(); this.Spell_Slot[1] = 0; this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 04) this.Spell_Slot[0] = 04 + this.IQ.Mod(); this.Spell_Slot[1] = 0; this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 05) this.Spell_Slot[0] = 05 + this.IQ.Mod(); this.Spell_Slot[1] = 01 + this.IQ.Mod(); this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 06) this.Spell_Slot[0] = 06 + this.IQ.Mod(); this.Spell_Slot[1] = 02 + this.IQ.Mod(); this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 07) this.Spell_Slot[0] = 07 + this.IQ.Mod(); this.Spell_Slot[1] = 03 + this.IQ.Mod(); this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 08) this.Spell_Slot[0] = 08 + this.IQ.Mod(); this.Spell_Slot[1] = 04 + this.IQ.Mod(); this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 09) this.Spell_Slot[0] = 09 + this.IQ.Mod(); this.Spell_Slot[1] = 05 + this.IQ.Mod(); this.Spell_Slot[2] = 01 + this.IQ.Mod(); this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 10) this.Spell_Slot[0] = 10 + this.IQ.Mod(); this.Spell_Slot[1] = 06 + this.IQ.Mod(); this.Spell_Slot[2] = 02 + this.IQ.Mod(); this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 11) this.Spell_Slot[0] = 10 + this.IQ.Mod(); this.Spell_Slot[1] = 07 + this.IQ.Mod(); this.Spell_Slot[2] = 03 + this.IQ.Mod(); this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 12) this.Spell_Slot[0] = 10 + this.IQ.Mod(); this.Spell_Slot[1] = 08 + this.IQ.Mod(); this.Spell_Slot[2] = 04 + this.IQ.Mod(); this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 13) this.Spell_Slot[0] = 10 + this.IQ.Mod(); this.Spell_Slot[1] = 09 + this.IQ.Mod(); this.Spell_Slot[2] = 05 + this.IQ.Mod(); this.Spell_Slot[3] = 01 + this.IQ.Mod(); this.Spell_Slot[4] = 0;
            if (this.XP_Level == 14) this.Spell_Slot[0] = 10 + this.IQ.Mod(); this.Spell_Slot[1] = 10 + this.IQ.Mod(); this.Spell_Slot[2] = 06 + this.IQ.Mod(); this.Spell_Slot[3] = 02 + this.IQ.Mod(); this.Spell_Slot[4] = 0;
            if (this.XP_Level == 15) this.Spell_Slot[0] = 10 + this.IQ.Mod(); this.Spell_Slot[1] = 10 + this.IQ.Mod(); this.Spell_Slot[2] = 07 + this.IQ.Mod(); this.Spell_Slot[3] = 03 + this.IQ.Mod(); this.Spell_Slot[4] = 0;
            if (this.XP_Level == 16) this.Spell_Slot[0] = 10 + this.IQ.Mod(); this.Spell_Slot[1] = 10 + this.IQ.Mod(); this.Spell_Slot[2] = 08 + this.IQ.Mod(); this.Spell_Slot[3] = 04 + this.IQ.Mod(); this.Spell_Slot[4] = 0;
            if (this.XP_Level == 17) this.Spell_Slot[0] = 10 + this.IQ.Mod(); this.Spell_Slot[1] = 10 + this.IQ.Mod(); this.Spell_Slot[2] = 09 + this.IQ.Mod(); this.Spell_Slot[3] = 05 + this.IQ.Mod(); this.Spell_Slot[4] = 0;
            if (this.XP_Level == 18) this.Spell_Slot[0] = 10 + this.IQ.Mod(); this.Spell_Slot[1] = 10 + this.IQ.Mod(); this.Spell_Slot[2] = 10 + this.IQ.Mod(); this.Spell_Slot[3] = 06 + this.IQ.Mod(); this.Spell_Slot[4] = 01;
            if (this.XP_Level == 19) this.Spell_Slot[0] = 10 + this.IQ.Mod(); this.Spell_Slot[1] = 10 + this.IQ.Mod(); this.Spell_Slot[2] = 10 + this.IQ.Mod(); this.Spell_Slot[3] = 07 + this.IQ.Mod(); this.Spell_Slot[4] = 02;
            if (this.XP_Level == 20) this.Spell_Slot[0] = 10 + this.IQ.Mod(); this.Spell_Slot[1] = 10 + this.IQ.Mod(); this.Spell_Slot[2] = 10 + this.IQ.Mod(); this.Spell_Slot[3] = 08 + this.IQ.Mod(); this.Spell_Slot[4] = 03;
        }
        if (this.Character_Class == CharacterClassEnum.Healer)
        {
            if (this.XP_Level == 01) this.Spell_Slot[0] = 01 + this.Wisdom.Mod(); this.Spell_Slot[1] = 0; this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 02) this.Spell_Slot[0] = 02 + this.Wisdom.Mod(); this.Spell_Slot[1] = 0; this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 03) this.Spell_Slot[0] = 03 + this.Wisdom.Mod(); this.Spell_Slot[1] = 0; this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 04) this.Spell_Slot[0] = 04 + this.Wisdom.Mod(); this.Spell_Slot[1] = 0; this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 05) this.Spell_Slot[0] = 05 + this.Wisdom.Mod(); this.Spell_Slot[1] = 01 + this.Wisdom.Mod(); this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 06) this.Spell_Slot[0] = 06 + this.Wisdom.Mod(); this.Spell_Slot[1] = 02 + this.Wisdom.Mod(); this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 07) this.Spell_Slot[0] = 07 + this.Wisdom.Mod(); this.Spell_Slot[1] = 03 + this.Wisdom.Mod(); this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 08) this.Spell_Slot[0] = 08 + this.Wisdom.Mod(); this.Spell_Slot[1] = 04 + this.Wisdom.Mod(); this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 09) this.Spell_Slot[0] = 09 + this.Wisdom.Mod(); this.Spell_Slot[1] = 05 + this.Wisdom.Mod(); this.Spell_Slot[2] = 01 + this.Wisdom.Mod(); this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 10) this.Spell_Slot[0] = 10 + this.Wisdom.Mod(); this.Spell_Slot[1] = 06 + this.Wisdom.Mod(); this.Spell_Slot[2] = 02 + this.Wisdom.Mod(); this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 11) this.Spell_Slot[0] = 10 + this.Wisdom.Mod(); this.Spell_Slot[1] = 07 + this.Wisdom.Mod(); this.Spell_Slot[2] = 03 + this.Wisdom.Mod(); this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 12) this.Spell_Slot[0] = 10 + this.Wisdom.Mod(); this.Spell_Slot[1] = 08 + this.Wisdom.Mod(); this.Spell_Slot[2] = 04 + this.Wisdom.Mod(); this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 13) this.Spell_Slot[0] = 10 + this.Wisdom.Mod(); this.Spell_Slot[1] = 09 + this.Wisdom.Mod(); this.Spell_Slot[2] = 05 + this.Wisdom.Mod(); this.Spell_Slot[3] = 01 + this.Wisdom.Mod(); this.Spell_Slot[4] = 0;
            if (this.XP_Level == 14) this.Spell_Slot[0] = 10 + this.Wisdom.Mod(); this.Spell_Slot[1] = 10 + this.Wisdom.Mod(); this.Spell_Slot[2] = 06 + this.Wisdom.Mod(); this.Spell_Slot[3] = 02 + this.Wisdom.Mod(); this.Spell_Slot[4] = 0;
            if (this.XP_Level == 15) this.Spell_Slot[0] = 10 + this.Wisdom.Mod(); this.Spell_Slot[1] = 10 + this.Wisdom.Mod(); this.Spell_Slot[2] = 07 + this.Wisdom.Mod(); this.Spell_Slot[3] = 03 + this.Wisdom.Mod(); this.Spell_Slot[4] = 0;
            if (this.XP_Level == 16) this.Spell_Slot[0] = 10 + this.Wisdom.Mod(); this.Spell_Slot[1] = 10 + this.Wisdom.Mod(); this.Spell_Slot[2] = 08 + this.Wisdom.Mod(); this.Spell_Slot[3] = 04 + this.Wisdom.Mod(); this.Spell_Slot[4] = 0;
            if (this.XP_Level == 17) this.Spell_Slot[0] = 10 + this.Wisdom.Mod(); this.Spell_Slot[1] = 10 + this.Wisdom.Mod(); this.Spell_Slot[2] = 09 + this.Wisdom.Mod(); this.Spell_Slot[3] = 05 + this.Wisdom.Mod(); this.Spell_Slot[4] = 0;
            if (this.XP_Level == 18) this.Spell_Slot[0] = 10 + this.Wisdom.Mod(); this.Spell_Slot[1] = 10 + this.Wisdom.Mod(); this.Spell_Slot[2] = 10 + this.Wisdom.Mod(); this.Spell_Slot[3] = 06 + this.Wisdom.Mod(); this.Spell_Slot[4] = 01;
            if (this.XP_Level == 19) this.Spell_Slot[0] = 10 + this.Wisdom.Mod(); this.Spell_Slot[1] = 10 + this.Wisdom.Mod(); this.Spell_Slot[2] = 10 + this.Wisdom.Mod(); this.Spell_Slot[3] = 07 + this.Wisdom.Mod(); this.Spell_Slot[4] = 02;
            if (this.XP_Level == 20) this.Spell_Slot[0] = 10 + this.Wisdom.Mod(); this.Spell_Slot[1] = 10 + this.Wisdom.Mod(); this.Spell_Slot[2] = 10 + this.Wisdom.Mod(); this.Spell_Slot[3] = 08 + this.Wisdom.Mod(); this.Spell_Slot[4] = 03;
        }
        if (this.Character_Class == CharacterClassEnum.Cleric)
        {
            if (this.XP_Level == 01) this.Spell_Slot[0] = 03 + this.Wisdom.Mod(); this.Spell_Slot[1] = 0; this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 02) this.Spell_Slot[0] = 03 + this.Wisdom.Mod(); this.Spell_Slot[1] = 0; this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 03) this.Spell_Slot[0] = 04 + this.Wisdom.Mod(); this.Spell_Slot[1] = 0; this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 04) this.Spell_Slot[0] = 04 + this.Wisdom.Mod(); this.Spell_Slot[1] = 0; this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 05) this.Spell_Slot[0] = 04 + this.Wisdom.Mod(); this.Spell_Slot[1] = 01 + this.Wisdom.Mod(); this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 06) this.Spell_Slot[0] = 05 + this.Wisdom.Mod(); this.Spell_Slot[1] = 01 + this.Wisdom.Mod(); this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 07) this.Spell_Slot[0] = 05 + this.Wisdom.Mod(); this.Spell_Slot[1] = 02 + this.Wisdom.Mod(); this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 08) this.Spell_Slot[0] = 05 + this.Wisdom.Mod(); this.Spell_Slot[1] = 02 + this.Wisdom.Mod(); this.Spell_Slot[2] = 0; this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 09) this.Spell_Slot[0] = 06 + this.Wisdom.Mod(); this.Spell_Slot[1] = 03 + this.Wisdom.Mod(); this.Spell_Slot[2] = 01 + this.Wisdom.Mod(); this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 10) this.Spell_Slot[0] = 06 + this.Wisdom.Mod(); this.Spell_Slot[1] = 03 + this.Wisdom.Mod(); this.Spell_Slot[2] = 01 + this.Wisdom.Mod(); this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 11) this.Spell_Slot[0] = 06 + this.Wisdom.Mod(); this.Spell_Slot[1] = 04 + this.Wisdom.Mod(); this.Spell_Slot[2] = 02 + this.Wisdom.Mod(); this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 12) this.Spell_Slot[0] = 07 + this.Wisdom.Mod(); this.Spell_Slot[1] = 04 + this.Wisdom.Mod(); this.Spell_Slot[2] = 02 + this.Wisdom.Mod(); this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 13) this.Spell_Slot[0] = 07 + this.Wisdom.Mod(); this.Spell_Slot[1] = 05 + this.Wisdom.Mod(); this.Spell_Slot[2] = 03 + this.Wisdom.Mod(); this.Spell_Slot[3] = 0; this.Spell_Slot[4] = 0;
            if (this.XP_Level == 14) this.Spell_Slot[0] = 07 + this.Wisdom.Mod(); this.Spell_Slot[1] = 05 + this.Wisdom.Mod(); this.Spell_Slot[2] = 03 + this.Wisdom.Mod(); this.Spell_Slot[3] = 01 + this.Wisdom.Mod(); this.Spell_Slot[4] = 0;
            if (this.XP_Level == 15) this.Spell_Slot[0] = 08 + this.Wisdom.Mod(); this.Spell_Slot[1] = 06 + this.Wisdom.Mod(); this.Spell_Slot[2] = 04 + this.Wisdom.Mod(); this.Spell_Slot[3] = 01 + this.Wisdom.Mod(); this.Spell_Slot[4] = 0;
            if (this.XP_Level == 16) this.Spell_Slot[0] = 08 + this.Wisdom.Mod(); this.Spell_Slot[1] = 06 + this.Wisdom.Mod(); this.Spell_Slot[2] = 04 + this.Wisdom.Mod(); this.Spell_Slot[3] = 02 + this.Wisdom.Mod(); this.Spell_Slot[4] = 0;
            if (this.XP_Level == 17) this.Spell_Slot[0] = 08 + this.Wisdom.Mod(); this.Spell_Slot[1] = 07 + this.Wisdom.Mod(); this.Spell_Slot[2] = 05 + this.Wisdom.Mod(); this.Spell_Slot[3] = 02 + this.Wisdom.Mod(); this.Spell_Slot[4] = 0;
            if (this.XP_Level == 18) this.Spell_Slot[0] = 09 + this.Wisdom.Mod(); this.Spell_Slot[1] = 07 + this.Wisdom.Mod(); this.Spell_Slot[2] = 05 + this.Wisdom.Mod(); this.Spell_Slot[3] = 03 + this.Wisdom.Mod(); this.Spell_Slot[4] = 01;
            if (this.XP_Level == 19) this.Spell_Slot[0] = 09 + this.Wisdom.Mod(); this.Spell_Slot[1] = 08 + this.Wisdom.Mod(); this.Spell_Slot[2] = 06 + this.Wisdom.Mod(); this.Spell_Slot[3] = 03 + this.Wisdom.Mod(); this.Spell_Slot[4] = 02;
            if (this.XP_Level == 20) this.Spell_Slot[0] = 09 + this.Wisdom.Mod(); this.Spell_Slot[1] = 08 + this.Wisdom.Mod(); this.Spell_Slot[2] = 06 + this.Wisdom.Mod(); this.Spell_Slot[3] = 04 + this.Wisdom.Mod(); this.Spell_Slot[4] = 03;
        }

        //Default Spells
        if (this.Character_Class == CharacterClassEnum.Mage)
        {
            this.SpellBook[0] = true; this.SpellBook[3] = true; this.SpellBook[7] = true; this.SpellBook[10] = true; this.SpellBook[11] = true;
            if(this.XP_Level > 4) { this.SpellBook[24] = true; this.SpellBook[28] = true; }
            if(this.XP_Level > 8) { this.SpellBook[44] = true; this.SpellBook[47] = true; this.SpellBook[54] = true; }
            if(this.XP_Level > 12) { this.SpellBook[66] = true; this.SpellBook[69] = true; }
            if(this.XP_Level > 17) { this.SpellBook[82] = true; }
        }
        if (this.Character_Class == CharacterClassEnum.Healer)
        {
            this.SpellBook[0] = true; this.SpellBook[12] = true; this.SpellBook[13] = true; this.SpellBook[23] = true;
            if(this.XP_Level > 4) { this.SpellBook[34] = true; this.SpellBook[38] = true; }
            if(this.XP_Level > 8) { this.SpellBook[55] = true; this.SpellBook[56] = true; this.SpellBook[58] = true; }
            if(this.XP_Level > 12) { this.SpellBook[74] = true; this.SpellBook[75] = true; }
            if(this.XP_Level > 17) { this.SpellBook[88] = true; }
        }
        if (this.Character_Class == CharacterClassEnum.Cleric)
        {
            this.SpellBook[0] = true; this.SpellBook[12] = true; this.SpellBook[20] = true; this.SpellBook[22] = true;
            if(this.XP_Level > 4) { this.SpellBook[36] = true; this.SpellBook[37] = true; }
            if(this.XP_Level > 8) { this.SpellBook[55] = true; this.SpellBook[57] = true; this.SpellBook[65] = true; }
            if(this.XP_Level > 13) { this.SpellBook[76] = true; this.SpellBook[77] = true; }
            if(this.XP_Level > 17) { this.SpellBook[91] = true; }
        }
    }

    //TODO: Level up Characters

    public bool CanEquipThis(Item _item)
    {
        if (this.Character_Class == CharacterClassEnum.Knight || this.Character_Class == CharacterClassEnum.Warrior) 
            if (_item.SubType() == "Arcane") return false;
        if (this.Character_Class == CharacterClassEnum.Assassin)
            if (_item.Slot() == "Shield" || _item.SubType() == "Arcane" || _item.SubType() == "Exotic" || _item.SubType() == "Heavy")
                return false;
        if (this.Character_Class == CharacterClassEnum.Rogue)
            if (_item.Slot() == "Shield" || _item.SubType() == "Martial" || _item.SubType() == "Cleric" || _item.SubType() == "Arcane" || _item.SubType() == "Exotic" || _item.SubType() == "Heavy")
                return false;
        if (this.Character_Class == CharacterClassEnum.Cleric)
            if (_item.SubType() == "Martial" || _item.SubType() == "Simple" || _item.SubType() == "Arcane" || _item.SubType() == "Exotic")
                return false;
        if (this.Character_Class == CharacterClassEnum.Healer)
            if (_item.Slot() == "Shield" || _item.SubType() == "Simple" || _item.SubType() == "Martial" ||  _item.SubType() == "Arcane" || _item.SubType() == "Exotic" 
                || _item.SubType() == "Light" || _item.SubType() == "Medium"  || _item.SubType() == "Heavy")
                return false;
        if (this.Character_Class == CharacterClassEnum.Mage)
            if (_item.Slot() == "Shield" || _item.SubType() == "Simple" || _item.SubType() == "Martial" || _item.SubType() == "Cleric" || _item.SubType() == "Exotic" 
                || _item.SubType() == "Light" || _item.SubType() == "Medium" || _item.SubType() == "Heavy")
                return false;

        return true;
    }

    public string SaveCharacter()
    {
        string Character_Output = this.ID + ", ";
        Character_Output += this.Character_Name + ", ";
        Character_Output += this.Character_Class + ", ";
        Character_Output += this.Party_Slot + ", ";
        Character_Output += this.Strength.BaseValue + ", " + this.Strength._valueModifier + ", ";
        Character_Output += this.Dexterity.BaseValue + ", " + this.Dexterity._valueModifier + ", ";
        Character_Output += this.Fortitude.BaseValue + ", " + this.Fortitude._valueModifier + ", ";
        Character_Output += this.IQ.BaseValue + ", " + this.IQ._valueModifier + ", ";
        Character_Output += this.Wisdom.BaseValue + ", " + this.Wisdom._valueModifier + ", ";
        Character_Output += this.Charm.BaseValue + ", " + this.Charm._valueModifier + ", ";
        Character_Output += this.XP_Level + ", " + this.XP + ", " + this.XP_NNL + ", ";
        Character_Output += this.HP + ", " + this.HP_Max + ", ";
        Character_Output += this.Init_Bonus + ", ";
        Character_Output += this.Num_Attacks + ", ";
        Character_Output += this.Attack_Bonus + ", ";
        Character_Output += this.Damage_Bonus + ", ";
        Character_Output += this.Min_Damage + ", " + this.Max_Damage + ", ";
        Character_Output += this.Dodge + ", " + this.Crit + ", " + this.AC + ", " + this.Regen + ", ";
        Character_Output += this.Fire_Resist + ", " + this.Ice_Resist + ", " + this.Shock_Resist + ", " + this.Magic_Resist + ", ";

        if (this.Head_Slot != null) Character_Output += this.Head_Slot.item_Class_ID + ", " + (Head_Slot.identified ? 1: 0) + ", " + (Head_Slot.equipped ? 1 : 0) + ", ";
        if (this.Head_Slot == null) Character_Output += "Null, 0, 0, ";
        if (this.Neck_Slot != null) Character_Output += this.Neck_Slot.item_Class_ID + ", " + (Neck_Slot.identified ? 1 : 0) + ", " + (Neck_Slot.equipped ? 1 : 0) + ", ";
        if (this.Neck_Slot == null) Character_Output += "Null, 0, 0, ";
        if (this.Cloak_Slot != null) Character_Output += this.Cloak_Slot.item_Class_ID + ", " + (Cloak_Slot.identified ? 1 : 0) + ", " + (Cloak_Slot.equipped ? 1 : 0) + ", ";
        if (this.Cloak_Slot == null) Character_Output += "Null, 0, 0, ";
        if (this.RightFinger_Slot != null) Character_Output += this.RightFinger_Slot.item_Class_ID + ", " + (RightFinger_Slot.identified ? 1 : 0) + ", " + (RightFinger_Slot.equipped ? 1 : 0) + ", ";
        if (this.RightFinger_Slot == null) Character_Output += "Null, 0, 0, ";
        if (this.LeftFinger_Slot != null) Character_Output += this.LeftFinger_Slot.item_Class_ID + ", " + (LeftFinger_Slot.identified ? 1 : 0) + ", " + (LeftFinger_Slot.equipped ? 1 : 0) + ", ";
        if (this.LeftFinger_Slot == null) Character_Output += "Null, 0, 0, ";
        if (this.Armor_Slot != null) Character_Output += this.Armor_Slot.item_Class_ID + ", " + (Armor_Slot.identified ? 1 : 0) + ", " + (Armor_Slot.equipped ? 1 : 0) + ", ";
        if (this.Armor_Slot == null) Character_Output += "Null, 0, 0, ";
        if (this.Shield_Slot != null) Character_Output += this.Shield_Slot.item_Class_ID + ", " + (Shield_Slot.identified ? 1 : 0) + ", " + (Shield_Slot.equipped ? 1 : 0) + ", ";
        if (this.Shield_Slot == null) Character_Output += "Null, 0, 0, ";
        if (this.Weapon_Slot != null) Character_Output += this.Weapon_Slot.item_Class_ID + ", " + (Weapon_Slot.identified ? 1 : 0) + ", " + (Weapon_Slot.equipped ? 1 : 0) + ", ";
        if (this.Weapon_Slot == null) Character_Output += "Null, 0, 0, ";
        
        Character_Output += (this.Blind ? 1 : 0) + ", ";
        Character_Output += (this.Weak ? 1 : 0) + ", ";
        Character_Output += (this.Slow ? 1 : 0) + ", ";
        Character_Output += (this.Frail ? 1 : 0) + ", ";
        Character_Output += (this.ManaBurn ? 1 : 0) + ", ";
        Character_Output += (this.Poison ? 1 : 0) + ", ";
        Character_Output += (this.Stun ? 1 : 0) + ", ";
        Character_Output += (this.Paralyze ? 1 : 0) + ", ";
        Character_Output += (this.Stone ? 1 : 0) + ", ";
        Character_Output += (this.Dead ? 1 : 0) + ", ";
        Character_Output += (this.Ash ? 1 : 0) + ", ";
        Character_Output += (this.Blessed ? 1 : 0) + ", ";
        Character_Output += (this.Medium_Equipped ? 1 : 0) + ", ";
        Character_Output += (this.Heavy_Equipped ? 1 : 0) + ", ";
        Character_Output += (this.Caster ? 1 : 0) + ", ";
        Character_Output += (this.Skirmisher ? 1 : 0) + ", ";
        for (int i = 0; i < 5; i++) Character_Output += this.Spell_Slot[i] + ", " + this.Bonus_Slot[i] + ", " + this.Spells_Cast[i] + ", ";
        for (int i = 0; i < 100; i++) Character_Output += (this.SpellBook[i] ? 1 : 0) + ", ";
        Character_Output += this.effect_list.Count + ", ";
        if (this.effect_list.Count > 0) for (int i = 0; i < effect_list.Count; i++) Character_Output += this.effect_list[i].effect_name + ", ";
        if (this.effect_list.Count > 0) for (int i = 0; i < effect_list.Count; i++) Character_Output += this.effect_list[i].effect_time + ", ";
        if (this.effect_list.Count > 0) for (int i = 0; i < effect_list.Count; i++) Character_Output += this.effect_list[i].value + ", ";
        return Character_Output;
    }

    public void LoadCharacter(string _input)
    {
        string[] Character_Input = _input.Split(", ");
        this.ID = int.Parse(Character_Input[0]);
        this.Character_Name = Character_Input[1];
        this.Character_Class = CharacterClassEnum.none;
        if (Character_Input[2] == "Knight") this.Character_Class = CharacterClassEnum.Knight;
        if (Character_Input[2] == "Warrior") this.Character_Class = CharacterClassEnum.Warrior;
        if (Character_Input[2] == "Assassin") this.Character_Class = CharacterClassEnum.Assassin;
        if (Character_Input[2] == "Rogue") this.Character_Class = CharacterClassEnum.Rogue;
        if (Character_Input[2] == "Cleric") this.Character_Class = CharacterClassEnum.Cleric;
        if (Character_Input[2] == "Healer") this.Character_Class = CharacterClassEnum.Healer;
        if (Character_Input[2] == "Mage") this.Character_Class = CharacterClassEnum.Mage;
        this.Party_Slot = int.Parse(Character_Input[3]);
        this.Strength.ChangeBaseValue(int.Parse(Character_Input[4])); this.Strength.ModValue(int.Parse(Character_Input[5]));
        this.Dexterity.ChangeBaseValue(int.Parse(Character_Input[6])); this.Dexterity.ModValue(int.Parse(Character_Input[7]));
        this.Fortitude.ChangeBaseValue(int.Parse(Character_Input[8])); this.Fortitude.ModValue(int.Parse(Character_Input[9]));
        this.IQ.ChangeBaseValue(int.Parse(Character_Input[10])); this.IQ.ModValue(int.Parse(Character_Input[11]));
        this.Wisdom.ChangeBaseValue(int.Parse(Character_Input[12])); this.Wisdom.ModValue(int.Parse(Character_Input[13]));
        this.Charm.ChangeBaseValue(int.Parse(Character_Input[14])); this.Charm.ModValue(int.Parse(Character_Input[15]));
        this.XP_Level = int.Parse(Character_Input[16]); this.XP = int.Parse(Character_Input[17]); this.XP_NNL = int.Parse(Character_Input[18]);
        this.HP = int.Parse(Character_Input[19]); this.HP_Max = int.Parse(Character_Input[20]);
        this.Init_Bonus = int.Parse(Character_Input[21]); this.Num_Attacks = int.Parse(Character_Input[22]); this.Attack_Bonus = int.Parse(Character_Input[23]);
        this.Damage_Bonus = int.Parse(Character_Input[24]); this.Min_Damage = int.Parse(Character_Input[25]); this.Max_Damage = int.Parse(Character_Input[26]);
        this.Dodge = int.Parse(Character_Input[27]); this.Crit = int.Parse(Character_Input[28]); this.AC = int.Parse(Character_Input[29]); this.Regen = int.Parse(Character_Input[30]);
        this.Fire_Resist = int.Parse(Character_Input[31]); this.Ice_Resist = int.Parse(Character_Input[32]); this.Shock_Resist = int.Parse(Character_Input[33]); this.Magic_Resist = int.Parse(Character_Input[34]);

        if (Character_Input[35] != "Null") this.Head_Slot = new Item(int.Parse(Character_Input[35]), (int.Parse(Character_Input[36]) == 0 ? false : true), (int.Parse(Character_Input[37]) == 0 ? false : true));
        if (Character_Input[35] == "Null") this.Head_Slot = null;
        if (Character_Input[38] != "Null") this.Neck_Slot = new Item(int.Parse(Character_Input[38]), (int.Parse(Character_Input[39]) == 0 ? false : true), (int.Parse(Character_Input[40]) == 0 ? false : true));
        if (Character_Input[38] == "Null") this.Neck_Slot = null;
        if (Character_Input[41] != "Null") this.Cloak_Slot = new Item(int.Parse(Character_Input[41]), (int.Parse(Character_Input[42]) == 0 ? false : true), (int.Parse(Character_Input[43]) == 0 ? false : true));
        if (Character_Input[41] == "Null") this.Cloak_Slot = null;
        if (Character_Input[44] != "Null") this.RightFinger_Slot = new Item(int.Parse(Character_Input[44]), (int.Parse(Character_Input[45]) == 0 ? false : true), (int.Parse(Character_Input[46]) == 0 ? false : true));
        if (Character_Input[44] == "Null") this.RightFinger_Slot = null;
        if (Character_Input[47] != "Null") this.LeftFinger_Slot = new Item(int.Parse(Character_Input[47]), (int.Parse(Character_Input[48]) == 0 ? false : true), (int.Parse(Character_Input[49]) == 0 ? false : true));
        if (Character_Input[47] == "Null") this.LeftFinger_Slot = null;
        if (Character_Input[50] != "Null") this.Armor_Slot = new Item(int.Parse(Character_Input[50]), (int.Parse(Character_Input[51]) == 0 ? false : true), (int.Parse(Character_Input[52]) == 0 ? false : true));
        if (Character_Input[50] == "Null") this.Armor_Slot = null;
        if (Character_Input[53] != "Null") this.Shield_Slot = new Item(int.Parse(Character_Input[53]), (int.Parse(Character_Input[54]) == 0 ? false : true), (int.Parse(Character_Input[55]) == 0 ? false : true));
        if (Character_Input[53] == "Null") this.Shield_Slot = null;
        if (Character_Input[56] != "Null") this.Weapon_Slot = new Item(int.Parse(Character_Input[56]), (int.Parse(Character_Input[57]) == 0 ? false : true), (int.Parse(Character_Input[58]) == 0 ? false : true));
        if (Character_Input[56] == "Null") this.Weapon_Slot = null;

        this.Blind = (int.Parse(Character_Input[59]) == 0 ? false : true);
        this.Blind = (int.Parse(Character_Input[60]) == 0 ? false : true);
        this.Slow = (int.Parse(Character_Input[61]) == 0 ? false : true);
        this.Frail = (int.Parse(Character_Input[62]) == 0 ? false : true);
        this.ManaBurn = (int.Parse(Character_Input[63]) == 0 ? false : true);
        this.Poison = (int.Parse(Character_Input[64]) == 0 ? false : true);
        this.Stun = (int.Parse(Character_Input[65]) == 0 ? false : true);
        this.Paralyze = (int.Parse(Character_Input[66]) == 0 ? false : true);
        this.Stone = (int.Parse(Character_Input[67]) == 0 ? false : true);
        this.Dead = (int.Parse(Character_Input[68]) == 0 ? false : true);
        this.Ash = (int.Parse(Character_Input[69]) == 0 ? false : true);
        this.Blessed = (int.Parse(Character_Input[70]) == 0 ? false : true);
        this.Medium_Equipped = (int.Parse(Character_Input[71]) == 0 ? false : true);
        this.Heavy_Equipped = (int.Parse(Character_Input[72]) == 0 ? false : true);
        this.Caster = (int.Parse(Character_Input[73]) == 0 ? false : true);
        this.Skirmisher = (int.Parse(Character_Input[74]) == 0 ? false : true);
        this.Spell_Slot[0] = int.Parse(Character_Input[75]); //<-this gross and bullshit
        this.Bonus_Slot[0] = int.Parse(Character_Input[76]);
        this.Spells_Cast[0] = int.Parse(Character_Input[77]);
        this.Spell_Slot[1] = int.Parse(Character_Input[78]);
        this.Bonus_Slot[1] = int.Parse(Character_Input[79]);
        this.Spells_Cast[1] = int.Parse(Character_Input[80]);
        this.Spell_Slot[2] = int.Parse(Character_Input[81]);
        this.Bonus_Slot[2] = int.Parse(Character_Input[82]);
        this.Spells_Cast[2] = int.Parse(Character_Input[83]);
        this.Spell_Slot[3] = int.Parse(Character_Input[84]);
        this.Bonus_Slot[3] = int.Parse(Character_Input[85]);
        this.Spells_Cast[3] = int.Parse(Character_Input[86]);
        this.Spell_Slot[4] = int.Parse(Character_Input[87]);
        this.Bonus_Slot[4] = int.Parse(Character_Input[88]);
        this.Spells_Cast[4] = int.Parse(Character_Input[89]);
        for (int i = 90; i < 190; i++)
            this.SpellBook[i - 90] = (int.Parse(Character_Input[i]) == 0 ? false : true);
        int _fxNum = int.Parse(Character_Input[190]); this.effect_list.Clear();
        if(_fxNum > 0)
        { 
            for (int i = 0; i < _fxNum; i++) this.effect_list.Add(new Effect_C(Character_Input[i + 191],
                                                                           int.Parse(Character_Input[i + 191 + _fxNum]),
                                                                           int.Parse(Character_Input[i + 191 + _fxNum * 2]))); 
        }
    }
}

