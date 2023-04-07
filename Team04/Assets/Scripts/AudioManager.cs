using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] clips;
    public int volumeLevel = 1;

    private float[] volumeArray = {0.2f, 0.5f, 0.9f};

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlaySound(int clip){
        source.clip = clips[clip];
        source.Play();
    }

    public void SetVolumeLevel(int level){
        volumeLevel = level;
        source.volume = volumeArray[level];
    }

}
