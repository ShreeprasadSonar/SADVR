using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Animator door = null;
    private bool isPointerOnDoor = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            print(isPointerOnDoor);
            if (isPointerOnDoor)
            {
                OnPress();
            }
        }
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