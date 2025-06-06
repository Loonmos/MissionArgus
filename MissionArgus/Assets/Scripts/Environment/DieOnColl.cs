using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnColl : MonoBehaviour
{
    public GameObject player;
    public PlayerHealth playerHealth;
    public AllDialogue dialogue;

    private void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!dialogue.isChasing)
        {
            playerHealth.ObstacleDeath();
        }
        else if (dialogue.isChasing)
        {
            dialogue.ChaseFail();
        }
        
    }
}
