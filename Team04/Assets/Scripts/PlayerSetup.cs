using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public CharacterMovement movement;
    public GameObject cardboardRig;

    public void IsLocalPlayer(){
        movement.enabled = true;
        cardboardRig.SetActive(true);
    }
}
