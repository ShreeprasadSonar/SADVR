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
    
    private GameObject player;

    private GameObject mainCamera;
    private GameObject Reticle;
    private GameObject XRCardboardRig;
    private GameObject EventSystem;
    
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
        if (!gameStartMenu.activeSelf && Input.GetButtonDown("js7")) { // 'h'

          Debug.Log("InGameMenu.cs :: 'H' key pressed!");

          // if (inventoryMenu.activeSelf) {
          //   inventoryMenu.SetActive(false);
          // }

          player = GameObject.FindWithTag("Player");

          // XRCardboardRig = player.transform.Find("XRCardboardRig").gameObject;
          // EventSystem = player.transform.Find("EventSystem").gameObject;
          // mainCamera = player.transform.Find("Main Camera").gameObject;
          // Reticle = player.transform.Find("Reticle").gameObject;

          Debug.Log("\n*******");
          Debug.Log("player: " + player);
          // Debug.Log("XRCardboardRig: " + XRCardboardRig);
          // Debug.Log("EventSystem: " + EventSystem);
          // Debug.Log("mainCamera: " + mainCamera);
          // Debug.Log("Reticle: " + Reticle);
          Debug.Log("*******\n");

          OpenInGameMenu();
        }
    }

    public void OpenInGameMenu()
    {
        Debug.Log("OpenInGameMenu() called");

        inGameMenu.SetActive(true);

        player.GetComponent<CharacterMovement>().enabled = false;

        // mainCamera.GetComponent<PhysicsRaycaster>().enabled = false;
        // Reticle.GetComponent<MeshRenderer>().enabled = false;
        // XRCardboardRig.GetComponent<XRCardboardController>().enabled = false;

        // EventSystem.GetComponent<StandaloneInputModule>().enabled = true;
        // if (EventSystem.GetComponent<XRCardboardInputModule>().enabled != false){
        //     EventSystem.GetComponent<XRCardboardInputModule>().enabled = false;
        // }
    }

    public void CloseInGameMenu()
    {
        Debug.Log("CloseInGameMenu() called");

        inGameMenu.SetActive(false);

        player.GetComponent<CharacterMovement>().enabled = true;

        // mainCamera.GetComponent<PhysicsRaycaster>().enabled = true;
        // Reticle.GetComponent<MeshRenderer>().enabled = true;
        // XRCardboardRig.GetComponent<XRCardboardController>().enabled = true;

        // EventSystem.GetComponent<StandaloneInputModule>().enabled = false;
        // EventSystem.GetComponent<XRCardboardInputModule>().enabled = true;
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
