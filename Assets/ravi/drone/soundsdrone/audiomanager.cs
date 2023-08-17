using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
public class audiomanager : MonoBehaviour
{
    public sound[] sounds;
    // Start is called before the first frame update
    void Awake()
        
    {
        foreach (sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play(string name)
    {
        sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Play();
    }
}
