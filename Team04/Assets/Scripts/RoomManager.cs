using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    // public static RoomManager instance;

    public GameObject AudioManager;

    public GameObject player;
    [Space]
    public Transform[] spawnPoints;

    [Space]
    public GameObject roomCam;

    public GameObject connectingUI;

    public GameObject gameStartMenuCanvas;
    public GameObject inventoryMenuCanvas;
    public GameObject gameSettingsMenuCanvas;
    public GameObject inGameMenuCanvas;
    public GameObject playerSpeedManager;
    public GameObject gameMenuEventSystem;

    public GameObject startGameButton;
    public GameObject playerSpeedSettingsButton;
    public GameObject taskManager;

    private string nickname = "Astroboy";

    // void Awake() {
    //     instance = this;
    // }

    void Start()
    {
        inGameMenuCanvas.SetActive(false);
        gameSettingsMenuCanvas.SetActive(false);
        inventoryMenuCanvas.SetActive(false);
        gameStartMenuCanvas.SetActive(true);
        gameMenuEventSystem.SetActive(true);
        PhotonNetwork.AddCallbackTarget(this);

        // StartGameButtonHandler();
    }

    void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
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
        base.OnConnectedToMaster();

        Debug.Log(message:"Connected to Server");

        PhotonNetwork.JoinLobby();
    }


    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

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

        roomCam.SetActive(false);

        SpawnPlayer();
    }

    public void SpawnPlayer(){
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.Euler(0,180f,0)); // In Quaternion plater is making 180* turn, else use Quaternion.Identity
        
        _player.GetComponent<PlayerSetup>().IsLocalPlayer(); // will only be called on local player

        if(PhotonNetwork.CurrentRoom.PlayerCount > 1){
            _player.GetComponent<AudioListener>().enabled = false;
            _player.GetComponentInChildren<EventSystem>().GetComponent<EventSystem>().enabled = false;
        }

        // print("Numvber of players : " + PhotonNetwork.CurrentRoom.PlayerCount);
        

        _player.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, nickname + " " + PhotonNetwork.CurrentRoom.PlayerCount);
        AudioManager.GetComponent<AudioManager>().PlaySound(0);

        playerSpeedManager.GetComponent<PlayerSpeedManager>().SetInitialLoadPlayerSpeed();

        // taskManager.GetComponent<TaskCompletionMsg>().TriggerIntroMsgCanvas();
    
        StartCoroutine(IntoMsgCoroutine());
    }

    public void StartGameButtonHandler()
    {
        gameStartMenuCanvas.SetActive(false);
        gameMenuEventSystem.SetActive(false);
        JoinRoomButtonPressed();
    }

    public void GameSettingsButtonHandler()
    {
        gameStartMenuCanvas.SetActive(false);
        gameSettingsMenuCanvas.SetActive(true);
        gameMenuEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(playerSpeedSettingsButton);
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
