using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    [SerializeField] private Transform lastPosition;
    public Transform player;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
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
