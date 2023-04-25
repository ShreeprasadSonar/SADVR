using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InstantiateObjects : MonoBehaviourPunCallbacks
{
    public GameObject plyer;
    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the GameObject you want to instantiate
        // GameObject mySceneItem = GameObject.Find("MySceneItem");

        // Instantiate the item across the network
        PhotonNetwork.InstantiateSceneObject(plyer.name, plyer.transform.position, plyer.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
