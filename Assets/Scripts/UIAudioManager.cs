using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

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
        foreach (Sound s in sounds)
        {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
            s.AudioSource.clip = s.audioClip;
            s.AudioSource.volume = s.volume;
            s.AudioSource.pitch = s.pitch;
            s.AudioSource.loop = s.loop;
        }
    }

    public void Play(ClipName name)
    {
        Sound sound = Array.Find(sounds, s => s.clipName == name);

        if (sound != null)
            sound.AudioSource.Play();
        else
            Debug.LogError("Wrong name of the clip -" + name);
    }
}
public enum ClipName
{
    Play,
    Quit,
    Settings,

}