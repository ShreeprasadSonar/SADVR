using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemButton : MonoBehaviour
{
    public GameObject itemGameObject = null;

    public void SetItemGameObject(GameObject obj)
    {
        itemGameObject = obj;
    }

    public GameObject GetItemGameObject()
    {
        return itemGameObject;
    }

}
