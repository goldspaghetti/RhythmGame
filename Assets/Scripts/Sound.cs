using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;
    [HideInInspector]
    public AudioSource audioSource;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public bool loop;
    public int bpm;
    //public AudioLowPassFilter lowPassFilter;
    //rip doesn't allow initialization from this
    /*public Sound(){
        this.audioSource = new AudioSource();
        audioSource.volume = volume;
        audioSource.pitch = pitch;
    }*/
}
