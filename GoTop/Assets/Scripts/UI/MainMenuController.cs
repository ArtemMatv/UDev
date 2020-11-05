using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : BaseMenuController
{
    [SerializeField] private Button _chooseLvl;
    [SerializeField] private Button _reset;

    [SerializeField] private GameObject _lvlMenu;
    [SerializeField] private Button _closeLvlMenu;

    private int lvl = 1;

    protected override void Start()
    {
        base.Start();
        _chooseLvl.onClick.AddListener(OnUseLvlMenuClicked);
        _closeLvlMenu.onClick.AddListener(OnUseLvlMenuClicked);

        if (PlayerPrefs.HasKey(GamePrefs.LastLevelPlayed.ToString()))
        { 
            _play.GetComponentInChildren<TMP_Text>().text = "Resume";
            lvl = PlayerPrefs.GetInt(GamePrefs.LastLevelPlayed.ToString());
        }

        _play.onClick.AddListener(OnPlayClicked);

        _reset.onClick.AddListener(OnResetClicked);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _chooseLvl.onClick.RemoveListener(OnUseLvlMenuClicked);
        _closeLvlMenu.onClick.RemoveListener(OnUseLvlMenuClicked);
        _reset.onClick.RemoveListener(OnResetClicked);
    }

    private void OnUseLvlMenuClicked()
    {
        _lvlMenu.SetActive(!_lvlMenu.activeInHierarchy);
        base.OnPlayClicked();
    }

    protected override void OnPlayClicked()
    {
        base.OnPlayClicked();
        _manager.ChangeLvl(lvl);
    }

    private void OnResetClicked()
    {
        _play.GetComponentInChildren<TMP_Text>().text = "Play";
        lvl = 1;
        _manager.ResetProgress();
    }
}
