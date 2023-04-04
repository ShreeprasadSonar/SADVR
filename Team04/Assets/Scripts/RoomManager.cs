using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject player;
    [Space]
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(message:"Connecting...");

        // Theres a master server and once we are connected to this we can join diff rooms
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log(message:"Connected to Server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom(roomName: "test", roomOptions: null, typedLobby: null);
        
        Debug.Log(message:"Joined Lobby");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log(message:"Joined Room");
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.Euler(0,180f,0)); // In Quaternion plater is making 180* turn, else use Quaternion.Identity
        _player.GetComponent<PlayerSetup>().IsLocalPlayer(); // will only be called on local player

    }
}
