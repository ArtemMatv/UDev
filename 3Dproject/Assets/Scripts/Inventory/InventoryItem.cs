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
        public int Position { get; set; }
        public ItemBase Item => _item;
        public Inventory Owner { get; set; }

        public bool Use()
        {
            if (_item is Equipment)
            {
                return Owner.SetEquipment(this);
            }
            return false;
        }

        public void Drop()
        {
            Owner.RemoveItem(this);
        }
    }
}
