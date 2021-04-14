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

        private Equipment _weapon;
        private Equipment _shield;
        private Equipment _helmet;
        private Equipment _chest;
        private Equipment _leggins;

        public Inventory(int capacity)
        {
            inventory = new List<InventoryItem>();
            _capacity = capacity;
        }

        public bool AddToInventory(InventoryItem item)
        {
            if (inventory.Count >= _capacity)
                return false;

            item.Owner = this;
            inventory.Add(item);

            return true;
        }

        public void SetEquipment(InventoryEquipment item)
        {
            if (item.Owner != this)
                return;

            switch (item.Item.Type)
            {
                case EquipmentType.Weapon:
                    if (_weapon == null)
                    {
                        _weapon = item.Item;
                        RemoveItem(item);
                    }
                    else
                    {
                        RemoveItem(item);
                        _weapon = item.Item;
                        AddToInventory(new InventoryEquipment(_weapon));
                    }
                    break;
                case EquipmentType.Shield:
                    if (_shield == null)
                    {
                        _shield = item.Item;
                        RemoveItem(item);
                    }
                    else
                    {
                        RemoveItem(item);
                        _shield = item.Item;
                        AddToInventory(new InventoryEquipment(_shield));
                    }
                    break;
                case EquipmentType.Helmet:
                    if (_helmet == null)
                    {
                        _helmet = item.Item;
                        RemoveItem(item);
                    }
                    else
                    {
                        RemoveItem(item);
                        _helmet = item.Item;
                        AddToInventory(new InventoryEquipment(_helmet));
                    }
                    break;
                case EquipmentType.Chest:
                    if (_chest == null)
                    {
                        _chest = item.Item;
                        RemoveItem(item);
                    }
                    else
                    {
                        RemoveItem(item);
                        _chest = item.Item;
                        AddToInventory(new InventoryEquipment(_chest));
                    }
                    break;
                case EquipmentType.Leggins:
                    if (_leggins == null)
                    {
                        _leggins = item.Item;
                        RemoveItem(item);
                    }
                    else
                    {
                        RemoveItem(item);
                        _leggins = item.Item;
                        AddToInventory(new InventoryEquipment(_leggins));
                    }
                    break;
                default:
                    break;
            }
        }

        void RemoveItem(InventoryItem item)
        {
            inventory.Remove(item);
            item.Owner = null;
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
