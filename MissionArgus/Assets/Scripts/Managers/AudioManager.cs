using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> audioSources;
    Dictionary<string, AudioSource> audioSourcesDictionary = new();
    public static AudioManager instance;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSourcesDictionary.Add(audioSource.gameObject.name, audioSource);
        }
        StartCoroutine(PlayScreams());
    }

    public void PlayAudio(string _name = "", AudioSource audioSource = null)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            audioSourcesDictionary[_name].Play();
        }
    }

    IEnumerator PlayScreams()
    {
        while (true)
        {
            int whichScream = Random.Range(1, 5);
            string screamToPlay = "scream" + whichScream.ToString();
            AudioSource audioSource = GetAudioSource(screamToPlay);
            audioSource.pitch = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(Random.Range(30f, 35f));
            PlayAudio(audioSource: audioSource);
        }
    }

    AudioSource GetAudioSource(string _name)
    {
        return audioSourcesDictionary[_name];
    }
}
