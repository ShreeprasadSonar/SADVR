using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{

    public static RoomManager instance;

    [Space]
    public GameObject Camera;

    public GameObject connectingUI;
    public GameObject waitUI;

    public GameObject gameStartMenuCanvas;
    public GameObject inGameMenuCanvas;
    public GameObject playerSpeedManager;

    public GameObject startGameButton;
    public GameObject playerSpeedSettingsButton;
    public GameObject taskManager;

    public int noOfPlayers;
    private bool joinedRoom = false;
    private bool startGame = false;

    private string nickname = "Astroboy";

    // void Awake() {
    //     instance = this;
    // }

    void Start()
    {
        connectingUI.SetActive(true);
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        if (startGame && joinedRoom && PhotonNetwork.CurrentRoom.PlayerCount != noOfPlayers)
        {
            waitUI.SetActive(true);
            gameStartMenuCanvas.SetActive(false);
        }
        else if(startGame && joinedRoom && PhotonNetwork.CurrentRoom.PlayerCount == noOfPlayers){
            PhotonNetwork.LoadLevel(1);
        }
    }

    public void StartGame()
    {
        Debug.Log("Starting Game");
        startGame = true;
    }

    public void ChangeNickname(string _name){
        nickname = _name;
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Number of rooms available: " + roomList.Count);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(message:"Connected to Master");

        PhotonNetwork.JoinLobby();
    }


    public override void OnJoinedLobby()
    {
        Debug.Log(message:"Joined Lobby");

        PhotonNetwork.JoinOrCreateRoom(roomName: "Spaceship", roomOptions: null, typedLobby: null);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        Debug.LogError("Failed to join room: " + message);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        joinedRoom = true;

        Debug.Log("Current room name: " + PhotonNetwork.CurrentRoom.Name);

        Debug.Log("Number of players in the room: " + PhotonNetwork.CurrentRoom.PlayerCount);

        Debug.Log(message:"Joined Room");

        connectingUI.SetActive(false);

        gameStartMenuCanvas.SetActive(true);

        // SpawnPlayer();
    }

    public void StartGameButtonHandler()
    {
        gameStartMenuCanvas.SetActive(false);
    }

    public void GameSettingsButtonHandler()
    {
        gameStartMenuCanvas.SetActive(false);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

}
