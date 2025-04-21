using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnColl : MonoBehaviour
{
    public GameObject player;
    public PlayerHealth playerHealth;

    private void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerHealth.ObstacleDeath();
    }
}
