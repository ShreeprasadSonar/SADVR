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
        Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
        PhotonNetwork.Instantiate("PlayerController", spawnpoint.position, spawnpoint.rotation, 0, new object[] { PV.ViewID });
        // PhotonNetwork.Instantiate("PlayerController", Vector3.zero, Quaternion.identity);
    }
}
