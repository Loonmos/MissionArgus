using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDetect : MonoBehaviour
{
    public EnemyAI enemyAI;
    public PlayerHealth playerHealth;
    public bool isDetected = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDetected = true;
            enemyAI.tween.Pause();
        }
    }
    //    void OnTriggerStay2D(Collider2D collision)
    //    {
    //        if (collision.CompareTag("Player"))
    //        {
    //            playerHealth.TakeDamage(1);
    //        }
    //    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDetected = false;
            enemyAI.tween.Play();
        }
    }

    void Update()
    {
        if (isDetected == true)
        {
            StartCoroutine(takeDamage());
        }
    }

    IEnumerator takeDamage()
    {
        playerHealth.TakeDamage(1);
        yield return new WaitForSeconds(2f);
    }
}
