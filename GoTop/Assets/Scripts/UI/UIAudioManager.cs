using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class UIAudioManager : MonoBehaviour
{
    [SerializeField] private UISound[] _sounds;

    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    public static UIAudioManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        foreach(UISound s in _sounds)
        {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
            s.AudioSource.clip = s.AudioClip;
            s.AudioSource.volume = s.Volume;
            s.AudioSource.pitch = s.Pitch;
            s.AudioSource.loop = s.Loop;
            s.AudioSource.outputAudioMixerGroup = _audioMixerGroup;
        }
    }

    public void Play(UIClipNames name)
    {
        _sounds.FirstOrDefault(e => e.ClipName == name)?.AudioSource.Play();
    }
      
}

public enum UIClipNames
{
    Play,
    Settings,   
    Quit,
    Default,
    sss
}
