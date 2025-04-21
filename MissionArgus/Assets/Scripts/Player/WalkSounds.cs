using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WalkSounds : MonoBehaviour
{
    public AudioSource[] audioSources;

    public bool playing = false;
    float timer = 0;
    float delay = 0;

    public void Play(float _delay)
    {
        delay = _delay;
        playing = true;
    }

    public void Stop()
    {
        playing = false;
        timer = 0;
    }


    void Update()
    {
        if(playing)
        {
            timer += Time.deltaTime;
            if(timer > delay) 
            {
                audioSources[Random.Range(0, audioSources.Length)].Play();
                timer = 0;
            }
        }
    }
}
