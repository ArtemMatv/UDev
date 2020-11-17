using UnityEngine;

public abstract class Sound
{
    public AudioClip AudioClip;

    [Range(0, 1)]
    public float Volume;
    [Range(-3, 3)]
    public float Pitch;

    public bool Loop;
}