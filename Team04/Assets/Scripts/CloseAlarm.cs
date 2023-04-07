using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAlarm : MonoBehaviour
{
    [SerializeField] private Animator alarm = null;
    [SerializeField] private GameObject viewPoint = null;
    private bool isPointerOnAlarm = false;
    private float holdTime;

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
        if (Input.GetKey(KeyCode.R) || Input.GetButton("js3"))
        {
            holdTime += Time.deltaTime;

            if (holdTime >= 3f)
            {
                if (isPointerOnAlarm)
                {
                    OnPress();
                }
            }
        }
        else
        {
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