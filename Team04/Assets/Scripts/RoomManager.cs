using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    public GameObject AudioManager;

    public GameObject player;
    [Space]
    public Transform spawnPoint;

    [Space]
    public GameObject roomCam;

    [Space]
    public GameObject nameUI;

    public GameObject connectingUI;

    public GameObject gameStartMenuCanvas;
    public GameObject gameSettingsMenuCanvas;
    public GameObject inGameMenuCanvas;
    public GameObject playerSpeedManager;

    private string nickname = "unnamed";

    void Awake() {
        instance = this;
    }

    void Start()
    {
        inGameMenuCanvas.SetActive(false);
        gameSettingsMenuCanvas.SetActive(false);
        gameStartMenuCanvas.SetActive(true);
    }

    public void StartGameButtonHandler()
    {
        Debug.Log("Start Game Button Pressed");
        gameStartMenuCanvas.SetActive(false);
        JoinRoomButtonPressed();
    }

    public void GameSettingsButtonHandler()
    {
        Debug.Log("Game Settings Button Pressed");
        gameStartMenuCanvas.SetActive(false);
        gameSettingsMenuCanvas.SetActive(true);
    }

    public void QuitGame() 
    {
        Debug.Log("QuitGame() called");
        Application.Quit();
    }

    public void ChangeNickname(string _name){
        nickname = _name;
    }

    public void JoinRoomButtonPressed(){
        Debug.Log(message:"Connecting...");

        // Theres a master server and once we are connected to this we can join diff rooms
        PhotonNetwork.ConnectUsingSettings();

        nameUI.SetActive(false);
        connectingUI.SetActive(true);
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

        roomCam.SetActive(false);

        SpawnPlayer();
    }

    public void SpawnPlayer(){
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.Euler(0,180f,0)); // In Quaternion plater is making 180* turn, else use Quaternion.Identity
        _player.GetComponent<PlayerSetup>().IsLocalPlayer(); // will only be called on local player

        _player.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, nickname);
        AudioManager.GetComponent<AudioManager>().PlaySound(0);

        playerSpeedManager.GetComponent<PlayerSpeedManager>().SetInitialLoadPlayerSpeed();
    }
}
