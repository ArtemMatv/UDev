using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InventoryNS
{
    public abstract class ItemBase : ScriptableObject
    {
        [SerializeField] private ItemId _itemId;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _cost;
        [SerializeField] private int _stackCount;
        [SerializeField] private Sprite _inventoryIcon;

        public ItemId ItemId => _itemId;
        public string Name => _name;
        public string Description => _description;
        public int Cost => _cost;
        public int StackCount => _stackCount;
        public Sprite InventoryIcon => _inventoryIcon;
    }

    [CreateAssetMenu(fileName = "Consumable", menuName = "Item/Consumable")]
    public class Consumable : ItemBase
    {

    }

    [CreateAssetMenu(fileName = "Readable", menuName = "Item/Readable")]
    public class Readable : ItemBase
    {
        [SerializeField] private string _text;
        public string Text => _text;
    }

    public abstract class StatItemBase : ItemBase
    {
        [SerializeField] private int _requiredLevel;
        [SerializeField] private Stat[] _primaryStat;

        public int RequiredLevel => _requiredLevel;
        public Stat[] PrimaryStat => _primaryStat;
    }

    [CreateAssetMenu(fileName = "Equipment", menuName = "Item/Equipment")]
    public class Equipment : StatItemBase
    {
        [SerializeField] private EquipmentType _type;

        public EquipmentType Type => _type;

        private void Awake()
        {
            EditorUtility.SetDirty(this);
        }
    }
}
