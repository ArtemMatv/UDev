using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.AI;
using UnityEngine;
using Assets;
using TMPro;
using UnityEngine.UI;

namespace InventoryNS
{
    class UI_Inventory : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private GameObject _inventoryHandler;
        [SerializeField] private GameObject _equipmentHandler;
        [SerializeField] private GameObject image;
        private Inventory inventory;
        private List<InventoryItem> inventoryEquipment;
        private IItemUIController[] items;
        private IItemUIController[] equipments;

        private IItemUIController movable;
        private void Awake()
        {
            inventory = playerController.GetInventory();
            inventoryEquipment = inventory.GetEquipment();

            items = _inventoryHandler.GetComponentsInChildren<ItemUIController>();
            equipments = _equipmentHandler.GetComponentsInChildren<EquipmentUIController>();

            ItemRevertDrag img = image.GetComponent<ItemRevertDrag>();
            img.RightClick += RightClick;
            img.LeftClick += ChangePosition;
            img.LeftClickAboveEquipment += ChangeEquipment;
            img.DropItem += DropItem;

            for (int i = 0; i < items.Length; i++)
            {
                items[i].Position = i;
                items[i].OnLeftClick += LeftClick;
                items[i].OnRightClick += UseItem;

                img.RightClick += items[i].BackBackground;
            }

            foreach (var item in equipments)
            {
                item.OnRightClick += OnEquipmentRightClick;
                item.OnLeftClick += OnEquipmentLeftClick;
                img.RightClick += item.BackBackground;
            }
        }

        private void OnEquipmentLeftClick(IItemUIController obj)
        {
            movable = obj;
            if (obj == null)
                image.SetActive(false);

            if (obj?.GetItem() != null)
            {
                image.SetActive(true);
                image.GetComponent<Image>().sprite = obj.GetItem().Item.InventoryIcon;
            }
        }

        private void ChangeEquipment(EquipmentUIController obj)
        {
            if (obj != null)
                if (((Equipment)movable?.GetItem().Item).Type == obj.EquipmentType)
                {
                    if (obj.GetItem() != null && inventory.Robe(movable.GetItem())
                        || obj.GetItem() != null && inventory.Robe(obj.GetItem())
                        || movable.GetItem().Use())
                    {
                        DrawInventory();
                        LeftClick(null);
                    }
                }
        }

        private void OnEquipmentRightClick(InventoryItem obj)
        {
            if (obj.Use())
                DrawInventory();
        }

        private void DropItem()
        {
            inventory.RemoveItem(movable.GetItem());
            movable.SetItem(null);
            LeftClick(null);
        }

        private void UseItem(InventoryItem obj)
        {
            if (obj.Use())
                DrawInventory();
        }

        private void ChangePosition(ItemUIController obj)
        {
            if (obj == null)
                return;

            if (obj.GetItem() == null)
            {
                if (movable is EquipmentUIController)
                    inventory.Unrobe(movable.GetItem(), obj.Position);

                obj.SetItem(movable.GetItem());
                movable.SetItem(null);
                LeftClick(null);
            }
            else if (obj.GetItem() == movable.GetItem())
            {
                LeftClick(null);
                obj.BackBackground();
            }
            else
            {
                if (movable is EquipmentUIController)
                    inventory.Unrobe(movable.GetItem(), obj.Position);

                var objItem = obj.GetItem();
                var movableItem = movable.GetItem();
                obj.SetItem(null);
                movable.SetItem(null);
                obj.SetItem(movableItem);
                movable.SetItem(objItem, false);

                LeftClick(movable);
            }
        }

        void Swap(ref InventoryItem item1, ref InventoryItem item2)
        {
            InventoryItem temp = item1;
            item1 = item2;
            item2 = temp;
        }

        private void LeftClick(IItemUIController obj)
        {
            movable = obj;
            if (obj == null)
                image.SetActive(false);

            if (obj?.GetItem() != null)
            {
                image.SetActive(true);
                image.GetComponent<Image>().sprite = obj.GetItem().Item.InventoryIcon;
            }

        }

        private void RightClick()
        {
            if (movable != null)
            {
                LeftClick(null);
            }
        }

        private void Update()
        {
            if (movable?.GetItem() != null)
                image.transform.position = Input.mousePosition;
        }

        public void Enable()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
            LeftClick(null);

            if (gameObject.activeInHierarchy)
                DrawInventory();
        }

        void DrawInventory()
        {
            foreach (ItemUIController item in items)
            {
                item.SetItem(null);
            }

            foreach (InventoryItem item in inventory)
            {
                items[item.Position].SetItem(item);
            }

            foreach (EquipmentUIController item in equipments)
            {
                item.SetItem(null);
            }

            foreach (InventoryItem item in inventoryEquipment)
            {
                equipments.First(el => (el as EquipmentUIController).EquipmentType == ((Equipment)item.Item).Type).SetItem(item);
            }
        }
    }
}
