using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CloseAlarm : MonoBehaviourPunCallbacks
{
    [SerializeField] private Animator alarm = null;
    [SerializeField] private GameObject viewPoint = null;
    private bool isPointerOnAlarm = false;
    private float holdTime;
    public GameObject ProgressBar;

    public GameObject taskManager;

    public bool isActive = true;
    private bool isExecuted = false;

    void Update()
    {
        if (!isExecuted && !isActive)
        {
            Debug.Log("CloseAlarm.cs :: MULTIPLAYER :: Disabling alarm...");
            
            OnPress();
            taskManager.GetComponent<TaskManager>().SetTaskCompleted(5);
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
                    Debug.Log("CloseAlarm.cs :: Disabling alarm...");

                    isActive = false;
                    photonView.RPC("OnMyVariableChanged", RpcTarget.All, isActive);

                    taskManager.GetComponent<TaskManager>().SetTaskCompleted(5);

                    taskManager.GetComponent<TaskManager>().ShowTaskCompletedMessage();

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
        Debug.Log("CloseAlarm.cs :: OnPress() called!");
        Destroy(viewPoint);
    }

}
