using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarkDia1 : MonoBehaviour
{
    // state machine my beloved

    public GameObject diaScreenMark;
    public TextMeshProUGUI diaTextMark;
    public Animator animMark;
    public GameObject mark;

    public GameObject diaScreenPlayer;
    public TextMeshProUGUI diaTextPlayer;
    public Animator animPlayer;

    public enum State { Nothing, M1, M2, M3, M4, M5, M6, M7, M8, M9, M10, M11, M12 }
    public State state = State.Nothing;

    private float cooldown = 0;
    public float textTime;
    public bool finishedDialogue;

    public PlayerMovement2DPlatformer playerMovement;

    public GameObject triggerObject;

    void Start()
    {
        diaScreenMark.SetActive(false);
        diaScreenPlayer.SetActive(false);
        triggerObject.SetActive(true);
        finishedDialogue = false;
    }


    void Update()
    {
        CheckState();

        if (state != State.Nothing)
        {
            cooldown += Time.deltaTime;
        }
    }

    private void CheckState()
    {
        switch (state)
        {
            case State.Nothing: Nothing(); break;
            case State.M1: M1(); break;
            case State.M2: M2(); break;
            case State.M3: M3(); break;
            case State.M4: M4(); break;
            case State.M5: M5(); break;
            case State.M6: M6(); break;
            case State.M7: M7(); break;
            case State.M8: M8(); break;
            case State.M9: M9(); break;
            case State.M10: M10(); break;
            case State.M11: M11(); break;
            case State.M12: M12(); break;
        }
    }

    private void Nothing()
    {
        playerMovement.enabled = true;
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(false);
        cooldown = 0;
    }

    public void TriggerMark1()
    {
        if (!finishedDialogue)
        {
            state = State.M1;
        }
        else
        {
            state = State.M12;
        }
    }

    private void M1()
    {
        playerMovement.enabled = false;
        playerMovement.rb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("HEY YOU!");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.M2;
        }
    }

    private void M2()
    {
        diaTextMark.SetText("WHO THE HELL ARE YOU?");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.M3;
        }
    }

    private void M3()
    {
        diaScreenPlayer.SetActive(true);
        diaScreenMark.SetActive(false);
        diaTextPlayer.SetText("I'm Sam. I've been sent from Proxima Centauri to fix your communication station.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.M4;
        }
    }

    private void M4()
    {
        diaScreenPlayer.SetActive(true);
        diaTextPlayer.SetText("What happened here? Where is everyone?");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.M5;
        }
    }

    private void M5()
    {
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("LET'S JUST CALL IT AN EXPERIMENT GONE WRONG.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.M6;
        }
    }

    private void M6()
    {
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("HERE IS NOTHING FOR YOU TO DO HERE. YOU NEED TO GET THE HELL OUT BEFORE THEY GET YOU TOO.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.M7;
        }
    }

    private void M7()
    {
        diaScreenPlayer.SetActive(true);
        diaScreenMark.SetActive(false);
        diaTextPlayer.SetText("Who are they?");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.M8;
        }
    }

    private void M8()
    {
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("NOW DON'T YOU WORRY ABOUT THAT, JUST DON'T LOOK OUT OF THE WINDOW OR TOUCH THE GREEN STUFF.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            //animMarkMove.SetBool("move2", false);
            cooldown = 0;
            state = State.M9;
        }
    }

    private void M9()
    {
        diaScreenPlayer.SetActive(true);
        diaScreenMark.SetActive(false);
        diaTextPlayer.SetText("Look I can't even leave until I've recharged my ship, just tell me what is going on.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("trigger expo2");
            cooldown = 0;
            state = State.M10;
        }
    }

    private void M10()
    {
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("THEN YOU GO DOWN TO THE POWER STATION, FIX IT AND LEAVE!");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.M11;
        }
    }

    private void M11()
    {
        diaScreenPlayer.SetActive(true);
        diaScreenMark.SetActive(false);
        diaTextPlayer.SetText("Come on man, you gotta tell me something.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.M12;
        }
    }

    private void M12()
    {
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("LEAVE!!");

        finishedDialogue = true;

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Nothing;
        }
    }
}