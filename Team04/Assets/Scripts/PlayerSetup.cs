using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public CharacterMovement movement;
    public GameObject camera;
    public string nickname;
    public TextMeshPro nicknameText;

    void Start()
    {
        if (photonView.IsMine){
            nicknameText.text = "Astroboy " + PhotonNetwork.CurrentRoom.PlayerCount;
        }
    }

    public void IsLocalPlayer(){
        movement.enabled = true;
        camera.SetActive(true);
    }
}
