using System;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] private Slider _volume;
    [SerializeField] private AudioMixer _masterMixer;
    private float _selectedVolume;

    [Space]
    [SerializeField] private Toggle _fullScreen;
    private bool _selectedFullScreen;

    [Space]
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    private Resolution[] _availableResolutions;
    private Resolution _selectedResolution;

    [Space]
    [SerializeField] private TMP_Dropdown _qualityDropdown;
    private string[] _qualityLevels;
    private int _selectedQuality;

    [Space]
    [SerializeField] private Button _apply;

    void Start()
    {
        #region ApplyButton
        _masterMixer.GetFloat("Volume", out _selectedVolume);
        _selectedFullScreen = Screen.fullScreen;
        _selectedResolution = Screen.currentResolution;
        _selectedQuality = QualitySettings.GetQualityLevel();

        _apply.onClick.AddListener(OnApplyButtonClicked);
        #endregion

        #region Volume
        _volume.value = _selectedVolume;
        _volume.onValueChanged.AddListener(OnVolumeChanged);
        #endregion

        #region FullScreen
        _fullScreen.onValueChanged.AddListener(OnFullScreenChanged);
        #endregion

        #region Resolution
        _availableResolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();

        Resolution own;
        List<string> options = new List<string>();
        foreach (Resolution item in _availableResolutions)
        {
            if (item.width <= 800)
                continue;

            options.Add(item.width + "x" + item.height);

            if (item.width == Screen.width && item.height == Screen.height)
                own = item;
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = Array.FindIndex(_availableResolutions,
                                                    item => item.width == Screen.currentResolution.width 
                                                            && item.height == Screen.currentResolution.height);

        _resolutionDropdown.RefreshShownValue();

        _resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
        #endregion

        #region Quality
        _qualityLevels = QualitySettings.names;
        _qualityDropdown.onValueChanged.AddListener(OnQualityChanged);

        _qualityDropdown.ClearOptions();
        _qualityDropdown.AddOptions(_qualityLevels.ToList());
        _qualityDropdown.value = QualitySettings.GetQualityLevel();
        _qualityDropdown.RefreshShownValue();
        #endregion
    }

    void onDestroy()
    {
        _volume.onValueChanged.RemoveListener(OnVolumeChanged);
        _fullScreen.onValueChanged.RemoveListener(OnFullScreenChanged);
        _resolutionDropdown.onValueChanged.RemoveListener(OnResolutionChanged);
        _qualityDropdown.onValueChanged.RemoveListener(OnQualityChanged);
        _apply.onClick.RemoveAllListeners();
    }
    private void OnVolumeChanged(float value)
    {
        _selectedVolume = value;
    }

    private void OnFullScreenChanged(bool value)
    {
        _selectedFullScreen = value;
    }

    private void OnResolutionChanged(int index)
    {
        _selectedResolution = _availableResolutions[index];
    }

    private void OnQualityChanged(int index)
    {
        _selectedQuality = index;
    }

    private void OnApplyButtonClicked()
    {
        QualitySettings.SetQualityLevel(_selectedQuality);

        Screen.SetResolution(_selectedResolution.width,
                             _selectedResolution.height,
                             Screen.fullScreen);

        Screen.fullScreen = _selectedFullScreen;

        _masterMixer.SetFloat("Volume", _selectedVolume);

        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void CloseMenu()
    {
        _masterMixer.GetFloat("Volume", out _selectedVolume);
        _selectedFullScreen = Screen.fullScreen;
        _selectedResolution = Screen.currentResolution;
        _selectedQuality = QualitySettings.GetQualityLevel();
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
