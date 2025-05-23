using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarkDia2 : MonoBehaviour
{
    // state machine my beloved

    public GameObject diaScreenMark;
    public TextMeshProUGUI diaTextMark;
    public Animator animMark;
    public GameObject mark2;

    public GameObject diaScreenPlayer;
    public TextMeshProUGUI diaTextPlayer;
    public Animator animPlayer;

    public enum State { Nothing, A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11, A12 }
    public State state = State.Nothing;

    private float cooldown = 0;
    public float textTime;
    public bool finishedDialogue;

    public PlayerMovement2DPlatformer playerMovement;

    public GameObject triggerObject;
    public Transform markPos1;

    void Start()
    {
        diaScreenMark.SetActive(false);
        diaScreenPlayer.SetActive(false);
        finishedDialogue = false;
        mark2.SetActive(false);
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
            case State.A1: A1(); break;
            case State.A2: A2(); break;
            case State.A3: A3(); break;
            case State.A4: A4(); break;
            case State.A5: A5(); break;
            case State.A6: A6(); break;
            case State.A7: A7(); break;
            case State.A8: A8(); break;
            case State.A9: A9(); break;
            case State.A10: A10(); break;
            case State.A11: A11(); break;
            case State.A12: A12(); break;
        }
    }

    private void Nothing()
    {
        playerMovement.enabled = true;
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(false);
        cooldown = 0;
    }

    public void TriggerMarkMove1()
    {
        //state = State.M1;
        mark2.SetActive(true);
        triggerObject.SetActive(true);
        Debug.Log("trigger move mark");
    }

    public void TriggerMark2()
    {
        if (!finishedDialogue)
        {
            state = State.A2;
        }
        else
        {
            state = State.A10;
        }
    }

    private void A1()
    {
        //mark.position = markPos1.position;
        triggerObject.SetActive(true);
        Debug.Log("moved mark");
    }

    private void A2()
    {
        playerMovement.enabled = false;
        playerMovement.rb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("Wow, you did it.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.A3;
        }
    }

    private void A3()
    {
        playerMovement.enabled = false;
        playerMovement.rb.velocity = new Vector2(0, 0);
        diaScreenPlayer.SetActive(true);
        diaScreenMark.SetActive(false);
        diaTextPlayer.SetText("It wasn't too bad.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.A4;
        }
    }

    private void A4()
    {
        playerMovement.enabled = false;
        playerMovement.rb.velocity = new Vector2(0, 0);
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("You know, it might be a good idea to contact others. Maybe you could fix the communications station.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.A5;
        }
    }

    private void A5()
    {
        playerMovement.enabled = false;
        playerMovement.rb.velocity = new Vector2(0, 0);
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("It seems like some wires are fried, but there should be spares in storage.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.A6;
        }
    }

    private void A6()
    {
        playerMovement.enabled = false;
        playerMovement.rb.velocity = new Vector2(0, 0);
        diaScreenPlayer.SetActive(true);
        diaScreenMark.SetActive(false);
        diaTextPlayer.SetText("I probably could, but what would you be doing in the meantime?");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.A7;
        }
    }

    private void A7()
    {
        playerMovement.enabled = false;
        playerMovement.rb.velocity = new Vector2(0, 0);
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("Well I wouldn't really be able to help you, so I'm gonna go to life support to refill my water tank.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.A8;
        }
    }

    private void A8()
    {
        playerMovement.enabled = false;
        playerMovement.rb.velocity = new Vector2(0, 0);
        diaScreenPlayer.SetActive(true);
        diaScreenMark.SetActive(false);
        diaTextPlayer.SetText("Fine, but I'd better hear the full story of this place when get comms up and running.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            //animMarkMove.SetBool("move2", false);
            cooldown = 0;
            state = State.A9;
        }
    }

    private void A9()
    {
        playerMovement.enabled = false;
        playerMovement.rb.velocity = new Vector2(0, 0);
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("Yeah yeah, see you in a bit.");

        finishedDialogue = true;

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.A10;
        }
    }

    private void A10()
    {
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("This is where the demo ends. Thank you for playing!");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Nothing;
        }
    }

    private void A11()
    {
        diaScreenPlayer.SetActive(true);
        diaScreenMark.SetActive(false);
        diaTextPlayer.SetText("Come on man, you gotta tell me something.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.A12;
        }
    }

    private void A12()
    {
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("Get out!!");

        finishedDialogue = true;

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Nothing;
        }
    }

    public void RemoveMark2Trigger()
    {
        triggerObject.SetActive(false);
    }
}