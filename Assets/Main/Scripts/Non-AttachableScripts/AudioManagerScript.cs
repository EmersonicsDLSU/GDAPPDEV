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
        s.source.Play();
    }

    public void muteAllSound()
    {
        foreach (var item in sounds)
        {
            item.source.volume = 0;
        }
    }
    public void unMuteAllSound()
    {
        foreach (var item in sounds)
        {
            item.source.volume = 1;
        }
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
