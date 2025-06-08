using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMosster : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public bool isDetected = false;
    public bool isDamaging = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDetected = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDetected = false;
        }
    }

    void Update()
    {
        if (isDetected == true && !isDamaging)
        {
            StartCoroutine(takeDamage());
        }
    }

    IEnumerator takeDamage()
    {
        isDamaging = true;
        playerHealth.TakeDamage(5);
        yield return new WaitForSeconds(1f);
        isDamaging = false;
    }
}
