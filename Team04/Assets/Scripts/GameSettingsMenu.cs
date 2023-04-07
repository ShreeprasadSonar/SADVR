using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameSettingsMenu : MonoBehaviour
{

    public GameObject gameStartMenuCanvas;
    public GameObject gameSettingsMenuCanvas;

    public GameObject playerSpeedManager;
    public AudioManager audioManager;
    
    public GameObject playerSpeedButton;
    public GameObject audioLevelButton;

    public Sprite highSpeedButtonImageSprite;
    public Sprite mediumSpeedButtonImageSprite;
    public Sprite lowSpeedButtonImageSprite;

    public Sprite softAudioLevelButtonImageSprite;
    public Sprite defaultAudioLevelButtonImageSprite;
    public Sprite loudAudioLevelButtonImageSprite;

    void Start()
    {
        Debug.Log("GameSettingsMenu.cs started!");
    }

    void Update()
    {
        
    }

    public void OpenGameSettingsMenu()
    {
        Debug.Log("GameSettingsMenu.cs :: OpenMenu() called!");
        gameStartMenuCanvas.SetActive(true);
    }

    public void CloseGameSettingsMenu()
    {
        Debug.Log("GameSettingsMenu.cs :: CloseMenu() called!");
        gameStartMenuCanvas.SetActive(false);
    }

    public void SetPlayerSpeedLevel()
    {
        Debug.Log("GameSettingsMenu.cs :: SetPlayerSpeedLevel() called!");

        int currentSpeedLevel = playerSpeedManager.GetComponent<PlayerSpeedManager>().playerSpeed;

        int newSpeedLevel = (currentSpeedLevel + 1) % 3;

        playerSpeedManager.GetComponent<PlayerSpeedManager>().SetPlayerSpeedLevel(newSpeedLevel);

        if (newSpeedLevel == 0) {
            playerSpeedButton.GetComponent<Image>().sprite = lowSpeedButtonImageSprite; // low
        } else if (newSpeedLevel == 1) {
            playerSpeedButton.GetComponent<Image>().sprite = mediumSpeedButtonImageSprite; // medium
        } else if (newSpeedLevel == 2) {
            playerSpeedButton.GetComponent<Image>().sprite = highSpeedButtonImageSprite; // high
        }
    }

    public void SetAudioLevel()
    {
        Debug.Log("GameSettingsMenu.cs :: SetAudioLevel() called!");

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

    public void BackToGameStartMenu()
    {
        Debug.Log("GameSettingsMenu.cs :: BackToGameStartMenu() called!");
        
        gameSettingsMenuCanvas.SetActive(false);
        gameStartMenuCanvas.SetActive(true);
    }

}
