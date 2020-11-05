using UnityEngine;
using UnityEngine.UI;

public class InGameMenuController : BaseMenuController
{
    [SerializeField] private Button _restart;
    [SerializeField] private Button _mainMenu;

    protected override void Start()
    {
        base.Start();
        _play.onClick.AddListener(OnPlayClicked);
        _restart.onClick.AddListener(_manager.Restart);
        _mainMenu.onClick.AddListener(OnMainMenuClicked);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _play.onClick.RemoveListener(OnPlayClicked);
        _restart.onClick.RemoveListener(_manager.Restart);
        _mainMenu.onClick.RemoveListener(OnMainMenuClicked);
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            OnPlayClicked();
    }

    protected override void OnPlayClicked()
    {
        base.OnPlayClicked();
        Time.timeScale = _menu.activeInHierarchy ? 0 : 1;
    }
    private void OnMainMenuClicked()
    {
        _manager.ChangeLvl((int)Scenes.MainMenu);
    }
}
