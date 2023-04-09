using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InGameMenu : MonoBehaviour
{
    public GameObject inGameMenu;
    public GameObject gameStartMenu;
    
    public GameObject playerSpeedManager;
    public AudioManager audioManager;
    public GameObject gameMenuEventSystem;
    
    private GameObject player;
    private GameObject playerXRCardboardRig = null;
    private GameObject playerEventSystem = null;
    private GameObject playerMainCamera = null;
    private GameObject playerReticleMesh1 = null;
    private GameObject playerReticleMesh2 = null;
    
    public GameObject resumeButton;
    public GameObject speedButton;
    public GameObject audioLevelButton;
    // public GameObject inventoryMenu;
    
    public Sprite highSpeedButtonImageSprite;
    public Sprite mediumSpeedButtonImageSprite;
    public Sprite lowSpeedButtonImageSprite;

    public Sprite softAudioLevelButtonImageSprite;
    public Sprite defaultAudioLevelButtonImageSprite;
    public Sprite loudAudioLevelButtonImageSprite;

    void Start()
    {
        // player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (!gameStartMenu.activeSelf && Input.GetButtonDown("js7")) { // 'H' key

          Debug.Log("InGameMenu.cs :: 'H' key pressed!");

          // if (inventoryMenu.activeSelf) {
          //   inventoryMenu.SetActive(false);
          // }

          player = GameObject.FindWithTag("Player");

          playerXRCardboardRig = player.transform.GetChild(0).gameObject;
          playerEventSystem = playerXRCardboardRig.transform.GetChild(1).gameObject;
          playerMainCamera = playerXRCardboardRig.transform.GetChild(0).GetChild(0).gameObject;
          playerReticleMesh1 = playerMainCamera.transform.GetChild(1).GetChild(0).gameObject;
          playerReticleMesh2 = playerMainCamera.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;

          // XRCardboardRig = player.transform.Find("XRCardboardRig").gameObject;
          // EventSystem = player.transform.Find("EventSystem").gameObject;
          // mainCamera = player.transform.Find("Main Camera").gameObject;
          // Reticle = player.transform.Find("Reticle").gameObject;

          Debug.Log("*******");
          Debug.Log("player: " + player);
          Debug.Log("playerXRCardboardRig: " + playerXRCardboardRig);
          Debug.Log("playerEventSystem: " + playerEventSystem);
          Debug.Log("playerMainCamera: " + playerMainCamera);
          Debug.Log("playerReticleMesh1: " + playerReticleMesh1);
          Debug.Log("playerReticleMesh2: " + playerReticleMesh2);
          Debug.Log("*******");

          OpenInGameMenu();
        }
    }

    public void OpenInGameMenu()
    {
        Debug.Log("OpenInGameMenu() called");

        inGameMenu.SetActive(true);

        player.GetComponent<CharacterMovement>().enabled = false;

        playerXRCardboardRig.GetComponent<XRCardboardController>().enabled = false;
        playerMainCamera.GetComponent<PhysicsRaycaster>().enabled = false;
        playerReticleMesh1.GetComponent<MeshRenderer>().enabled = false;
        playerReticleMesh2.GetComponent<MeshRenderer>().enabled = false;

        playerEventSystem.GetComponent<StandaloneInputModule>().enabled = true;
        if (playerEventSystem.GetComponent<XRCardboardInputModule>().enabled != false){
            playerEventSystem.GetComponent<XRCardboardInputModule>().enabled = false;
        }

        playerEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(resumeButton);
    }

    public void CloseInGameMenu()
    {
        Debug.Log("CloseInGameMenu() called");

        inGameMenu.SetActive(false);

        player.GetComponent<CharacterMovement>().enabled = true;

        playerXRCardboardRig.GetComponent<XRCardboardController>().enabled = true;
        playerMainCamera.GetComponent<PhysicsRaycaster>().enabled = true;
        playerReticleMesh1.GetComponent<MeshRenderer>().enabled = true;
        playerReticleMesh2.GetComponent<MeshRenderer>().enabled = true;

        playerEventSystem.GetComponent<StandaloneInputModule>().enabled = false;
        playerEventSystem.GetComponent<XRCardboardInputModule>().enabled = true;

        playerEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
    }

    public void ResumeGame()
    {
        Debug.Log("ResumeGame() called");
        CloseInGameMenu();
    }

    public void PlayerSpeed() 
    {
        Debug.Log("PlayerSpeed() called");

        int currentSpeedLevel = playerSpeedManager.GetComponent<PlayerSpeedManager>().playerSpeed;

        int newSpeedLevel = (currentSpeedLevel + 1) % 3;

        playerSpeedManager.GetComponent<PlayerSpeedManager>().SetPlayerSpeedLevel(newSpeedLevel);

        Debug.Log("currentSpeedLevel: " + currentSpeedLevel);
        Debug.Log("newSpeedLevel: " + newSpeedLevel);

        if (newSpeedLevel == 0) {
            speedButton.GetComponent<Image>().sprite = lowSpeedButtonImageSprite; // low
            player.GetComponent<CharacterMovement>().speed = 5;
        } else if (newSpeedLevel == 1) {
            speedButton.GetComponent<Image>().sprite = mediumSpeedButtonImageSprite; // medium
            player.GetComponent<CharacterMovement>().speed = 10;
        } else if (newSpeedLevel == 2) {
            speedButton.GetComponent<Image>().sprite = highSpeedButtonImageSprite; // high
            player.GetComponent<CharacterMovement>().speed = 20;
        }
    }

    public void AudioCuesVolumeControl() 
    {
        Debug.Log("AudioCuesVolumeControl() called");

        int newVolumeLevel = (audioManager.volumeLevel + 1) % 3;
        audioManager.SetVolumeLevel(newVolumeLevel);

        if (newVolumeLevel == 0) {
            audioLevelButton.GetComponent<Image>().sprite = softAudioLevelButtonImageSprite; // soft
        } else if (newVolumeLevel == 1) {
            audioLevelButton.GetComponent<Image>().sprite = defaultAudioLevelButtonImageSprite; // default (medium)
        } else if (newVolumeLevel == 2) {
            audioLevelButton.GetComponent<Image>().sprite = loudAudioLevelButtonImageSprite; // loud
        }
    }

    // public void Inventory() 
    // {
    //   Debug.Log("inGameMenu :: Inventory() called");

    //   inGameMenu.SetActive(false);
    //   inventoryMenu.SetActive(true);
    // }

    public void QuitGame() 
    {
        Debug.Log("inGameMenu :: QuitGame() called");
        Application.Quit();
    }
}
