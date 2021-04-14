using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InventoryNS
{
    [Serializable]
    public class InventoryItem
    {
        [SerializeField] protected ItemBase _item;

        public ItemBase Item => _item;
        public Inventory Owner { get; set; }

        public virtual void Use()
        {
            Debug.Log("InventoryItem");
        }

        public void Drop()
        {

        }
    }

    [Serializable]
    public class InventoryEquipment : InventoryItem
    {
        [SerializeField] public new Equipment _item;
        public InventoryEquipment(Equipment equipment)
        {
            _item = equipment;
        }

        public new Equipment Item => _item;
        public override void Use()
        {
            Debug.Log("InventoryEquipment");
            Owner.SetEquipment(this);
        }
    }
}
