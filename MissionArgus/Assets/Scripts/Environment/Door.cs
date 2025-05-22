using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float playerDistance;
    public PlayerInventory player;
    public bool doorOpen = false;
    public string neededKeycard;
    public float minDoorDistance;
    public float maxDoorDistance;
    public Animator doorAnim;
    public Collider2D doorCollider;

    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        doorAnim = GetComponent<Animator>();
    }

    void Update()
    {
        playerDistance = (player.transform.position - transform.position).magnitude;

        if (playerDistance <= minDoorDistance && !doorOpen && (player.keyItems.ContainsKey(neededKeycard) || neededKeycard == ""))
        {
            doorAnim.SetBool("open", true);
            doorCollider.enabled = false;
            doorOpen = true;
        }
        else if (playerDistance >= maxDoorDistance && doorOpen)
        {
            doorAnim.SetBool("open", false);
            doorCollider.enabled = true;
            doorOpen = false;
        }
    }
}
