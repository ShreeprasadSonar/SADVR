using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{

    public static RoomManager instance;

    [Space]
    public GameObject Camera;

    public GameObject connectingUI;

    public GameObject gameStartMenuCanvas;
    public GameObject inGameMenuCanvas;
    public GameObject playerSpeedManager;

    public GameObject startGameButton;
    public GameObject playerSpeedSettingsButton;
    public GameObject taskManager;

    private string nickname = "Astroboy";

    // void Awake() {
    //     instance = this;
    // }

    void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }


    public void ChangeNickname(string _name){
        nickname = _name;
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Number of rooms available: " + roomList.Count);
    }

    public void JoinRoomButtonPressed(){
        Debug.Log(message:"Connecting...");
        // Theres a master server and once we are connected to this we can join diff rooms
        PhotonNetwork.ConnectUsingSettings();

        connectingUI.SetActive(true);
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

        Debug.Log("Current room name: " + PhotonNetwork.CurrentRoom.Name);

        Debug.Log("Number of players in the room: " + PhotonNetwork.CurrentRoom.PlayerCount);

        Debug.Log("Number of rooms available: " + PhotonNetwork.CountOfRooms);

        Debug.Log(message:"Joined Room");

        // SpawnPlayer();
    }

    public void StartGameButtonHandler()
    {
        gameStartMenuCanvas.SetActive(false);
        JoinRoomButtonPressed();
    }

    public void GameSettingsButtonHandler()
    {
        gameStartMenuCanvas.SetActive(false);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    IEnumerator IntoMsgCoroutine()
    {
        yield return new WaitForSeconds(2f); // 2 seconds

        taskManager.GetComponent<TaskCompletionMsg>().TriggerIntroMsgCanvas();
    }
}
