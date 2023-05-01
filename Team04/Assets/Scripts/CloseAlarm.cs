using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAlarm : MonoBehaviour
{
    [SerializeField] private Animator alarm = null;
    [SerializeField] private GameObject viewPoint = null;
    private bool isPointerOnAlarm = false;
    private float holdTime;
    public GameObject ProgressBar;

    public GameObject taskCompletedMsgScriptObj;

    public bool isActive = true;
    private bool isExecuted = false;

    void Update()
    {
        if (!isExecuted && !isActive)
        {
            Debug.Log("PutOutFire.cs :: Multiplayer :: Disabling alarm...");
            
            OnPress();
            taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().SetTaskCompleted(5);
            isExecuted = true;
        }
        
        if (isActive && (Input.GetKey(KeyCode.E) || Input.GetButton("js10")) && isPointerOnAlarm) // Keyboard R, Android j2 (A)
        {
            ProgressBar.SetActive(true);
            holdTime += Time.deltaTime;

            if (holdTime >= 3f)
            {
                if (isPointerOnAlarm)
                {
                    Debug.Log("PutOutFire.cs :: Disabling alarm...");

                    isActive = false;
                    // Call the "OnMyVariableChanged" method over the Photon Network
                    photonView.RPC("OnMyVariableChanged", RpcTarget.All, isActive);

                    taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().SetTaskCompleted(5);

                    taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().ShowTaskCompletedMessage();

                    OnPress();
                    ProgressBar.SetActive(false);
                }
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
        isPointerOnAlarm = true;
    }

    public void OnPointerExit()
    {
        isPointerOnAlarm = false;
    }
    
    public void OnPress()
    {   
        Debug.Log("CloseAram.cs :: OnPress() called!");
        Destroy(viewPoint);
    }

}
