using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialColliders : MonoBehaviour
{
    public DialogueSystem tutManager;
    public UnityEvent event1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            event1.Invoke();
            //Debug.Log("triggered event");
        }
    }

    public void MoveMark2()
    {
        tutManager.state = DialogueSystem.State.MoveMark2;
        Destroy(gameObject);
        Debug.Log("triggered state");
    }

    public void Expo1()
    {
        tutManager.state = DialogueSystem.State.Expo1;
        Destroy(gameObject);
        //Debug.Log("triggered state");
    }

    public void Death1()
    {
        tutManager.state = DialogueSystem.State.Death1;
        Destroy(gameObject);
        //Debug.Log("triggered state");
    }
}
