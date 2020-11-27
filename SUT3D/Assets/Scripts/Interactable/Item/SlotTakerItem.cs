using System;
using UnityEngine;

[Serializable]
public class SlotTakerItem : ItemBaseController
{
    [SerializeField] private Slot _type;
    [SerializeField] private Traits _traits;

    public Slot Type => _type;

    public Traits Traits => _traits;
}

public enum Slot
{
    Weapon,
    Arms,
    Torso,
    Legs,
    Foot,
    SpecialItem,
}

