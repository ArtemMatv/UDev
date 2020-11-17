using System;
using UnityEngine;

[Serializable]
public class UISound : Sound
{
    public UIClipNames ClipName;
    [HideInInspector]
    public AudioSource AudioSource;
}