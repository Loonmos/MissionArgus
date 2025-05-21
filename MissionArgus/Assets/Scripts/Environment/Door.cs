using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float playerDistance;
    public PlayerInventory player;
    public bool doorOpen = false;
    public string neededKeycard;
    public float minDoorDistance;
    public float maxDoorDistance;

    void Update()
    {
        playerDistance = (player.transform.position - transform.position).magnitude;

        if (playerDistance <= minDoorDistance && !doorOpen && player.keyItems.ContainsKey(neededKeycard))
        {
            transform.position += new Vector3(0, 3, 0);
            doorOpen = true;
        }
        else if (playerDistance >= maxDoorDistance && doorOpen)
        {
            transform.position += new Vector3(0, -3, 0);
            doorOpen = false;
        }
    }
}
