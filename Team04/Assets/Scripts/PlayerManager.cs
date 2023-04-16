using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(PV.IsMine){
            CreateController();
        }
    }

    void CreateController(){
        PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity);
    }
}
