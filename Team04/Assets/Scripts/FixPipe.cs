using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FixPipe : MonoBehaviourPunCallbacks
{
    public GameObject Pipe;
    public GameObject Smoke;
    public GameObject ProgressBar;
    public GameObject taskManager;
    public AudioSource audioSource;

    public GameObject Wrench;
    
    private float distance;

    private float holdTime;
    private bool isPointerOnPipe = false;
    private bool isPositionCorrect = false;
    private bool isPipeFixed = false;

    public bool isActive = true;
    private bool isExecuted = false;

    void Update()
    {
        distance = Vector3.Distance(Pipe.transform.position, Wrench.transform.position);
        if (!isExecuted && !isActive)
        {
            Debug.Log("FixPipe.cs :: MULTIPLAYER :: Fixing pipe...");

            if (!isPositionCorrect) 
            {
                Vector3 position = Pipe.transform.position;
                position.y += 0.075f;
                position.z -= 0.03f;
                Pipe.transform.position = position;
                isPositionCorrect = true;
            }

            Smoke.SetActive(false);
            isPipeFixed = true;

            taskManager.GetComponent<TaskManager>().SetTaskCompleted(4);

            isExecuted = true;
        }

        if (distance < 3f)
        {

            if (isActive && (Input.GetKey(KeyCode.E) || Input.GetButton("js10")) && isPointerOnPipe) // Keyboard F, Android js2 (X)
            {
                if (!isPipeFixed) ProgressBar.SetActive(true);

                holdTime += Time.deltaTime;

                if (holdTime >= 3f)
                {
                    if (!isPositionCorrect) 
                    {
                        Vector3 position = Pipe.transform.position;
                        position.y += 0.075f;
                        position.z -= 0.03f;
                        Pipe.transform.position = position;
                        isPositionCorrect = true;
                    }

                    Debug.Log("FixPipe.cs :: Fixing pipe...");

                    isActive = false;
                    // Call the "OnMyVariableChanged" method over the Photon Network
                    photonView.RPC("OnMyVariableChanged", RpcTarget.All, isActive);

                    taskManager.GetComponent<TaskManager>().SetTaskCompleted(4);
                    taskManager.GetComponent<TaskManager>().ShowTaskCompletedMessage();

                    Smoke.SetActive(false);
                    isPipeFixed = true;
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
    }

    // This method is called over the Photon Network to update "myVariable"
    [PunRPC]
    public void OnMyVariableChanged(bool newValue)
    {
        isActive = newValue;
    }

    public void OnPointerEnter()
    {
        isPointerOnPipe = true;
    }

    public void OnPointerExit()
    {
        isPointerOnPipe = false;
    }

}
