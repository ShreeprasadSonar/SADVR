using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Timer : MonoBehaviourPunCallbacks
{
    public GameObject timerText;
    public GameObject timeUpCanvas;
    public GameObject taskManager;
    public GameObject timeUpQuitButton;
    public Button timerButton;

    public float timeLimit = 300f;
    public float blinkInterval = 1f;
    public Color onColor = Color.red;
    public Color offColor = Color.white;
    private bool isTimeLessThanAMinute = false;
    public bool isTimeUp = false;
    public bool isTimeUpForAllPlayers = false;
    private bool isExecuted = false;

    private float timeRemaining;

    private bool isTimerLimitSetupExecuted = false;
    public bool isFirstPlayer = false;
    public float timeRemainingShared = 0f;

    void Start()
    {
        timeRemaining = timeLimit;
    }

    void Update()
    {
        if (!isExecuted && isTimeUpForAllPlayers)
        {
            Debug.Log("Timer.cs :: MULTIPLAYER :: First player's time up; Ending Game...");

            timerText.GetComponent<TextMeshProUGUI>().SetText("00:00");
            timerButton.image.color = onColor;

            isTimeUp = true;
            taskManager.GetComponent<TaskManager>().SetMenuOptionInEventSystem(timeUpQuitButton);
            timeUpCanvas.SetActive(true);
            taskManager.GetComponent<TaskManager>().DisablePlayerMovement();

            isExecuted = true;
        }

        if (isExecuted) return;

        if (!isTimerLimitSetupExecuted)
        {
            Debug.Log("Timer.cs :: *******************");
            Debug.Log("Timer.cs :: isTimerLimitSetupExecuted: " + isTimerLimitSetupExecuted);
            Debug.Log("Timer.cs :: isFirstPlayer: " + isFirstPlayer);
            Debug.Log("Timer.cs :: timeRemaining: " + timeRemaining);

            if (!isFirstPlayer)
            {
                isFirstPlayer = true;
                timeRemainingShared = timeRemaining;

                photonView.RPC("OnVariablesChanged", RpcTarget.All, isTimeUpForAllPlayers, isFirstPlayer);
            }
            else
            {
                // for other players, get the time remaining from the first player
                timeRemaining = timeRemainingShared;
            }

            Debug.Log("Timer.cs :: timeRemainingShared: " + timeRemainingShared);
            Debug.Log("Timer.cs :: *******************");

            isTimerLimitSetupExecuted = true;
        }

        // decrement time remaining by 1 second
        timeRemaining -= Time.deltaTime;

        if (!isTimeUp && timeRemaining <= 0f)
        {
            Debug.Log("Timer.cs :: Time's up!");

            isTimeUp = true;

            isTimeUpForAllPlayers = true;
            // photonView.RPC("OnMyVariableChanged", RpcTarget.All, isTimeUpForAllPlayers);
            photonView.RPC("OnVariablesChanged", RpcTarget.All, isTimeUpForAllPlayers, isFirstPlayer);

            taskManager.GetComponent<TaskManager>().SetMenuOptionInEventSystem(timeUpQuitButton);
            timeUpCanvas.SetActive(true);
            taskManager.GetComponent<TaskManager>().DisablePlayerMovement();

            return;
        }

        if (isTimeUp) return;

        if (!isTimeLessThanAMinute && timeRemaining <= 60f)
        {
            Debug.Log("Timer.cs :: 1 minute remaining!");
            isTimeLessThanAMinute = true;
            StartCoroutine(BlinkTimer());
        }

        UpdateTimerDisplay();
    }

    public bool GetIsTimeUp()
    {
        return isTimeUp;
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.GetComponent<TextMeshProUGUI>().SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

    // [PunRPC]
    // public void OnMyVariableChanged(bool newValue)
    // {
    //     isTimeUpForAllPlayers = newValue;
    // }

    [PunRPC]
    public void OnVariablesChanged(bool _isTimeUp, bool _isFirstPlayer)
    {
        Debug.Log("*** Timer.cs :: OnVariablesChanged() :: OLD isFirstPlayer: " + isFirstPlayer);
        Debug.Log("*** Timer.cs :: OnVariablesChanged() :: _isFirstPlayer: " + _isFirstPlayer);

        isTimeUpForAllPlayers = _isTimeUp;
        isFirstPlayer = _isFirstPlayer;
    }

    private IEnumerator BlinkTimer()
    {
        while (!isTimeUp) 
        {
            timerButton.image.color = onColor;
            yield return new WaitForSeconds(blinkInterval);
            timerButton.image.color = offColor;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

}
