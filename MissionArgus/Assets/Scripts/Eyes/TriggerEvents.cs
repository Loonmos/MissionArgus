using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    public UnityEvent event1;
    public UnityEvent event2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        event1.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        event2.Invoke();
    }
}
