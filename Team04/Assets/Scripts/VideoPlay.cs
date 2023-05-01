using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Photon.Pun;
using Photon.Realtime;

public class VideoPlay : MonoBehaviourPunCallbacks
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public GameObject taskCompletedMsgScriptObj;

    private bool isPointerOnMonitor = false;
    private bool isVideoPlaying = false;
    public bool isAudioPlaying = false;

    public bool isActive = true;
    private bool isExecuted = false;

    void Update()
    {
        if (!isExecuted && !isActive)
        {
            Debug.Log("VideoPlay.cs :: MULTIPLAYER :: Playing video on monitor...");

            if (!isVideoPlaying)
            {
                videoPlayer.Play();
                isVideoPlaying = true;
            }

            if (!isAudioPlaying)
            {
                audioSource.Play();
                isAudioPlaying = true;
            }

            taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().SetTaskCompleted(3);

            isExecuted = true;
        }

        if (isActive && (Input.GetKeyDown(KeyCode.E) || Input.GetButton("js10")) && isPointerOnMonitor) // Keyboard P, Android js2 (A)
        {
            if (!isVideoPlaying)
            {
                videoPlayer.Play();
                isVideoPlaying = true;

                if (!isAudioPlaying)
                {
                    Debug.Log("VideoPlay.cs :: Playing video on monitor...");

                    audioSource.Play();
                    isAudioPlaying = true;

                    isActive = false;
                    // Call the "OnMyVariableChanged" method over the Photon Network
                    photonView.RPC("OnMyVariableChanged", RpcTarget.All, isActive);

                    taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().SetTaskCompleted(3);

                    taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().ShowTaskCompletedMessage();
                }
            }
            else
            {
                videoPlayer.Stop();
                isVideoPlaying = false;
            }
        }
    }

    // This method is called over the Photon Network to update "myVariable"
    [PunRPC]
    public void OnMyVariableChanged(bool newValue)
    {
        isActive = newValue;
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
