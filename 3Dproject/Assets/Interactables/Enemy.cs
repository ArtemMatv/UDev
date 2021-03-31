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
        public void Interact()
        {
            Time.timeScale = 0;
            _message.text = "Enemy!";
            _buttonText.text = "Fight!!!";
            _button.onClick.AddListener(() => {
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;
            });
            canvas.gameObject.SetActive(true);
        }
    }
}
