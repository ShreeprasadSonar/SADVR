using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabItem : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private GameObject reticlePointer;
    //[SerializeField] private GameObject[] interactableObjects;
    private GameObject currentObject;

    private bool isObjectAttached = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
      checkIfCurrentObjectIsInteractable();

          if (isObjectAttached && (Input.GetButtonDown("js11"))){
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
                if ((Input.GetButtonDown("js5"))) // X in Android & Y in PC
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
