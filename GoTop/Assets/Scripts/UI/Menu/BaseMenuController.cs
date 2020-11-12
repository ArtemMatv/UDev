using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMenuController : MonoBehaviour
{
    protected ServiceManager _manager;
    protected UIAudioManager _audioManager;

    [Header("Main buttons")]
    [SerializeField] protected GameObject _menu;
    [SerializeField] protected Button _play;
    [SerializeField] protected Button _settings;
    [SerializeField] protected Button _quit;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _manager = ServiceManager.Instance;
        _audioManager = UIAudioManager.Instance;

        _play.onClick.AddListener(OnPlayClicked);
        _settings.onClick.AddListener(OnSettingsClicked);
        _quit.onClick.AddListener(OnQuitClicked);
    }

    protected virtual void OnDestroy()
    {
        _play.onClick.RemoveAllListeners();
        _settings.onClick.RemoveAllListeners();
        _quit.onClick.RemoveAllListeners();
    }

    protected virtual void Update() { }

    protected virtual void OnPlayClicked()
    {
        Debug.Log("setted: " + !_menu.activeInHierarchy);
        _menu.SetActive(!_menu.activeInHierarchy);
        
        _audioManager.Play(UIClipNames.Play);
    }

    protected virtual void OnSettingsClicked()
    {
        _audioManager.Play(UIClipNames.Settings);
    }

    protected virtual void OnQuitClicked()
    {
        _audioManager.Play(UIClipNames.Quit);
        _manager.Quit();
    }
}
