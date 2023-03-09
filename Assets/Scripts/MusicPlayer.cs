using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Sound[] sounds;
    public string musicToPlay;
    public int indexToPlay;
    public AudioSource currMusic;
    public Sound currMusicSound;
    public bool soundLoaded = false;
    /*void Awake(){
        foreach (Sound sound in sounds){
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            //sound.lowPassFilter = gameObject.AddComponent<AudioLowPassFilter>();
            sound.audioSource.loop = sound.loop;
        }
        //start loop music
        //playSound("test");
        currMusic = sounds[indexToPlay].audioSource;
        currMusicSound = sounds[indexToPlay];
        soundLoaded = true;
    }*/
    public void initSound(){
        foreach (Sound sound in sounds){
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            //sound.lowPassFilter = gameObject.AddComponent<AudioLowPassFilter>();
            sound.audioSource.loop = sound.loop;
        }
        //start loop music
        //playSound("test");
        currMusic = sounds[indexToPlay].audioSource;
        currMusicSound = sounds[indexToPlay];
        soundLoaded = true;
    }
    void Start()
    {
       //playSound(musicToPlay);
       currMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("MUSIC TIME: " + sounds[indexToPlay].audioSource.time);
        //Debug.Log("TIME SAMPLE: " + sounds[indexToPlay].audioSource.timeSamples);
    }
    public Sound playSound(string name){
        Sound currSound = new Sound();
        foreach (Sound sound in sounds){
            //Debug.Log("playing sound");
            if (sound.name == name){
                sound.audioSource.Play();
                currSound = sound;
            }
        }
        return currSound;
    }
}
