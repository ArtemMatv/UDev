using InventoryNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Interactables
{
    class Item : MonoBehaviour, IInteractable
    {
        private MessageController controller;
        private float _radius;
        public InventoryItem _item;
        public Inventory PickUpTarget { private get; set; }

        void Start()
        {
            _radius = 1;
            controller = MessageController.GetInstance();
        }
        public float GetRadius()
        {
            return _radius;
        }

        public void Interact()
        {
            controller.Show(_item.Item.ItemId.ToString(), "Take", OnClickListener);
        }

        void OnClickListener()
        {
            if (PickUpTarget.AddToInventory(_item))
                Destroy(gameObject);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, _radius);
        }
    }
}
