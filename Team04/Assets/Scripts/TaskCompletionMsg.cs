using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskCompletionMsg : MonoBehaviour
{
    public GameObject taskCompletedMsgCanvas;
    public GameObject taskManagerCanvas;
    public GameObject introMsgCanvas;
    public GameObject introOkButton;
    public GameObject inGameMenuCanvas;
    public GameObject timerCanvas;
    public GameObject timeUpCanvas;
    public GameObject allTasksCompletedCanvas;
    public GameObject allTasksCompletedQuitButton;

    public const int numberOfTasks = 5;
    public GameObject[] taskCheckboxButtonsRed = new GameObject[numberOfTasks];
    public GameObject[] taskCheckboxButtonsGreen = new GameObject[numberOfTasks];

    private GameObject player = null;
    private GameObject playerXRCardboardRig = null;
    private GameObject playerEventSystem = null;
    private GameObject playerMainCamera = null;
    private GameObject playerReticleMesh1 = null;
    private GameObject playerReticleMesh2 = null;

    private bool task1Completion = false;
    private bool task2Completion = false;
    private bool task3Completion = false;
    private bool task4Completion = false;
    private bool task5Completion = false;

    void Start()
    {
        taskCompletedMsgCanvas.SetActive(false);
        taskManagerCanvas.SetActive(false);
        introMsgCanvas.SetActive(false);
        timeUpCanvas.SetActive(false);
        allTasksCompletedCanvas.SetActive(false);
        timerCanvas.SetActive(true);

        for (int i = 0; i < numberOfTasks; i++)
        {
            taskCheckboxButtonsGreen[i].SetActive(false);
            taskCheckboxButtonsRed[i].SetActive(true);
        }
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");

            playerXRCardboardRig = player.transform.GetChild(0).gameObject;
            playerEventSystem = playerXRCardboardRig.transform.GetChild(1).gameObject;
            playerMainCamera = playerXRCardboardRig.transform.GetChild(0).GetChild(0).gameObject;
            playerReticleMesh1 = playerMainCamera.transform.GetChild(1).GetChild(0).gameObject;
            playerReticleMesh2 = playerMainCamera.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;

            Debug.Log("*******");
            Debug.Log("player: " + player);
            Debug.Log("playerXRCardboardRig: " + playerXRCardboardRig);
            Debug.Log("playerEventSystem: " + playerEventSystem);
            Debug.Log("playerMainCamera: " + playerMainCamera);
            Debug.Log("playerReticleMesh1: " + playerReticleMesh1);
            Debug.Log("playerReticleMesh2: " + playerReticleMesh2);
            Debug.Log("*******");
        }

        if (player != null)
        {
            taskManagerCanvas.GetComponent<Canvas>().worldCamera = playerMainCamera.GetComponent<Camera>();
            taskManagerCanvas.GetComponent<Canvas>().planeDistance = 1;

            timerCanvas.GetComponent<Canvas>().worldCamera = playerMainCamera.GetComponent<Camera>();
            timerCanvas.GetComponent<Canvas>().planeDistance = 1;

            timeUpCanvas.GetComponent<Canvas>().worldCamera = playerMainCamera.GetComponent<Camera>();
            timeUpCanvas.GetComponent<Canvas>().planeDistance = 1;

            allTasksCompletedCanvas.GetComponent<Canvas>().worldCamera = playerMainCamera.GetComponent<Camera>();
            allTasksCompletedCanvas.GetComponent<Canvas>().planeDistance = 1;
        }

        if (player != null && Input.GetKeyDown(KeyCode.N)) 
        {
            Debug.Log("TaskCompletionMsg :: 'N' key pressed!");
            EnableTaskManagerMenu();
        }

        if (taskManagerCanvas.activeSelf && (Input.GetKeyDown(KeyCode.V))) 
        {
            DisableTaskManagerMenu();
        }

        CheckAllTasksCompleted();
    }

    private void CheckAllTasksCompleted()
    {
        Debug.Log("TaskCompletionMsg :: CheckAllTasksCompleted() called");

        if (task1Completion && task2Completion && task3Completion && task4Completion && task5Completion)
        {
            Debug.Log("TaskCompletionMsg :: All tasks completed successfully!");
            
            SetMenuOptionInEventSystem(allTasksCompletedQuitButton);
            StartCoroutine(AllTasksCompletedCanvasCoroutine());
            DisablePlayerMovement();
        }
    }

    public void SetTaskCompleted(int taskNum)
    {
        Debug.Log("TaskCompletionMsg :: SetTaskCompleted() called");

        switch (taskNum)
        {
            case 1:
                task1Completion = true;
                break;
            case 2:
                task2Completion = true;
                break;
            case 3:
                task3Completion = true;
                break;
            case 4:
                task4Completion = true;
                break;
            case 5:
                task5Completion = true;
                break;
            default:
                return;
        }
    }

    public void EnableTaskManagerMenu()
    {
        Debug.Log("TaskCompletionMsg :: EnableTaskManagerMenu() called");

        if (inGameMenuCanvas.activeSelf) inGameMenuCanvas.SetActive(false);

        RefreshTaskManagerMenu();
        taskManagerCanvas.SetActive(true);
    }

    public void DisableTaskManagerMenu()
    {
        Debug.Log("TaskCompletionMsg :: DisableTaskManagerMenu() called");

        taskManagerCanvas.SetActive(false);
    }

    private void RefreshTaskManagerMenu()
    {
        Debug.Log("TaskCompletionMsg :: RefreshTaskManagerMenu() called");

        if (task1Completion) 
        {
            taskCheckboxButtonsGreen[0].SetActive(true);
            taskCheckboxButtonsRed[0].SetActive(false);
        }

        if (task2Completion) 
        {
            taskCheckboxButtonsGreen[1].SetActive(true);
            taskCheckboxButtonsRed[1].SetActive(false);
        }

        if (task3Completion) 
        {
            taskCheckboxButtonsGreen[2].SetActive(true);
            taskCheckboxButtonsRed[2].SetActive(false);
        }

        if (task4Completion) 
        {
            taskCheckboxButtonsGreen[3].SetActive(true);
            taskCheckboxButtonsRed[3].SetActive(false);
        }

         if (task5Completion) 
        {
            taskCheckboxButtonsGreen[4].SetActive(true);
            taskCheckboxButtonsRed[4].SetActive(false);
        }
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

    public void SetMenuOptionInEventSystem(GameObject obj)
    {
        if (playerEventSystem) playerEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(obj);  
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

    public void TimeUpQuitButtonHandler()
    {
        Debug.Log("TaskCompletionMsg :: TimeUpQuitButtonHandler() called");
        Application.Quit();
    }

    public void TasksCompletedQuitButtonHandler()
    {
        Debug.Log("TaskCompletionMsg :: TasksCompletedQuitButtonHandler() called");
        Application.Quit();
    }

    public void DisablePlayerMovement()
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

    public void EnablePlayerMovement()
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

    IEnumerator AllTasksCompletedCanvasCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        timerCanvas.SetActive(false);
        allTasksCompletedCanvas.SetActive(true);
    }

}
