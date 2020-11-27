using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[Serializable]
public class Traits
{
    [SerializeField] public int Wisdom;
    //All the other traits
    public static Traits operator +(Traits traits, Traits traitsToAdd)
    {
        return new Traits() {
                        Wisdom = traits.Wisdom + traitsToAdd.Wisdom
                    };
    }
}

