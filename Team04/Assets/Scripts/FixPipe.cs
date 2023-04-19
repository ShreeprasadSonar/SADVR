using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPipe : MonoBehaviour
{
    public GameObject Pipe;
    public GameObject Smoke;
    public GameObject ProgressBar;
    public GameObject taskCompletedMsgScriptObj;
    public AudioSource audioSource;

    private float holdTime;
    private bool isPointerOnPipe=false;

    void Update()
    {
        if ((Input.GetKey(KeyCode.E) || Input.GetButton("js2")) && isPointerOnPipe) // Keyboard F, Android js2 (X)
        {
            ProgressBar.SetActive(true);
            holdTime += Time.deltaTime;

            if (holdTime >= 3f)
            {
                Vector3 position = Pipe.transform.position;
                position.y += 0.0045f;
                position.z -= 0.002f;
                Pipe.transform.position = position;

                taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().ShowTaskCompletedMessage();

                Smoke.SetActive(false);
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
        isPointerOnPipe = true;
    }

    public void OnPointerExit()
    {
        isPointerOnPipe = false;
    }
}
