using UnityEngine;

public class ConsumableItem : ItemBaseController
{
    [SerializeField] private ConsumeType _type;
    [SerializeField] private int _value;
    
    public ConsumeType Type => _type;
    public int Value => _value;
}

public enum ConsumeType
{
    Heal,
    Mana,
    Energy
}
