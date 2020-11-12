using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LvlButtonController : MonoBehaviour
{
    private Button _lvl;
    private TMP_Text _text;
    private UIAudioManager _audioManager;

    [SerializeField] private Scenes scene;

    void Start()
    {
        _audioManager = UIAudioManager.Instance;
        _lvl = GetComponent<Button>();
        GetComponentInChildren<TMP_Text>().text = ((int)scene).ToString();
        if (!PlayerPrefs.HasKey((int)scene + GamePrefs.LevelPlayed.ToString()))
        {
            _lvl.interactable = false;
            return;
        }
            
        _lvl.onClick.AddListener(OnLvlClicked);
        
    }

    private void OnDestroy()
    {
        _lvl.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void OnLvlClicked()
    {
        _audioManager.Play(UIClipNames.Default);
        ServiceManager.Instance.ChangeLvl((int)scene);
    }
}
