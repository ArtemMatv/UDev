using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryNS
{
    public class Inventory : IEnumerable<InventoryItem>
    {
        private readonly int _capacity;
        private readonly List<InventoryItem> inventory;
        private readonly List<InventoryItem> equipment;
        public Action<InventoryItem> Dropped { get; set; }
        public Inventory(int capacity)
        {
            inventory = new List<InventoryItem>();
            _capacity = capacity;

            equipment = new List<InventoryItem>();
        }

        internal bool SetEquipment(InventoryItem item)
        {
            if (!(item.Item is Equipment))
                return false;

            var equipmentItem = item.Item as Equipment;
            var itemEq = equipment.Find(el => ((Equipment)el.Item).Type == equipmentItem.Type);

            if (itemEq == null)
            {
                equipment.Add(item);
                inventory.Remove(item);
                return true;
            }
            else if (itemEq != item)
            {
                equipment.Remove(itemEq);
                equipment.Add(item);
                inventory.Remove(item);
                itemEq.Position = FindPosition();
                inventory.Add(itemEq);
                return true;
            }
            else
            {
                equipment.Remove(item);
                item.Position = FindPosition();
                inventory.Add(item);
                return true;
            }
        }

        internal List<InventoryItem> GetEquipment()
        {
            return equipment;
        }

        public bool AddToInventory(InventoryItem item)
        {
            if (inventory.Count >= _capacity)
                return false;

            item.Owner = this;
            item.Position = FindPosition();
            inventory.Add(item);

            return true;
        }

        private int FindPosition()
        {
            int position = 0;

            while (position < inventory.Count)
            {
                int pos = 1;
                foreach (var item in inventory)
                {
                    if (item.Position == position)
                        break;
                    pos++;
                }

                if (pos > inventory.Count)
                    return position;

                position++;
            }
            return position;

        }

        public void RemoveItem(InventoryItem item)
        {
            inventory.Remove(item);
            item.Owner = null;
            Dropped(item);
        }

        public IEnumerator<InventoryItem> GetEnumerator()
        {
            return inventory.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return inventory.GetEnumerator();
        }
    }
}
