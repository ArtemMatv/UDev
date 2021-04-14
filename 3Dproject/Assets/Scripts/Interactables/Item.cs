using InventoryNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Interactables
{
    class Item : MonoBehaviour, IInteractable
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private TMP_Text _message;
        [SerializeField] private float _radius;
        [SerializeField] private InventoryItem _item;
        public Inventory PickUpTarget { private get; set; }

        public float GetRadius()
        {
            return _radius;
        }

        public void Interact()
        {
            Time.timeScale = 0;

            _message.text = "Great sword!!!";

            _buttonText.text = "Take";

            _button.onClick.AddListener(OnClickListener);

            canvas.gameObject.SetActive(true);
        }

        void OnClickListener()
        {
            canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            _button.onClick.RemoveAllListeners();

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
