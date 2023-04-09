using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OutlineOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Outline obj;

    void Start() {
        // Debug.Log("Highlighter.cs: Script started");
        obj = GetComponent<Outline>();
        obj.enabled = false;
    }

    void Update() {
        
    }

    public void OnPointerEnter(PointerEventData eventData) {
        // Debug.Log("Highlighter.cs: Pointer ENTER");
        obj.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        // Debug.Log("Highlighter.cs: Pointer EXIT");
        obj.enabled = false;
    }

}
