using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PutOutFire : MonoBehaviourPunCallbacks
{
    public GameObject Fire;
    public GameObject taskManager;
    public GameObject progressBar;

    public GameObject FireExtinquisher;
    
    private float distance;

    private float holdTime;
    private bool isPointerOnFire = false;
    public AudioSource audiosource;

    public bool isActive = true;
    private bool isExecuted = false;

    void Update()
    {

        distance = Vector3.Distance(Fire.transform.position, FireExtinquisher.transform.position);

        if (distance < 3f)
        {   
            if (!isExecuted && !isActive)
            {
                Debug.Log("PutOutFire.cs :: MULTIPLAYER :: Putting out fire...");
                
                Fire.SetActive(false);
                taskManager.GetComponent<TaskManager>().SetTaskCompleted(2);
                isExecuted = true;
            }

            if (isActive && (Input.GetKey(KeyCode.E) || Input.GetButton("js10")) && isPointerOnFire) // Keyboard F, Android js2 (A)
            {
                progressBar.SetActive(true);
                holdTime += Time.deltaTime;

                if (holdTime >= 3f)
                {
                    Debug.Log("PutOutFire.cs :: Putting out fire...");

                    isActive = false;

                    // Call the "OnMyVariableChanged" method over the Photon Network
                    photonView.RPC("OnMyVariableChanged", RpcTarget.All, isActive);

                    taskManager.GetComponent<TaskManager>().SetTaskCompleted(2);

                    taskManager.GetComponent<TaskManager>().ShowTaskCompletedMessage();
                    
                    Fire.SetActive(false);
                    progressBar.SetActive(false);
                    audiosource.Play();
                }
            }
            else
            {
                progressBar.SetActive(false);
                holdTime = 0f;
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
        isPointerOnFire = true;
    }

    public void OnPointerExit()
    {
        isPointerOnFire = false;
    }

}
