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
    class Enemy : MonoBehaviour, IInteractable
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private TMP_Text _message;
        [SerializeField] private float _radius;

        public float GetRadius()
        {
            return _radius;
        }

        public void Interact()
        {
            Time.timeScale = 0;

            _message.text = "Dragooon";

            _buttonText.text = "Fight";

            _button.onClick.AddListener(() => {
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;
                _button.onClick.RemoveAllListeners();
            });

            canvas.gameObject.SetActive(true);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, _radius);
        }
    }
}
