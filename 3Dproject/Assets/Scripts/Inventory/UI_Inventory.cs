using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.AI;
using UnityEngine;
using Assets;
using TMPro;

namespace InventoryNS
{
    class UI_Inventory : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private TMP_Text text;
        private Inventory inventory;
        private void Awake()
        {
            inventory = playerController.GetInventory();
            text.text = "";
        }

        public void Enable()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
            DrawInventory();
        }

        void DrawInventory()
        {
            foreach (InventoryItem item in inventory)
            {
                text.text += item.Item.Name + " ";
            }
        }
    }
}
