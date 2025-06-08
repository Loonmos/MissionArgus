using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    public Light2D lightFlicker;
    
    void Start()
    {
        lightFlicker = GetComponent<Light2D>();
        StartCoroutine(flickerLights());
    }

    //ienum that flice
    IEnumerator flickerLights()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            lightFlicker.enabled = false;
            yield return new WaitForSeconds(Random.Range(0.2f, 1f));
            lightFlicker.enabled = true;
        }
    }
}
