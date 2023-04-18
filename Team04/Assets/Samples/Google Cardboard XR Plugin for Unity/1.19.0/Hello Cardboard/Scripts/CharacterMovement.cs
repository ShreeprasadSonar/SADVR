using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class CharacterMovement : MonoBehaviour
{
    CharacterController charCntrl;
    [Tooltip("The speed at which the character will move.")]
    public float speed = 5f;
    [Tooltip("The camera representing where the character is looking.")]
    public GameObject cameraObj;
    [Tooltip("Should be checked if using the Bluetooth Controller to move. If using keyboard, leave this unchecked.")]
    public bool joyStickMode;

    Rigidbody rb;
    PhotonView PV;

    PlayerManager playerManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();

        // playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!PV.IsMine){
            Destroy(GetComponentInChildren<AudioListener>());
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<PhysicsRaycaster>().enabled = false;
            GetComponentInChildren<XRCardboardController>().enabled = false;
            Destroy(rb);
        }
        charCntrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        //Get horizontal and Vertical movements
        float horComp = Input.GetAxis("Horizontal");
        float vertComp = Input.GetAxis("Vertical");

        if (joyStickMode)
        {
            horComp = Input.GetAxis("Vertical");
            vertComp = Input.GetAxis("Horizontal") * -1;
        }

        Vector3 moveVect = Vector3.zero;

        //Get look Direction
        Vector3 cameraLook = cameraObj.transform.forward;
        cameraLook.y = 0f;
        cameraLook = cameraLook.normalized;

        Vector3 forwardVect = cameraLook;
        Vector3 rightVect = Vector3.Cross(forwardVect, Vector3.up).normalized * -1;

        moveVect += rightVect * horComp;
        moveVect += forwardVect * vertComp;

        moveVect *= speed;
     

        charCntrl.SimpleMove(moveVect);


    }

    void FixedUpdate() {
        if (!PV.IsMine)
        {
            return;
        }
    }
}
