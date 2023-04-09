using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGrabStore : MonoBehaviour
{
    private GameObject player;
    private GameObject playerReticlePointer = null;

    public GameObject inventoryManager;
    public GameObject inventoryFullMsgCanvas;
    public GameObject gameStartMenu;

    private GameObject currentObject;

    public bool inventoryFull = false;
    public int inventorySize = 3;
    
    public bool isObjectAttached = false;

    void Start()
    {
        // inventoryFullMsgCanvas.SetActive(false);
    }

    void Update()
    {
        if (!gameStartMenu.activeSelf) 
        {
            player = GameObject.FindWithTag("Player");

            // Debug.Log("InventoryGrabStore :: player: " + player);

            if (player != null) 
            {
                playerReticlePointer = player.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).gameObject;

                // Debug.Log("InventoryGrabStore :: playerReticlePointer: " + playerReticlePointer);
                
                checkIfCurrentObjectIsInteractable();

                if (isObjectAttached && (Input.GetButtonDown("js10"))) // 'K' key
                {
                    Debug.Log("InventoryGrabStore :: Dropping object...");
                    DropObject();
                }
            }
        }
    }

    private void checkIfCurrentObjectIsInteractable()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerReticlePointer.transform.position, playerReticlePointer.transform.forward, out hit, Mathf.Infinity))
        {

            // Debug.Log("InventoryGrabStore :: hit.collider.gameObject -> " + hit.collider.gameObject);

            // check if raycast hitting object is interactable or not

            bool isInteractable = hit.collider.gameObject.CompareTag("Interactable");
            bool isInteractableParent = hit.collider.gameObject.transform.parent.CompareTag("Interactable");

            if (isInteractable || isInteractableParent)
            {
                // Debug.Log("InventoryGrabStore :: Current object is interactable");

                if ((Input.GetButtonDown("js12"))) // 'M' key
                {
                    Debug.Log("InventoryGrabStore :: 'M' key pressed");

                    currentObject = isInteractable ? hit.collider.gameObject : hit.collider.gameObject.transform.parent.gameObject;
                    GrabObject();
                }

                if ((Input.GetButtonDown("js9"))) // 'J' key
                {
                    Debug.Log("InventoryGrabStore :: 'J' key pressed");

                    currentObject = isInteractable ? hit.collider.gameObject : hit.collider.gameObject.transform.parent.gameObject;
                    StoreObject();
                }
            }
        }
    }

    public void StoreObject()
    {
        Debug.Log("InventoryGrabStore :: StoreObject() called");
      
        if (!inventoryFull)
        {
            Debug.Log("Current obj: " + currentObject);

            currentObject.GetComponent<InventoryItem>().SetItemInInventory();
            currentObject.SetActive(false);

            // inventoryManager.GetComponent<InvenManager>().currentInventoryLimit = 0;
            
            inventorySize--;
            
            if (inventorySize <= 0)
            {
                inventoryFull = true;
            }
        }
        else
        {
            // display the 'inventory full' message using a coroutine for 2 seconds
            StartCoroutine(ShowInventoryFullMessage());
        }
    }

    public void GrabPassedObject(GameObject obj)
    {
        Debug.Log("InventoryGrabStore :: GrabPassedObject() called");
    
        Debug.Log("Passed obj: " + obj);

        currentObject = obj;

        GrabObject();
    }

    public void GrabObject()
    {
        Debug.Log("InventoryGrabStore :: GrabObject() called");
    
        //  updating object's position and setting kinematic to true
        currentObject.transform.parent = playerReticlePointer.transform;
        currentObject.GetComponent<Rigidbody>().isKinematic = true;
        currentObject.transform.localPosition = new Vector3(0f, 0f, 1.5f);
        
        isObjectAttached = true;
    }

    void DropObject() 
    {
        // reseting everything
        currentObject.transform.parent = null;
        currentObject.GetComponent<Rigidbody>().isKinematic = false;
        currentObject = null;

        isObjectAttached = false;
    }

    IEnumerator ShowInventoryFullMessage()
    {
        // inventoryFullMsgCanvas.SetActive(true);
        // inventoryFullMsgCanvas.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Inventory is Full";
        Debug.Log("InventoryGrabStore :: Inventory is Full");
        yield return new WaitForSeconds(2f);
        // inventoryFullMsgCanvas.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
        // inventoryFullMsgCanvas.SetActive(false);
    }

}