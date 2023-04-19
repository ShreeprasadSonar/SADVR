using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayTask1Audio : MonoBehaviour
{
    public AudioSource audioSource;
    private bool isAudioPlayed = false;

    void Update()
    {

    }

    public void OnPointerEnter()
    {
        if (!isAudioPlayed)
        {
            audioSource.Play();
        }
        isAudioPlayed = true;
    }

}
