using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixWire : MonoBehaviour
{
    public GameObject Wires;
    public GameObject ProgressBar;
    public GameObject taskCompletedMsgScriptObj;
    public AudioSource audioSource;

    private float holdTime;
    private bool isPointerOnWire = false;

    void Start()
    {
        // TODO:
    }

    void Update()
    {   
        if ((Input.GetKey(KeyCode.E) || Input.GetButton("js10")) && isPointerOnWire) // Keyboard L, Android js2 (A)
        {
            ProgressBar.SetActive(true);
            holdTime += Time.deltaTime;

            if (holdTime >= 3f)
            {
                Debug.Log("FixWire :: Fixing wire...");

                taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().SetTaskCompleted(1);

                taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().ShowTaskCompletedMessage();
                
                Wires.SetActive(false);
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

    public void OnPointerEnter()
    {
        isPointerOnWire = true;
    }

    public void OnPointerExit()
    {
        isPointerOnWire = false;
    }

}
