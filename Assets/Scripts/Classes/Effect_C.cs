using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_C
{
    public string effect_name;
    public int effect_time;
    public int value;
    
    public Effect_C(string _n = "", int _t = 0, int _v = 0)
    {
        this.effect_name = _n;
        this.effect_time = _t;
        this.value = _v;
    }
}
