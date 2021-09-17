using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public CustomSound[] sounds;
    public static AudioManagerScript instance;

    [HideInInspector] public bool isMute = false;

    void Awake()
    {
        //doesn't cut the background music that starts playing from the mainMenu
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        foreach (var item in sounds)
        {
            item.source = gameObject.AddComponent<AudioSource>();
            item.source.clip = item.clip;
            item.source.volume = item.volume;
            item.source.pitch = item.pitch;
            item.source.loop = item.loop;
        }
    }

    void Start()
    {
        playSound("TitleMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(string name)
    {
        CustomSound s = null;
        foreach (var item in sounds)
        {
            if(item.name == name)
            {
                s = item; 
            }
        }
        if(s==null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        
        //Before code
        s.source.Play();
        

        /*
        //sir JR VERSION
        //Manually load the clip first
        s.clip.LoadAudioData();
        //Checks if clip is done loading
        if(s.clip.loadState == AudioDataLoadState.Loaded && !s.source.isPlaying)
        {
            //plays the sound clip
            s.source.PlayOneShot(s.clip);
        }
        */
    }
    public void playSoundOne(string name)
    {
        CustomSound s = null;
        foreach (var item in sounds)
        {
            if (item.name == name)
            {
                s = item;
            }
        }
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }

        //sir JR VERSION
        //Manually load the clip first
        s.clip.LoadAudioData();
        //Checks if clip is done loading
        if(s.clip.loadState == AudioDataLoadState.Loaded && !s.source.isPlaying)
        {
            //plays the sound clip
            s.source.PlayOneShot(s.clip);
        }
    }

    public void muteAllSound()
    {
        foreach (var item in sounds)
        {
            item.source.volume = 0;
        }
        this.isMute = true;
    }
    public void unMuteAllSound()
    {
        foreach (var item in sounds)
        {
            item.source.volume = 1;
        }
        this.isMute = false;
    }

    public void stopAllSound()
    {
        foreach (var item in sounds)
        {
            item.source.Stop();
        }
    }

    public void stopSound(string name)
    {
        CustomSound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
