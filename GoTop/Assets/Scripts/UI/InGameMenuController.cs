using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuController : MonoBehaviour
{
    ServiceManager _manager;

    [SerializeField] private GameObject _menu;
    [SerializeField] private Button _resume;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _settings;
    [SerializeField] private Button _mainMenu;
    [SerializeField] private Button _quit;

    void Start()
    {
        _manager = ServiceManager.Instance;

        _resume.onClick.AddListener(ChangeMenuStatus);
        _restart.onClick.AddListener(_manager.Restart);
        _mainMenu.onClick.AddListener(GoToMainMenu);
        _quit.onClick.AddListener(_manager.Quit);
    }

    void OnDestroy()
    {
        _resume.onClick.RemoveListener(ChangeMenuStatus);
        _restart.onClick.RemoveListener(_manager.Restart);
        _mainMenu.onClick.RemoveListener(GoToMainMenu);
        _quit.onClick.RemoveListener(_manager.Quit);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            ChangeMenuStatus();
        
    }

    public void ChangeMenuStatus()
    {
        _menu.SetActive(!_menu.activeInHierarchy);
        Time.timeScale = _menu.activeInHierarchy ? 0 : 1;
    }
    private void GoToMainMenu()
    {
        _manager.ChangeLvl((int)Scenes.MainMenu);
    }
}
