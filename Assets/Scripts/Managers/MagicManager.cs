using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    public void CastSpell(Character_C _caster, Spell_C _spell, Character_C _ally = null, Monster_C _monster = null)
    {
        //cancel if the spell is not valid for the current game state
        if ((_spell.battle && GameStateManager.GAME_STATE != "Battle") && (_spell.utility && GameStateManager.GAME_STATE != "Explore")) return;

        //cancel if the spell targets an enemy, and there is not enemy target
        if (_spell.spellAlignment == "Enemy" && _spell.spellTarget == "Single" && _monster == null) return;

        //cancel if the spell targers an ally, and there is no ally target
        if (_spell.spellAlignment == "Ally" && _spell.spellTarget == "Single" && _ally == null) return;

        if(_spell.spellEffectType == "Damage")
        {
            if (_spell.spellTarget == "Single") _monster.Damage(Random.Range(_spell.spellMin, _spell.spellMax) + Mathf.RoundToInt(_spell.spellMod * _caster.XP_Level));
            //if (_spell.spellTarget == "All") apply this to all monsters in battle: Damage(Random.Range(_spell.spellMin, _spell.spellMax) + Mathf.RoundToInt(_spell.spellMod * _caster.XP_Level));            
        }
        if(_spell.spellEffectType == "Inflict_Blind")
        {
            //if (_spell.spellTarget == "Single") _monster.blind = true;
            //if (_spell.spellTarget == "All") All Monsters.blind = true;
        }
        if(_spell.spellEffectType == "Inflict_Weak")
        {
            //if (_spell.spellTarget == "Single") _monster.weak = true;
            //if (_spell.spellTarget == "All") All Monsters.weak = true;
        }
        if(_spell.spellEffectType == "Inflict_Slow")
        {
            //if (_spell.spellTarget == "Single") _monster.slow = true;
            //if (_spell.spellTarget == "All") All Monsters.slow = true;
        }
        if(_spell.spellEffectType == "Inflict_Frail")
        {
            //if (_spell.spellTarget == "Single") _monster.frail = true;
            //if (_spell.spellTarget == "All") All Monsters.frail = true;
        }
        if(_spell.spellEffectType == "Reduce_Fire_Resist")
        {
            //if (_spell.spellTarget == "Single") _monster.reduce_fire_resist;
            //if (_spell.spellTarget == "All") All Monsters.reduce_fire_resist
        }
        if(_spell.spellEffectType == "Reduce_Ice_Resist")
        {
            //if (_spell.spellTarget == "Single") _monster.reduce_ice_resist;
            //if (_spell.spellTarget == "All") All Monsters.reduce_ice_resist
        }
        if(_spell.spellEffectType == "Reduce_Shock_Resist")
        {
            //if (_spell.spellTarget == "Single") _monster.reduce_shock_resist;
            //if (_spell.spellTarget == "All") All Monsters.reduce_shock_resist
        }
        if(_spell.spellEffectType == "Reduce_Magic_Resist")
        {
            //if (_spell.spellTarget == "Single") _monster.reduce_magic_resist;
            //if (_spell.spellTarget == "All") All Monsters.reduce_magic_resist
        }
        if(_spell.spellEffectType == "Kill")
        {
            //if (_spell.spellTarget == "Single") _monster.Kill;
            //if (_spell.spellTarget == "All") All Monsters.Kill;
        }
        if (_spell.spellEffectType == "Detec_Coord") ResourceManager.FindParty().detect_loc = 10;
        if (_spell.spellEffectType == "Detec_Facing") ResourceManager.FindParty().detect_dir = 10;
        if (_spell.spellEffectType == "Heal")
        {
            if (_spell.spellTarget == "Single")
            {
                _ally.Heal_Character(Random.Range(_spell.spellMin, _spell.spellMax) + Mathf.RoundToInt(_spell.spellMod * _caster.XP_Level));
                _ally.UpdateDerivedStats();
            }
            if (_spell.spellTarget == "All") 
            {
                Party_Class _party = ResourceManager.FindParty();
                for (int i = 0; i < _party.Party.Length; i++)
                {
                    _party.Party[i].Heal_Character(Random.Range(_spell.spellMin, _spell.spellMax) + Mathf.RoundToInt(_spell.spellMod * _caster.XP_Level));
                    _party.Party[i].UpdateDerivedStats();
                }
            }

        }
        if (_spell.spellEffectType == "Minor_Cure")
        {
            if (_spell.spellTarget == "Single") _ally.Minor_Cure();
            if (_spell.spellTarget == "All") 
            {
                Party_Class _party = ResourceManager.FindParty();
                for (int i = 0; i < _party.Party.Length; i++)
                    _party.Party[i].Minor_Cure();
            }
        }
        if (_spell.spellEffectType == "Major_Cure")
        {
            if (_spell.spellTarget == "Single") _ally.Major_Cure();
            if (_spell.spellTarget == "All") 
            {
                Party_Class _party = ResourceManager.FindParty();
                for (int i = 0; i < _party.Party.Length; i++)
                    _party.Party[i].Major_Cure();
            }
        }
        if (_spell.spellEffectType == "Critical_Cure")
        {
            if (_spell.spellTarget == "Single") _ally.Critical_Cure();
            if (_spell.spellTarget == "All") 
            {
                Party_Class _party = ResourceManager.FindParty();
                for (int i = 0; i < _party.Party.Length; i++)
                    _party.Party[i].Critical_Cure();
            }
        }
        if (_spell.spellEffectType == "Death_Cure")
        {
            if (_spell.spellTarget == "Single") _ally.Death_Cure();
        }
        if(_spell.spellEffectType == "Off_Buff")
        {
            if (_spell.spellTarget == "Single") _ally.Off_Buff(Random.Range(_spell.spellMin, _spell.spellMax) + Mathf.RoundToInt(_spell.spellMod * _caster.XP_Level));
            if (_spell.spellTarget == "All")
            {
                Party_Class _party = ResourceManager.FindParty();
                for (int i = 0; i < _party.Party.Length; i++)
                    _party.Party[i].Off_Buff(Random.Range(_spell.spellMin, _spell.spellMax) + Mathf.RoundToInt(_spell.spellMod * _caster.XP_Level));
            }
        }
        if(_spell.spellEffectType == "Def_Buff")
        {
            if (_spell.spellTarget == "Single") _ally.Def_Buff(Random.Range(_spell.spellMin, _spell.spellMax) + Mathf.RoundToInt(_spell.spellMod * _caster.XP_Level));
            if (_spell.spellTarget == "All")
            {
                Party_Class _party = ResourceManager.FindParty();
                for (int i = 0; i < _party.Party.Length; i++)
                    _party.Party[i].Def_Buff(Random.Range(_spell.spellMin, _spell.spellMax) + Mathf.RoundToInt(_spell.spellMod * _caster.XP_Level));
            }
        }
        if(_spell.spellEffectType == "Increase_Fire_Resist")
        {
            if (_spell.spellTarget == "Single")
            {
                _ally.effect_list.Add(new Effect_C("Increase_Fire_Resist", 10, 1));
                _ally.UpdateDerivedStats();
            }
            if (_spell.spellTarget == "All")
            {
                Party_Class _party = ResourceManager.FindParty();
                for (int i = 0; i < _party.Party.Length; i++)
                {
                    _party.Party[i].effect_list.Add(new Effect_C("Increase_Fire_Resist", 10, 1));
                    _party.Party[i].UpdateDerivedStats();
                }
            }
        }
        if(_spell.spellEffectType == "Increase_Ice_Resist")
        {
            if (_spell.spellTarget == "Single")
            {
                _ally.effect_list.Add(new Effect_C("Increase_Ice_Resist", 10, 1));
                _ally.UpdateDerivedStats();
            }
            if (_spell.spellTarget == "All")
            {
                Party_Class _party = ResourceManager.FindParty();
                for (int i = 0; i < _party.Party.Length; i++)
                {
                    _party.Party[i].effect_list.Add(new Effect_C("Increase_Ice_Resist", 10, 1));
                    _party.Party[i].UpdateDerivedStats();
                }
            }
        }
        if(_spell.spellEffectType == "Increase_Shock_Resist")
        {
            if (_spell.spellTarget == "Single")
            {
                _ally.effect_list.Add(new Effect_C("Increase_Shock_Resist", 10, 1));
                _ally.UpdateDerivedStats();
            }
            if (_spell.spellTarget == "All")
            {
                Party_Class _party = ResourceManager.FindParty();
                for (int i = 0; i < _party.Party.Length; i++)
                {
                    _party.Party[i].effect_list.Add(new Effect_C("Increase_Shock_Resist", 10, 1));
                    _party.Party[i].UpdateDerivedStats();
                }
            }
        }
        if(_spell.spellEffectType == "Increase_Magic_Resist")
        {
            if (_spell.spellTarget == "Single")
            {
                _ally.effect_list.Add(new Effect_C("Increase_Magic_Resist", 10, 1));
                _ally.UpdateDerivedStats();
            }
            if (_spell.spellTarget == "All")
            {
                Party_Class _party = ResourceManager.FindParty();
                for (int i = 0; i < _party.Party.Length; i++)
                {
                    _party.Party[i].effect_list.Add(new Effect_C("Increase_Magic_Resist", 10, 1));
                    _party.Party[i].UpdateDerivedStats();
                }
            }
        }
        if(_spell.spellEffectType == "Undo_Lock")
        {
            //TO DO: Implement this
        }
        if(_spell.spellEffectType == "Detect_Secret")
        {
            //TO DO: Implement this
        }
        if(_spell.spellEffectType == "Add_Light")
        {
            Party_Class _party = ResourceManager.FindParty();
            _party.Light += 125;
        }
        if(_spell.spellEffectType == "Float")
        {
            Party_Class _party = ResourceManager.FindParty();
            _party.floating += 10;
        }
        if (_spell.spellEffectType == "Blink")
        {
            //TO DO: Implement this
        }
        if (_spell.spellEffectType == "Teleport")
        {
            //TO DO: Implement this
        }
        if (_spell.spellEffectType == "Teleport_Up")
        {
            //TO DO: Implement this
        }
        if (_spell.spellEffectType == "Town_Portal")
        {
            //TO DO: Implement this
        }

    }
}
