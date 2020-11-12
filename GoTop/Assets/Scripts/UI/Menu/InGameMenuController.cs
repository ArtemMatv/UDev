using UnityEngine;
using UnityEngine.UI;

public class InGameMenuController : BaseMenuController
{
    [SerializeField] private Button _restart;
    [SerializeField] private Button _mainMenu;

    protected override void Start()
    {
        base.Start();
        _restart.onClick.AddListener(OnRestartClicked);
        _mainMenu.onClick.AddListener(OnMainMenuClicked);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _restart.onClick.RemoveAllListeners();
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

    protected virtual void OnRestartClicked()
    {
        _audioManager.Play(UIClipNames.Default);
        _manager.Restart();
    }

    private void OnMainMenuClicked()
    {
        _audioManager.Play(UIClipNames.Default);
        _manager.ChangeLvl((int)Scenes.MainMenu);
    }
}
