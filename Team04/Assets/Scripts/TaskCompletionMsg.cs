using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskCompletionMsg : MonoBehaviour
{
    public GameObject taskCompletedMsgCanvas;
    public GameObject introMsgCanvas;

    private GameObject player = null;
    private GameObject playerXRCardboardRig = null;
    private GameObject playerEventSystem = null;
    private GameObject playerMainCamera = null;
    private GameObject playerReticleMesh1 = null;
    private GameObject playerReticleMesh2 = null;
    
    public GameObject introOkButton;

    void Start()
    {
        taskCompletedMsgCanvas.SetActive(false);
        introMsgCanvas.SetActive(false);
    }

    void Update()
    {
        
    }

    private void SetPlayerGameObjects()
    {
        Debug.Log("TaskCompletionMsg :: SetPlayerGameObjects() called");

        player = GameObject.FindWithTag("Player");

        // Debug.Log("TaskCompletionMsg :: player: " + player);

        if (player != null) 
        {
            playerMainCamera = player.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;

            taskCompletedMsgCanvas.GetComponent<Canvas>().worldCamera = playerMainCamera.GetComponent<Camera>();
            
            taskCompletedMsgCanvas.GetComponent<Canvas>().planeDistance = 1;
        }
    }

    public void TriggerIntroMsgCanvas()
    {
        Debug.Log("TaskCompletionMsg :: TriggerIntroMsgCanvas() called");

        player = GameObject.FindWithTag("Player");

        // Debug.Log("TaskCompletionMsg :: player: " + player);

        if (player != null) 
        {
            playerXRCardboardRig = player.transform.GetChild(0).gameObject;
            playerEventSystem = playerXRCardboardRig.transform.GetChild(1).gameObject;
            playerMainCamera = playerXRCardboardRig.transform.GetChild(0).GetChild(0).gameObject;
            playerReticleMesh1 = playerMainCamera.transform.GetChild(1).GetChild(0).gameObject;
            playerReticleMesh2 = playerMainCamera.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;

            introMsgCanvas.GetComponent<Canvas>().worldCamera = playerMainCamera.GetComponent<Camera>();
            
            introMsgCanvas.GetComponent<Canvas>().planeDistance = 1;

            playerEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(introOkButton);  

            // DisablePlayerMovement();
            StartCoroutine(DisablePlayerMovementCoroutine());

            introMsgCanvas.SetActive(true);
        }
    }

    public void CloseIntroMsgCanvas()
    {
        Debug.Log("TaskCompletionMsg :: CloseIntroMsgCanvas() called");

        EnablePlayerMovement();

        introMsgCanvas.SetActive(false);
    }

    private void DisablePlayerMovement()
    {
        player.GetComponent<CharacterMovement>().enabled = false;

        playerXRCardboardRig.GetComponent<XRCardboardController>().enabled = false;
        playerMainCamera.GetComponent<PhysicsRaycaster>().enabled = false;
        playerReticleMesh2.SetActive(false);
        playerReticleMesh1.SetActive(false);

        playerEventSystem.GetComponent<StandaloneInputModule>().enabled = true;
        if (playerEventSystem.GetComponent<XRCardboardInputModule>().enabled != false){
            playerEventSystem.GetComponent<XRCardboardInputModule>().enabled = false;
        } 
    }

    private void EnablePlayerMovement()
    {
        player.GetComponent<CharacterMovement>().enabled = true;

        playerXRCardboardRig.GetComponent<XRCardboardController>().enabled = true;
        playerMainCamera.GetComponent<PhysicsRaycaster>().enabled = true;
        // playerReticleMesh1.GetComponent<MeshRenderer>().enabled = true;
        // playerReticleMesh2.GetComponent<MeshRenderer>().enabled = true;
        playerReticleMesh2.SetActive(true);
        playerReticleMesh1.SetActive(true);

        playerEventSystem.GetComponent<StandaloneInputModule>().enabled = false;
        playerEventSystem.GetComponent<XRCardboardInputModule>().enabled = true;

        playerEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
    }

    public void ShowTaskCompletedMessage()
    {
        Debug.Log("TaskCompletionMsg :: ShowTaskCompletedMessage() called");

        StartCoroutine(TaskCompletedMessageCoroutine());
    }

    IEnumerator TaskCompletedMessageCoroutine()
    {
        taskCompletedMsgCanvas.SetActive(true);

        yield return new WaitForSeconds(2f); // 2 seconds

        taskCompletedMsgCanvas.SetActive(false);
    }

    IEnumerator DisablePlayerMovementCoroutine()
    {
        Debug.Log("TaskCompletionMsg :: DisablePlayerMovementCoroutine() called");

        yield return new WaitForSeconds(4f); // 4 seconds

        player.GetComponent<CharacterMovement>().enabled = false;

        playerXRCardboardRig.GetComponent<XRCardboardController>().enabled = false;
        playerMainCamera.GetComponent<PhysicsRaycaster>().enabled = false;
        playerReticleMesh2.SetActive(false);
        playerReticleMesh1.SetActive(false);

        playerEventSystem.GetComponent<StandaloneInputModule>().enabled = true;
        if (playerEventSystem.GetComponent<XRCardboardInputModule>().enabled != false){
            playerEventSystem.GetComponent<XRCardboardInputModule>().enabled = false;
        } 
    }

}
