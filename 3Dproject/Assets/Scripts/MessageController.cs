using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private TMP_Text _message;

    private static MessageController instance = null;

    public static MessageController GetInstance() => instance;
    void Awake()
    {
        if (instance == null)
        { 
            instance = this; 
        }
        else if (instance == this)
        {
            Destroy(gameObject); 
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Show(string message, string messageText, UnityAction call)
    {
        Time.timeScale = 0;

        _message.text = message;

        _buttonText.text = messageText;

        _button.onClick.AddListener(OnClickListener);
        _button.onClick.AddListener(call);

        _canvas.gameObject.SetActive(true);
    }

    private void OnClickListener()
    {
        _canvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        _button.onClick.RemoveAllListeners();
    }
}
