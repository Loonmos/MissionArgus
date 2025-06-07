using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTimer;

public class EyeMainMenu : MonoBehaviour
{
    public GameObject openEye;
    public GameObject closedEye;
    public GameObject eyeBall;

    public bool isWorking;

    [SerializeField] private bool isLooking;
    public float lookTime;

    public float randomLookCooldown;
    public float maxTime;
    public float minTime;
    public bool timerSet;

    Timer cooldown;
    Timer look;

    void Start()
    {
        openEye.SetActive(false);
        closedEye.SetActive(true);
        eyeBall.SetActive(false);

        isLooking = false;
        timerSet = false;
    }

    void Update()
    {
        if (!isLooking && !timerSet && isWorking)
        {
            timerSet = true;

            randomLookCooldown = Random.Range(minTime, maxTime);

            cooldown = Timer.Register(randomLookCooldown, OpenTheEye);
        }
    }

    public void OpenTheEye()
    {
        openEye.SetActive(true);
        closedEye.SetActive(false);
        eyeBall.SetActive(true);

        isLooking = true;
        timerSet = false;

        look = Timer.Register(lookTime, CloseTheEye);
    }

    public void CloseTheEye()
    {
        openEye.SetActive(false);
        closedEye.SetActive(true);
        eyeBall.SetActive(false);

        isLooking = false;
    }
}
