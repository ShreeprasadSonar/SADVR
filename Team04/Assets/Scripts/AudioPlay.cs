using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        audioSource.Play();
    }
}
