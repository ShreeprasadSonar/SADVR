using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class FixWire : MonoBehaviourPunCallbacks
{
    public GameObject Wires;
    public GameObject ProgressBar;
    public GameObject taskCompletedMsgScriptObj;
    public AudioSource audioSource;

    public bool isActive = true;

    private float holdTime;
    private bool isPointerOnWire = false;
    private bool isExecuted = false;

    void Update()
    {   
        if (!isExecuted && !isActive)
        {
            Debug.Log("PutOutFire.cs :: Multiplayer :: Fixing wire...");
            
            Wires.SetActive(false);
            taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().SetTaskCompleted(1);
            isExecuted = true;
        }

        if (isActive && (Input.GetKey(KeyCode.E) || Input.GetButton("js10")) && isPointerOnWire) // Keyboard L, Android js2 (A)
        {
            ProgressBar.SetActive(true);
            holdTime += Time.deltaTime;

            if (holdTime >= 3f)
            {
                Debug.Log("FixWire.cs :: Fixing wire...");

                isActive = false;
                // Call the "OnMyVariableChanged" method over the Photon Network
                photonView.RPC("OnMyVariableChanged", RpcTarget.All, isActive);

                taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().SetTaskCompleted(1);

                taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().ShowTaskCompletedMessage();
                
                Wires.SetActive(false);
                ProgressBar.SetActive(false);
                audioSource.Play();
            }
        }
        else
        {
            ProgressBar.SetActive(false);
            holdTime = 0f;
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
        isPointerOnWire = true;
    }

    public void OnPointerExit()
    {
        isPointerOnWire = false;
    }

}
