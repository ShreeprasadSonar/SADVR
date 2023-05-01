using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Timer : MonoBehaviour
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

    private float timeRemaining;

    void Start()
    {
        timeRemaining = timeLimit;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (!isTimeUp && timeRemaining <= 0f)
        {
            Debug.Log("Timer.cs :: Time's up!");

            isTimeUp = true;
            taskManager.GetComponent<TaskManager>().SetMenuOptionInEventSystem(timeUpQuitButton);
            timeUpCanvas.SetActive(true);
            taskManager.GetComponent<TaskManager>().DisablePlayerMovement();

            return;
        }

        if (!isTimeLessThanAMinute && timeRemaining <= 60f)
        {
            Debug.Log("Timer.cs :: 1 minute remaining!");
            isTimeLessThanAMinute = true;
            StartCoroutine(BlinkTimer());
        }

        if (isTimeUp) return;

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
