using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixWire : MonoBehaviour
{
    public GameObject Wires;
    public GameObject taskCompletedMsgScriptObj;

    private float holdTime;
    private bool isPointerOnWire = false;

    void Start()
    {

    }

    void Update()
    {
        
        if ((Input.GetKey(KeyCode.E) || Input.GetButton("js10")) && isPointerOnWire) // Keyboard L, Android js10 (A)
        {
            
            holdTime += Time.deltaTime;

            if (holdTime >= 3f)
            {
                Debug.Log("FixWire :: Fixing wire...");

                taskCompletedMsgScriptObj.GetComponent<TaskCompletionMsg>().ShowTaskCompletedMessage();
                
                Wires.SetActive(false);
            }
        }
        else
        {
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
