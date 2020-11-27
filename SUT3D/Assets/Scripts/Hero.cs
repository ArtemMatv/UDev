using System;
using UnityEngine;

[Serializable]
public class Hero
{
    [SerializeField] public string Name;

    [SerializeField] public Traits HeroTraits;

    [SerializeField] public SlotTakerItem[] WearingItems = new SlotTakerItem[6];

}

