using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class OpenDoor : MonoBehaviourPunCallbacks
{
    [SerializeField] private Animator door = null;
    private bool isPointerOnDoor = false;

    public bool doorOpened = false;

    void Update()
    {
        if (isPointerOnDoor)  // Keyboard E, Android js2 (A)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButton("js10"))
            {
                Debug.Log("OpenDoor.cs :: Opening door...");
                OnPress();
                doorOpened = true;
                photonView.RPC("OnMyVariableChanged", RpcTarget.All, doorOpened);
            }
        }
        else
        {
            if (doorOpened)
            {
                OnPress();
                doorOpened = false;
                photonView.RPC("OnMyVariableChanged", RpcTarget.All, doorOpened);
            }
        }
    }

    [PunRPC]
    public void OnMyVariableChanged(bool newValue)
    {
        doorOpened = newValue;
    }

    public void OnPointerEnter()
    {
        isPointerOnDoor = true;
    }

    public void OnPointerExit()
    {
        isPointerOnDoor = false;
    }
    
    public void OnPress()
    {
        door.Play("DoorOpen", 0, 0.0f);
        StartCoroutine(CloseDoorAfterDelay());
    }

    private IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        door.Play("DoorClose", 0, 0.0f);
    }

}