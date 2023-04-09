using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    private GameObject player;
    private GameObject playerXRCardboardRig = null;
    private GameObject playerEventSystem = null;
    private GameObject playerMainCamera = null;
    private GameObject playerReticlePointer = null;

    public int currentInventoryLimit = 3;
    
    public const int interactableItemsCount = 4;
    public GameObject[] interactableItems = new GameObject[interactableItemsCount];

    public GameObject gameStartMenu;
    public GameObject inventoryManager;
    public GameObject inventoryMenuCanvas;

    private GameObject inventoryItem1 = null;
    private GameObject inventoryItem2 = null;
    private GameObject inventoryItem3 = null;

    void Start()
    {
        inventoryMenuCanvas.SetActive(false);

        inventoryItem1 = inventoryMenuCanvas.transform.GetChild(0).GetChild(0).gameObject;
        inventoryItem2 = inventoryMenuCanvas.transform.GetChild(0).GetChild(1).gameObject;
        inventoryItem3 = inventoryMenuCanvas.transform.GetChild(0).GetChild(2).gameObject;
    }

    void Update()
    {
        if (!gameStartMenu.activeSelf && Input.GetButtonDown("js8")) { // 'I' key
            Debug.Log("InventoryManager :: Opening inventory menu...");

            player = GameObject.FindWithTag("Player");

            playerXRCardboardRig = player.transform.GetChild(0).gameObject;
            playerEventSystem = playerXRCardboardRig.transform.GetChild(1).gameObject;
            playerMainCamera = playerXRCardboardRig.transform.GetChild(0).GetChild(0).gameObject;
            playerReticlePointer = playerMainCamera.transform.GetChild(1).GetChild(0).gameObject;

            OpenInventoryMenu();
        }

        if (inventoryMenuCanvas.activeSelf && !gameStartMenu.activeSelf && Input.GetButtonDown("js11")){ // 'L' key
            CloseInventoryMenu();
        }
    }

    private void EnablePlayerMovement()
    {
        Debug.Log("InventoryManager :: EnablePlayerMovement() called");

        player.GetComponent<CharacterMovement>().enabled = true;

        playerXRCardboardRig.GetComponent<XRCardboardController>().enabled = true;
        playerMainCamera.GetComponent<PhysicsRaycaster>().enabled = true;
        playerReticlePointer.GetComponent<MeshRenderer>().enabled = true;

        playerEventSystem.GetComponent<StandaloneInputModule>().enabled = false;
        playerEventSystem.GetComponent<XRCardboardInputModule>().enabled = true;

        playerEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
    }

    private void DisablePlayerMovement()
    {
        Debug.Log("InventoryManager :: DisablePlayerMovement() called");

        player.GetComponent<CharacterMovement>().enabled = false;

        playerXRCardboardRig.GetComponent<XRCardboardController>().enabled = false;
        playerMainCamera.GetComponent<PhysicsRaycaster>().enabled = false;
        playerReticlePointer.GetComponent<MeshRenderer>().enabled = false;

        playerEventSystem.GetComponent<StandaloneInputModule>().enabled = true;
        if (playerEventSystem.GetComponent<XRCardboardInputModule>().enabled != false){
            playerEventSystem.GetComponent<XRCardboardInputModule>().enabled = false;
        }

        playerEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(inventoryItem1);
    }

    public void OpenInventoryMenu()
    {
        Debug.Log("InventoryManager :: OpenInventoryMenu() called");

        DisablePlayerMovement();

        inventoryMenuCanvas.SetActive(true);

        SetInventoryItemsImages();
    }

    public void CloseInventoryMenu()
    {
        Debug.Log("InventoryManager :: CloseInventoryMenu() called");

        inventoryMenuCanvas.SetActive(false);

        EnablePlayerMovement();
    }

    public void SetInventoryItemsImages()
    {
        Debug.Log("InventoryManager :: SetInventoryItemsImages() called");

        int k = 0;

        for (int i = 0; i < currentInventoryLimit; i++) 
        {
            GameObject currentInventoryItem = inventoryMenuCanvas.transform.GetChild(0).GetChild(k++).gameObject;

            currentInventoryItem.GetComponent<InventoryItemButton>().SetItemGameObject(null);
            currentInventoryItem.GetComponent<Image>().sprite = null;
        }

        k = 0;

        for (int i = 0; i < interactableItemsCount; i++)
        {
            GameObject currentObj = interactableItems[i];
            bool isPresent = currentObj.GetComponent<InventoryItem>().IsItemPresentInInventory();
            Sprite currentObjSprite = currentObj.GetComponent<InventoryItem>().GetItemImageSprite();

            if (isPresent) {
                GameObject currentInventoryItem = inventoryMenuCanvas.transform.GetChild(0).GetChild(k++).gameObject;

                currentInventoryItem.GetComponent<Image>().sprite = currentObjSprite;
                currentInventoryItem.GetComponent<InventoryItemButton>().SetItemGameObject(currentObj);
            }
        }
    }

    public void RemoveAndGrabItem(GameObject itemButton)
    {
        Debug.Log("InventoryManager :: RemoveAndGrabItem() called");
        
        Debug.Log("itemButton: " + itemButton);

        GameObject item = itemButton.GetComponent<InventoryItemButton>().GetItemGameObject();

        Debug.Log("item: " + item);
        
        for (int i = 0; i < interactableItemsCount; i++)
        {
            GameObject currentObj = interactableItems[i];

            if (currentObj == item) {
                currentObj.GetComponent<InventoryItem>().SetItemNotInInventory();

                itemButton.GetComponent<InventoryItemButton>().SetItemGameObject(null);
                itemButton.GetComponent<Image>().sprite = null;

                inventoryMenuCanvas.SetActive(false);
                currentObj.SetActive(true);

                EnablePlayerMovement();

                inventoryManager.GetComponent<InventoryGrabStore>().GrabPassedObject(currentObj);

                break;
            }
        }
    }

}