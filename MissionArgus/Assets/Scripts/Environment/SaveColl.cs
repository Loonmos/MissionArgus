using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveColl : MonoBehaviour
{
    public SaveAndLoad saveLoad;
    public Transform roomPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            saveLoad.SavePosition(roomPosition);
        }
    }
}
