using Assets.Interactables;
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
        public Action<GameObject, EquipmentType> ChangeEquipment { get; internal set; }

        public Inventory(int capacity)
        {
            inventory = new List<InventoryItem>();
            _capacity = capacity;

            equipment = new List<InventoryItem>();
        }

        internal bool SetEquipment(InventoryItem item)
        {
            if (item == null)
                return false;
            if (!(item.Item is Equipment))
                return false;

            var itemEq = equipment.Find(el => ((Equipment)el.Item).Type == (item.Item as Equipment).Type);

            if (itemEq == null)
            {
                equipment.Add(item);
                inventory.Remove(item);

                var eq = Resources.Load("Prefabs/" + item.Item.ItemId) as GameObject;

                eq.GetComponent<Item>()._item = item;
                ChangeEquipment(eq, ((Equipment)item.Item).Type);
                return true;
            }
            else if (itemEq != item)
            {
                equipment.Remove(itemEq);
                equipment.Add(item);
                inventory.Remove(item);
                itemEq.Position = FindPosition();
                inventory.Add(itemEq);

                var eq = Resources.Load("Prefabs/" + item.Item.ItemId) as GameObject;

                eq.GetComponent<Item>()._item = item;
                ChangeEquipment(eq, ((Equipment)itemEq.Item).Type);

                return true;
            }
            else
            {
                equipment.Remove(item);
                item.Position = FindPosition();
                inventory.Add(item);

                ChangeEquipment(null, ((Equipment)itemEq.Item).Type);
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

        internal bool Robe(InventoryItem item)
        {
            if (!(item.Item is Equipment))
                return false;

            if (equipment.Contains(item))
                return true;
            else
                return SetEquipment(item);
        }

        internal bool Unrobe(InventoryItem item, int position = -1)
        {
            if(!(item.Item is Equipment))
                return false;

            if (equipment.Contains(item))
            {
                equipment.Remove(item);
                
                if (position == -1)
                    position = FindPosition();

                item.Position = position;
                inventory.Add(item);

                ChangeEquipment(null, ((Equipment)item.Item).Type);

                return true;
            }
            else
                return false;
        }

        public void RemoveItem(InventoryItem item)
        {
            inventory.Remove(item);
            equipment.Remove(item);
            item.Owner = null;
            ChangeEquipment(null, ((Equipment)item.Item).Type);
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
