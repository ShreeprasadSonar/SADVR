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
    public bool active = true;

    private float holdTime;
    private bool isPointerOnWire = false;

    void Start()
    {
    }

    void Update()
    {
        
        if ((Input.GetKey(KeyCode.E) || Input.GetButton("js2")) && isPointerOnWire) // Keyboard L, Android js2 (X)
        {

            ProgressBar.SetActive(true);
            holdTime += Time.deltaTime;

            if (holdTime >= 3f)
            {
                Debug.Log("FixWire :: Fixing wire...");

                taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().ShowTaskCompletedMessage();

                active = false;
                // Call the "OnMyVariableChanged" method over the Photon Network
                photonView.RPC("OnMyVariableChanged", RpcTarget.All, active);
                
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
        active = newValue;
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
