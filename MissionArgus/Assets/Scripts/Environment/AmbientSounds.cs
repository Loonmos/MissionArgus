using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSounds : MonoBehaviour
{
    public AudioSource[] audioSources;

    public bool playing = false;
    private float timer = 0;
    public float delay = 0;

    public void Play()
    {
        playing = true;
    }

    public void Stop()
    {
        playing = false;
        timer = 0;
    }

    private void Start()
    {
        Play();
    }

    void Update()
    {
        foreach (AudioSource audio in audioSources)
        {
            if (audio.isPlaying)
            {
                playing = true;
            }
        }

        if (!playing)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                audioSources[Random.Range(0, audioSources.Length)].Play();
                timer = 0;
            }
        }
    }
}
