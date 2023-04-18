using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SSRoomManager : MonoBehaviourPunCallbacks
{
    public static SSRoomManager Instance;

    void Awake(){
        if(Instance){ // Checks if another roommanager exists in the scene
            Destroy(gameObject); // Destroyes itself if there is
            return;
        }
        DontDestroyOnLoad(gameObject); // Makes it the only one
        Instance = this;
    }

    public override void OnEnable() {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable() {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode){
        if(scene.buildIndex == 1){
            PhotonNetwork.Instantiate("PlayerManager", Vector3.zero, Quaternion.identity);
        }
    }
}
