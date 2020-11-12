using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] _sounds;

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
        foreach(Sound s in _sounds)
        {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
            s.AudioSource.clip = s.AudioClip;
            s.AudioSource.volume = s.Volume;
            s.AudioSource.pitch = s.Pitch;
            s.AudioSource.loop = s.Loop;
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
