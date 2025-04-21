using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBallBehaviour : MonoBehaviour
{
    public GameObject lookAt;
    private float distance;
    public float speed = 1;
    public float maxDistance;

    void Start()
    {
        
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, lookAt.transform.position);

        if (distance > maxDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, lookAt.transform.position, speed * Time.deltaTime);
        }
    }
}
