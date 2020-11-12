using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    public UIClipNames ClipName;
    public AudioClip AudioClip;

    [Range(0, 1)]
    public float Volume;
    [Range(-3, 3)]
    public float Pitch;

    public bool Loop;

    [HideInInspector]
    public AudioSource AudioSource;
}
