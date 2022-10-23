using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //AudioManager.instance.Play("Ääni tähän");

    public static AudioManager instance;
    public Sound[] sounds;

    void Awake()
    {
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.panStereo = s.stereo;
            s.source.loop = s.loop;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //Play("Musiikki");
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning("Sound: " + name + (" not found!"));
            return;
        }
        s.source.Play();
    }
}