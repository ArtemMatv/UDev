using System;
using UnityEngine;

[Serializable]
public class HeroArmyController : ArmyController
{
    [SerializeField] private Hero _armyHero;
    public Hero ArmyHero => _armyHero;

    protected virtual void Move()
    {
        //move close to nest
    }
}
