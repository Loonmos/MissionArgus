using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainAbility : MonoBehaviour
{
    public PlayerMovement2DPlatformer playerMovement;
    public GameObject boots;
    public GameObject particles;
    public Animator anim;
    public AllDialogue dialogue;

    public float playerDistance;
    public GameObject player;
    public bool inInventory;
    public float spriteChangeDistance;
    public GameObject itemLight;

    public bool pickedUp;
    public GameObject interact;

    void Start()
    {
        itemLight.SetActive(false);
        pickedUp = false;
        interact.SetActive(false);
    }

    void Update()
    {
        if (inInventory) return;
        playerDistance = (player.transform.position - transform.position).magnitude;

        if (playerDistance <= spriteChangeDistance && !pickedUp)
        {
            itemLight.SetActive(true);
            interact.SetActive(true);
        }
        else if (playerDistance >= spriteChangeDistance && !pickedUp)
        {
            itemLight.SetActive(false);
            interact.SetActive(false);
        }

        if (playerDistance <= spriteChangeDistance && Input.GetKeyDown(KeyCode.E))
        {
            pickedUp = true;
            Debug.Log("E registered");
            playerMovement.unlockedJump = true;
            boots.SetActive(false);
            particles.SetActive(false);
            itemLight.SetActive(false);
            interact.SetActive(false);
            anim.Play("Ability");
            dialogue.ActivateJumpText();
            inInventory = true;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    playerMovement.unlockedJump = true;
    //    boots.SetActive(false);
    //    particles.SetActive(false);
    //    anim.Play("Ability");
    //    dialogue.ActivateJumpText();
    //    gameObject.SetActive(false);
    //}
}
