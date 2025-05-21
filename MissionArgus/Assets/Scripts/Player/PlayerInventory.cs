using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerInventory : MonoBehaviour
{
    //public List<ItemPickup> inventoryItems = new();
    public Dictionary<string, ItemPickup> keyItems = new();
    public List<Image> images;

    public void AddItemToInventory(ItemPickup _itemPickup)
    {
        keyItems.Add(_itemPickup.itemName, _itemPickup);
        //  images[keyItems.Count - 1].sprite = _itemPickup.item1;
        Image imageToChange = images[keyItems.Count - 1];
        imageToChange.gameObject.SetActive(true);
        imageToChange.sprite = _itemPickup.item1;
    }
}
