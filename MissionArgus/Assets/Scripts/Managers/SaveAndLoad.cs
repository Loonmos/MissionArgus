using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    [SerializeField] private Transform lastPosition;
    public Transform player;

    public PlayerHealth playerHealth;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerHealth.ObstacleDeath();
        }
    }

    public void SavePosition(Transform position)
    {
        lastPosition = position;
    }

    public void LoadPosition()
    {
        player.position = lastPosition.position;
    }
}
