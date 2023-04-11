using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private GameObject reticlePointer = null;
    //[SerializeField] private GameObject[] interactableObjects;
    private GameObject currentObject;

    private bool isObjectAttached = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        reticlePointer = player.transform.Find("Reticle")?.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
      checkIfCurrentObjectIsInteractable();

          if (isObjectAttached && (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("js3"))){ // Keyboard K, Android js3 (Y)
            Debug.Log("ObjectMenu :: Object dropped");
            DropObject();
        }
    }

    void checkIfCurrentObjectIsInteractable()
    {
        RaycastHit hit;

        if (Physics.Raycast(reticlePointer.transform.position, reticlePointer.transform.forward, out hit, Mathf.Infinity))
        {
            // Debug.Log("Inside Raycast");
            // check if raycast hitting object is interactable or not
            if (hit.collider.gameObject.CompareTag("Interactable"))
            {
                // Debug.Log( "Inside Interactable");
                if ((Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("js10"))) // Keyboard M, Android js10 (A)
                {

                    currentObject = hit.collider.gameObject;
                    GrabPassedObject(currentObject);
                }
            }
        }
    }

    public void GrabPassedObject(GameObject obj)
    {
        Debug.Log("ObjectMenu :: GrabPassedObject() called");
    
        currentObject = obj;
        GrabObject();
    }

    public void GrabObject()
    {
        Debug.Log("ObjectMenu :: GrabObject() called");
    
        //  updating object's position and setting kinematic to true
        currentObject.transform.parent = reticlePointer.transform;
        currentObject.GetComponent<Rigidbody>().isKinematic = true;
        currentObject.transform.localPosition = new Vector3(0f, 0f, 1.5f);
        
        isObjectAttached = true;
        
    }

    void DropObject() 
    {
      Debug.Log("ObjectMenu :: DropObject() called");
        // reseting everything
        currentObject.transform.parent = null;
        currentObject.GetComponent<Rigidbody>().isKinematic = false;
        currentObject = null;

        isObjectAttached = false;
    }


}
