using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMenuController : MonoBehaviour
{
    protected ServiceManager _manager;

    [Header("Main buttons")]
    [SerializeField] protected GameObject _menu;
    [SerializeField] protected Button _play;
    [SerializeField] protected Button _settings;
    [SerializeField] protected Button _quit;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _manager = ServiceManager.Instance;

        _quit.onClick.AddListener(_manager.Quit);
    }

    protected virtual void OnDestroy()
    {
        _quit.onClick.RemoveListener(_manager.Quit);
    }

    protected virtual void Update() { }

    protected virtual void OnPlayClicked()
    {
        _menu.SetActive(!_menu.activeInHierarchy);
    }
}
