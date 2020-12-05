using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    public ClipName clipName;
    public AudioClip audioClip;

    [Range(0, 1)]
    public float volume;
    [Range(-3, 3)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource AudioSource;

    
}
