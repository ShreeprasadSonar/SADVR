using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPipe : MonoBehaviour
{
    public GameObject Pipe;
    public GameObject Smoke;
    private float holdTime;
    private bool isPointerOnPipe=false;

    void Update()
    {
        
        if ((Input.GetKey(KeyCode.F) || Input.GetButton("js10")) && isPointerOnPipe) // Keyboard F, Android js10 (A)
        {
            
            holdTime += Time.deltaTime;

            if (holdTime >= 3f)
            {
                Vector3 position = Pipe.transform.position;
                position.y += 0.0045f;
                position.z -= 0.002f;
                Pipe.transform.position = position;
                Smoke.SetActive(false);
            }
        }
        else
        {
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
