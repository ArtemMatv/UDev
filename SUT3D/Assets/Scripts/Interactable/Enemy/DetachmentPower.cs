using System;
using UnityEngine;

[Serializable]
public class DetachmentPower
{
    public readonly string Name;
    public readonly int Health;
    public readonly int Damage;
    public readonly int Amount;

    public EnemiesTypes WeakAgainst;
    public EnemiesTypes StrongAgainst;

    public DetachmentPower(EnemiesTypes type, int amount)
    {
        Name = type.ToString() + 's';
        Amount = amount;

        switch (type)
        {
            case EnemiesTypes.CentawrWarrior:
                WeakAgainst = EnemiesTypes.Dwarf;
                StrongAgainst = EnemiesTypes.Elf;
                Health = amount * 100;
                Damage = amount * 20;
                break;
            case EnemiesTypes.Dwarf:
                WeakAgainst = EnemiesTypes.Elf;
                StrongAgainst = EnemiesTypes.CentawrWarrior;
                Health = amount * 80;
                Damage = amount * 15;
                break;
            case EnemiesTypes.Elf:
                WeakAgainst = EnemiesTypes.CentawrWarrior;
                StrongAgainst = EnemiesTypes.Dwarf;
                Health = amount * 50;
                Damage = amount * 20;
                break;
            default:
                break;
        }

    }
}

public enum EnemiesTypes
{
    CentawrWarrior,
    Dwarf,
    Elf
}
