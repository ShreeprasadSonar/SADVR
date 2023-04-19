using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlay : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private bool isPointerOnMonitor = false;
    private bool isVideoPlaying = false;
    public AudioSource audioSource;
    public bool isAudioPlaying = false;

    void Update()
    {
        // RaycastHit hit;
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // if (Physics.Raycast(ray, out hit))
        // {
        //     Debug.Log("Object hit: " + hit.collider.gameObject.name);
        // }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetButton("js2")) // Keyboard P, Android js2 (X)
        {
            print(isPointerOnMonitor);
            if (isPointerOnMonitor)
            {
                if (!isVideoPlaying)
                {
                    videoPlayer.Play();
                    isVideoPlaying = true;
                    if (!isAudioPlaying)
                    {
                        audioSource.Play();
                        isAudioPlaying = true;
                    }
                }
                else
                {
                    videoPlayer.Stop();
                    isVideoPlaying = false;
                }
            }
        }
    }

    public void OnPointerEnter()
    {
        isPointerOnMonitor = true;
    }

    public void OnPointerExit()
    {
        isPointerOnMonitor = false;
    }
    
}
