using System;
using UnityEngine;
using UnityEngine.Audio; 

[Serializable]
public class Sound
{
    public string soundName; 
    public AudioSource source;
    public AudioClip clip;
    public AudioMixerGroup group; 
}
