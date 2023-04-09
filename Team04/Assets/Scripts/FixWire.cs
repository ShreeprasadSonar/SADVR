using UnityEngine;

public class FixWire : MonoBehaviour
{
    public GameObject Wires;
    private float holdTime;
    private bool isPointerOnWire=false;

    void Update()
    {
        
        if ((Input.GetKey(KeyCode.L) || Input.GetButton("js10")) && isPointerOnWire) // Keyboard L, Android js10 (A)
        {
            
            holdTime += Time.deltaTime;

            if (holdTime >= 3f)
            {
                Wires.SetActive(false);
            }
        }
        else
        {
            holdTime = 0f;
        }
    }

    public void OnPointerEnter()
    {
        isPointerOnWire = true;
    }

    public void OnPointerExit()
    {
        isPointerOnWire = false;
    }
}
