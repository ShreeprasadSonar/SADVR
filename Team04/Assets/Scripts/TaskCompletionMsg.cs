using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskCompletionMsg : MonoBehaviour
{
    public GameObject taskCompletedMsgCanvas;

    private GameObject player = null;
    private GameObject playerMainCamera = null;

    void Start()
    {
        taskCompletedMsgCanvas.SetActive(false);
    }

    void Update()
    {
        
    }

    private void SetPlayerGameObjects()
    {
        player = GameObject.FindWithTag("Player");

        // Debug.Log("TaskCompletionMsg :: player: " + player);

        if (player != null) 
        {
            playerMainCamera = player.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;

            taskCompletedMsgCanvas.GetComponent<Canvas>().worldCamera = playerMainCamera.GetComponent<Camera>();
            
            taskCompletedMsgCanvas.GetComponent<Canvas>().planeDistance = 1;
        }
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

}
