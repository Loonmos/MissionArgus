using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class AllMarkEvents : MonoBehaviour
{
    [Header("Mark Stuff")]
    public GameObject screenMark;
    public TextMeshProUGUI textMark;
    public Animator animMark;
    public GameObject mark;

    public Transform markpos2;

    [Header("Player Stuff")]
    public GameObject screenPlayer;
    public TextMeshProUGUI textPlayer;
    public Animator animPlayer;
    public PlayerMovement2DPlatformer playerMovement;
    public Rigidbody2D playerRb;

    [Header("State Machine Stuff")]
    private float cooldown = 0;
    public float textTime;
    public bool finishedDialogue1;
    public bool finishedDialogue2;
    public bool finishedDialogue3;

    public enum State { Nothing, M10, P10, M11, M20, P20, M21, M30, P30, M31 }
    public State state = State.Nothing;

    [Header("Triggers")]
    public GameObject triggerEvent1;
    public GameObject triggerEvent2;
    public GameObject triggerEvent3;

    void Start()
    {
        screenMark.SetActive(false);
        screenPlayer.SetActive(false);
        triggerEvent1.SetActive(true);
        finishedDialogue1 = false;
        finishedDialogue2 = false;
        finishedDialogue3 = false;
        mark.SetActive(true);
        playerMovement.enabled = true;
    }

    void Update()
    {
        CheckState();

        if (state != State.Nothing)
        {
            cooldown += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Transition1to2();
        }
    }

    private void CheckState()
    {
        switch (state)
        {
            case State.Nothing: Nothing(); break;

            case State.M10: M10(); break;
            case State.P10: P10(); break;
            case State.M11: M11(); break;

            case State.M20: M20(); break;
            case State.P20: P20(); break;
            case State.M21: M21(); break;

            case State.M30: M30(); break;
            case State.P30: P30(); break;
            case State.M31: M31(); break;
        }
    }

    private void Nothing()
    {
        screenMark.SetActive(false);
        screenPlayer.SetActive(false);
        playerMovement.enabled = true;
    }

    public void TriggerEvent1()
    {
        if (!finishedDialogue1)
        {
            state = State.M10;
        }
        else
        {
            state = State.M11;
        }
    }

    private void M10()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("This is event 1");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.P10;
        }
    }

    private void P10()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark.SetActive(false);
        screenPlayer.SetActive(true);
        textPlayer.SetText("This is player response 1");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.M11;
        }
    }

    private void M11()
    {
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("This is the end text 1");

        finishedDialogue1 = true;

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Nothing;
        }
    }

    public void Transition1to2()
    {
        mark.transform.DOMove(markpos2.position, 10f);
        triggerEvent1.SetActive(false);
        triggerEvent2.SetActive(true);
    }

    public void TriggerEvent2()
    {
        if (!finishedDialogue2)
        {
            state = State.M20;
        }
        else
        {
            state = State.M21;
        }
    }

    private void M20()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("This is event 2");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.P20;
        }
    }

    private void P20()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark.SetActive(false);
        screenPlayer.SetActive(true);
        textPlayer.SetText("This is player response 2");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.M21;
        }
    }

    private void M21()
    {
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("This is the end text 2");

        finishedDialogue2 = true;

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Nothing;
        }
    }

    public void Transition2to3()
    {
        //move mark
        triggerEvent2.SetActive(false);
        triggerEvent3.SetActive(true);
    }

    public void TriggerEvent3()
    {
        if (!finishedDialogue3)
        {
            state = State.M30;
        }
        else
        {
            state = State.M31;
        }
    }

    private void M30()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("This is event 3");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.P30;
        }
    }

    private void P30()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark.SetActive(false);
        screenPlayer.SetActive(true);
        textPlayer.SetText("This is player response 3");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.M31;
        }
    }

    private void M31()
    {
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("This is the end text 3");

        finishedDialogue3 = true;

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Nothing;
        }
    }

}
