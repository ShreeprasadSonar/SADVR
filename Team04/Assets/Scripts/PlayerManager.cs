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
        Vector3 position = new Vector3(11, 11, -4);
        PhotonNetwork.Instantiate("PlayerController", position, Quaternion.identity);
        // PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity);
    }
}
