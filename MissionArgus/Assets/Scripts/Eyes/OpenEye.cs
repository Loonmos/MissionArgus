using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEye : MonoBehaviour
{
    public GameObject openEye;
    public GameObject closedEye;
    public GameObject eyeBall;

    public GameObject player;
    private PlayerHealth playerHealth;

    [SerializeField] private bool isLooking;
    public int damage = 5;
    public float damageCooldown = 1;
    private bool damageReady;

    public AudioSource eyeNoise;
    public float startVolume;
    public float endVolume;
    public float fadeLength;

    public bool alreadyOpen;
    public bool isNotPlayer;

    public GameObject UIisLooking;

    void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();

        if (alreadyOpen)
        {
            openEye.SetActive(true);
            closedEye.SetActive(false);
            eyeBall.SetActive(true);
        }
        else
        {
            openEye.SetActive(false);
            closedEye.SetActive(true);
            eyeBall.SetActive(false);
        }

        isLooking = false;
        damageReady = true;
    }

    private void Update()
    {
        if (isLooking && damageReady && !isNotPlayer)
        {
            StartCoroutine(GiveDamage());
        }
    }

    IEnumerator GiveDamage()
    {
        damageReady = false;
        playerHealth.TakeDamage(damage);

        yield return new WaitForSeconds(damageCooldown);

        damageReady = true;
    }

    public void OpenTheEye()
    {
        openEye.SetActive(true);
        closedEye.SetActive(false);
        eyeBall.SetActive(true);
        UIisLooking.SetActive(true);

        isLooking = true;
        if (!isNotPlayer)
        {
            StopCoroutine(VolumeEnd());
            eyeNoise.Play();
            StartCoroutine(VolumeStart());
        }
    }

    public void CloseTheEye()
    {
        openEye.SetActive(false);
        closedEye.SetActive(true);
        eyeBall.SetActive(false);
        UIisLooking.SetActive(false);

        isLooking = false;
        StopCoroutine(VolumeStart());
        StartCoroutine(VolumeEnd());
    }
    private IEnumerator VolumeStart()
    {
        float startTime = Time.time;

        while (Time.time < startTime + fadeLength && eyeNoise.isPlaying)
        {
            eyeNoise.volume = endVolume + ((startVolume - endVolume) * ((Time.time - startTime) / fadeLength));

            yield return null;
        }

        if (startVolume == 0.8f) { StopCoroutine(VolumeStart()); }
    }

    private IEnumerator VolumeEnd()
    {
        float startTime = Time.time;

        while (Time.time < startTime + fadeLength && eyeNoise.isPlaying)
        {
            eyeNoise.volume = startVolume + ((endVolume - startVolume) * ((Time.time - startTime) / fadeLength));

            yield return null;
        }

        if (endVolume == 0) { eyeNoise.Stop(); }
    }
}
