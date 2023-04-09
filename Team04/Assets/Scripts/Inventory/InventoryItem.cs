using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public bool inventoryFlag = false;
    public Sprite itemSprite;

    public bool IsItemPresentInInventory()
    {
        return inventoryFlag;
    }

    public Sprite GetItemImageSprite()
    {
        return itemSprite;
    }

    public void SetItemInInventory()
    {
        inventoryFlag = true;
    }

    public void SetItemNotInInventory()
    {
        inventoryFlag = false;
    }

}
