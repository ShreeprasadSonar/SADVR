using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedManager : MonoBehaviour
{
    private GameObject player = null;
    // public GameObject gameStartMenu;
    public int playerSpeed = 1; // medium
    private int[] speedLevelArray = { 5, 10, 20 };

    void Start()
    {
        Debug.Log("PlayerSpeedManager.cs started!");
    }

    void Update()
    {
        // if (!gameStartMenu.activeSelf) 
        // {
        //     player = GameObject.FindWithTag("Player");
        // }
    }

    void FindPlayerUsingTag()
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log("PlayerSpeedManager.cs :: Player: " + player);
    }

    public void SetPlayerSpeedLevel(int speedLevel)
    {
        Debug.Log("PlayerSpeedManager.cs :: Setting player speed to level " + speedLevel);
        playerSpeed = speedLevel;
        // player.GetComponent<CharacterMovement>().speed = speedLevelArray[speedLevel];
    }

    public void SetInitialLoadPlayerSpeed()
    {
        Debug.Log("PlayerSpeedManager.cs :: Setting initial player speed to level " + playerSpeed);
        
        FindPlayerUsingTag();

        player.GetComponent<CharacterMovement>().speed = speedLevelArray[playerSpeed];
    }

}
