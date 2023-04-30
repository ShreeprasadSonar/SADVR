using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutOutFire : MonoBehaviour
{
    public GameObject Fire;
    public GameObject taskCompletedMsgScriptObj;
    public GameObject progressBar;

    private float holdTime;
    private bool isPointerOnFire=false;
    public AudioSource audiosource;

    void Update()
    {
        if ((Input.GetKey(KeyCode.E) || Input.GetButton("js10")) && isPointerOnFire) // Keyboard F, Android js2 (A)
        {
            progressBar.SetActive(true);
            holdTime += Time.deltaTime;

            if (holdTime >= 3f)
            {
                taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().SetTaskCompleted(2);
                taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().ShowTaskCompletedMessage();
                
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

    public void OnPointerEnter()
    {
        isPointerOnFire = true;
    }

    public void OnPointerExit()
    {
        isPointerOnFire = false;
    }
}
