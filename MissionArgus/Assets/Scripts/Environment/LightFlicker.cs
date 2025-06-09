using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    public Light2D lightFlicker;
    public GameObject lightOn, lightOff;
    public Sprite spriteOn, spriteOff;
    public SpriteRenderer sr;
    
    void Start()
    {
        lightFlicker = GetComponent<Light2D>();
        //lightSprite = GetComponent<SpriteRenderer>();
        StartCoroutine(flickerLights());
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
    }

    //ienum that flice
    IEnumerator flickerLights()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            if (lightFlicker != null) lightFlicker.enabled = false;
            if (spriteOff != null)
            {
                Debug.Log("THIS IS BEING CALLED!!!!! BY " + gameObject.name);
                sr.sprite = spriteOff;  
            }
            if (lightOff != null) lightOff.SetActive(true);
            if (lightOn != null) lightOn.SetActive(false);
            yield return new WaitForSeconds(Random.Range(0.2f, 1f));
            if (lightFlicker != null) lightFlicker.enabled = true;
            if (spriteOn != null)
            {
                sr.sprite = spriteOn;
            }
            if (lightOn != null) lightOn.SetActive(true);
            if (lightOff != null) lightOff.SetActive(false);
        }
    }
}
