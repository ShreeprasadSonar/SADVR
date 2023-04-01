using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{

    [SerializeField] private Animator door = null;

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;
    private bool isDoorOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                if (isDoorOpen)
                {
                    door.Play("DoorClose", 0, 0.0f);
                    isDoorOpen = false;
                    // gameObject.SetActive(false);
                }
                else
                {
                    door.Play("DoorOpen", 0, 0.0f);
                    isDoorOpen = true;
                }
                // gameObject.SetActive(false);
            }
            else if (closeTrigger)
            {
                if (!isDoorOpen)
                {
                    door.Play("DoorOpen", 0, 0.0f);
                    isDoorOpen = true;
                    // gameObject.SetActive(false);
                }
                else
                {
                    door.Play("DoorClose", 0, 0.0f);
                    isDoorOpen = false;
                }
                // gameObject.SetActive(false);
            }
        }
    }
}