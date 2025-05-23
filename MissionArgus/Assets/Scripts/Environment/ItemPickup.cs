using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public Sprite item1;
    public Sprite item2;
    public float playerDistance;
    private SpriteRenderer spriteRenderer;
    public PlayerInventory playerInventory;
    public bool inInventory;
    public string itemName;
    public float spriteChangeDistance;
    public GameObject itemLight;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item1;
        itemLight.SetActive(false);
    }

    void Update()
    {
        if (inInventory) return;
        playerDistance = (playerInventory.transform.position - transform.position).magnitude;

        if (playerDistance <= spriteChangeDistance && spriteRenderer.sprite != item2)
        {
            spriteRenderer.sprite = item2;
            itemLight.SetActive(true);
        }
        else if (playerDistance >= spriteChangeDistance && spriteRenderer.sprite != item1)
        {
            spriteRenderer.sprite = item1;
            itemLight.SetActive(false);
        }

        if (spriteRenderer.sprite == item2 && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E registered");
            playerInventory.AddItemToInventory(this);
            transform.position = new Vector3(1000, -1000, 1000); //teleport it to the middle of fucking nowhere!!!!!!!!!
            inInventory = true;
        }
    }
}
