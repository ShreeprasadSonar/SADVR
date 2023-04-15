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

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.R))
    //     {
    //         // print(isPointerOnAlarm);
    //         if (isPointerOnAlarm)
    //         {
    //             OnPress();
    //         }
    //     }
    // }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) || Input.GetButton("js2")) // Keyboard R, Android j2 (X)
        {
            ProgressBar.SetActive(true);
            holdTime += Time.deltaTime;

            if (holdTime >= 3f)
            {
                if (isPointerOnAlarm)
                {
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
        print("inside onpress alarm");
        Destroy(viewPoint);
    }

}
