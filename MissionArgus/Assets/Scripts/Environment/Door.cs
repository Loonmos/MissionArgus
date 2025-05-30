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
    public AudioSource audioSource;

    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        doorAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        playerDistance = (player.transform.position - transform.position).magnitude;

        if (playerDistance <= minDoorDistance && !doorOpen && (player.keyItems.ContainsKey(neededKeycard) || neededKeycard == ""))
        {
            doorAnim.SetBool("open", true);
            doorCollider.enabled = false;
            audioSource.Play();
            doorOpen = true;
        }
        else if (playerDistance >= maxDoorDistance && doorOpen)
        {
            doorAnim.SetBool("open", false);
            doorCollider.enabled = true;
            StartCoroutine(playDelay());
            doorOpen = false;
        }
    }

    IEnumerator playDelay()
    {
        yield return new WaitForSeconds(0.28f);
        audioSource.Play();
    }
}
