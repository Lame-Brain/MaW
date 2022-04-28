using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute_C
{
    public int BaseValue { get; private set; }

    public int _valueModifier {get; private set; }


    public Attribute_C(int v)
    {
        this.BaseValue = v;
        this._valueModifier = 0;
    }

    public void ChangeBaseValue(int i)
    {
        this.BaseValue = i;
    }

    public void ModValue(int v)
    {
        this._valueModifier += v;
    }

    public int Mod()
    {
        int result = (BaseValue + _valueModifier) / 2 - 5;

        return result;
    }

    public int Value()
    {
        int result = BaseValue + _valueModifier;
        if (result < 0) result = 0;

        return result;
    }
    public bool StatCheck(int dc)
    {
        int roll = Random.Range(1, 21);
        if (roll + Mod() >= dc) return true;
        return false;
    }
}
