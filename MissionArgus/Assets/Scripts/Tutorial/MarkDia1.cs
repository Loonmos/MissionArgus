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

    public enum State {Nothing, IntroMark1, ReplyMark, IntroMark2, IntroMark3, IntroMark4, MoveMark1, TPMark1, MoveMark2, Expo1, Expo2, Expo3, Expo4, Expo5, MoveMark3, TPMark2, Death1, Death2, Death3, 
        Death4, Jump1, Jump2, End }
    public State state = State.Nothing;

    private float cooldown = 0;
    public float animTime;
    public float textTime;

    public PlayerMovement2DPlatformer playerMovement;

    public Collider2D diaTrigger;
    
    void Start()
    {
        diaScreenMark.SetActive(false);
        diaScreenPlayer.SetActive(false);
    }

    
    void Update()
    {
        CheckState();

        if (state != State.End)
        {
            cooldown += Time.deltaTime;
        }
    }

    private void CheckState()
    {
        switch (state)
        {
            case State.Nothing: Nothing(); break;
            case State.IntroMark1: IntroMark1(); break;
            case State.ReplyMark: ReplyMark(); break;
            case State.IntroMark2: IntroMark2(); break;
            case State.IntroMark3: IntroMark3(); break;
            case State.IntroMark4: IntroMark4(); break;
            case State.MoveMark1: MoveMark1(); break;
            case State.TPMark1: TPMark1(); break;
            case State.MoveMark2: MoveMark2(); break;
            case State.Expo1: Expo1(); break;
            case State.Expo2: Expo2(); break;
            case State.Expo3: Expo3(); break;
            case State.Expo4: Expo4(); break;
            case State.Expo5: Expo5(); break;
            case State.MoveMark3: MoveMark3(); break;
            case State.TPMark2: TPMark2(); break;
            case State.Death1: Death1(); break;
            case State.Death2: Death2(); break;
            case State.Death3: Death3(); break;
            case State.Death4: Death4(); break;
            case State.Jump1: Jump1(); break;
            case State.Jump2: Jump2(); break;
            case State.End: End(); break;
        }
    }

    private void Nothing()
    {
        playerMovement.enabled = true;
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(false);
        cooldown = 0;
    }

    private void IntroMark1()
    {
        playerMovement.enabled = false;
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("HEY, WHAT ARE YOU STILL DOING HERE?");
        //if (!markSound.isPlaying)
        //{
        //    markSound.Play();
        //}

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.ReplyMark;
        }
    }

    private void ReplyMark()
    {
        diaScreenMark.SetActive(false);
        diaScreenPlayer.SetActive(true);
        diaTextPlayer.SetText("MARK?");
        //playerSound.Play();

        if (cooldown >= animTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.IntroMark2;
        }
    }

    private void IntroMark2()
    {
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("COME, WE NEED TO GET OUT OF HERE");
        //markSound.Play();

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.IntroMark3;
        }
    }

    private void IntroMark3()
    {
        playerMovement.enabled = true;
        diaTextMark.SetText("USE A AND D TO MOVE");
        //markSound.Play();

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.IntroMark4;
        }
    }

    private void IntroMark4()
    {
        playerMovement.enabled = true;
        diaTextMark.SetText("USE T TO TURN YOUR CONTROLS GUIDE ON AND OFF");
        //markSound.Play();

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.MoveMark1;
        }
    }

    private void MoveMark1()
    {
        diaScreenMark.SetActive(false);
        animMarkMove.SetBool("move1", true);
        animMark.SetBool("Walking", true);

        if (cooldown >= animTime)
        {
            cooldown = 0;
            state = State.TPMark1;
        }
    }

    private void TPMark1()
    {
        animMark.SetBool("Walking", false);
        animMarkMove.SetBool("move1", false);
        animMarkMove.SetBool("tp1", true);

        if (cooldown >= animTime)
        {
            cooldown = 0;
            state = State.End;
        }
    }

    private void MoveMark2()
    {
        // activate with collider
        animMarkMove.SetBool("tp1", false);
        animMark.SetBool("Walking", true);
        animMarkMove.SetBool("move2", true);

        if (cooldown >= animTime)
        {
            animMarkMove.SetBool("move2", false);
            cooldown = 0;
            state = State.End;
        }
    }

    private void Expo1()
    {
        // activate with collider
        animMark.SetBool("Walking", false);
        animPlayer.SetBool("Walking", false);
        playerMovement.enabled = false;
        playerMovement.rb.velocity = new Vector2(0, 0);
        diaScreenPlayer.SetActive(true);
        diaTextPlayer.SetText("TELL ME WHAT'S GOING ON");
        //playerSound.Play();

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("trigger expo2");
            cooldown = 0;
            state = State.Expo2;
        }
    }

    private void Expo2()
    {
        diaScreenPlayer.SetActive(false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("LOOK, THE MOON WE'RE INVESTIGATING");
        //markSound.Play();

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Expo3;
        }
    }

    private void Expo3()
    {
        diaTextMark.SetText("IT'S GOT EYES...");
        //markSound.Play();

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Expo4;
        }
    }

    private void Expo4()
    {
        diaTextMark.SetText("AND WHEN THEY LOOK AT YOU, YOU LOSE YOUR MIND");
        //markSound.Play();

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Expo5;
        }
    }

    private void Expo5()
    {
        diaTextMark.SetText("WE NEED TO GET TO THE ESCAPE PODS AT THE TOP OF THE SHIP");
        //markSound.Play();

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.MoveMark3;
        }
    }

    private void MoveMark3()
    {
        animMark.SetBool("Walking", true);
        animMarkMove.SetBool("move3", true);
        diaScreenMark.SetActive(false);

        if (cooldown >= animTime)
        {
            cooldown = 0;
            state = State.TPMark2;
        }
    }

    private void TPMark2()
    {
        animMark.SetBool("Walking", false);
        animMarkMove.SetBool("move3", false);
        animMarkMove.SetBool("tp2", true);
        playerMovement.enabled = true;

        if (cooldown >= animTime)
        {
            cooldown = 0;
            state = State.End;
        }
    }

    private void Death1()
    {
        // activate with collider
        playerMovement.enabled = false;
        animPlayer.SetBool("Jumping", false);
        animMarkMove.SetBool("tp2", false);
        animMarkMove.SetBool("move4", true);
        animMark.SetBool("Jumping", true);

        if (cooldown >= animTime)
        {
            cooldown = 0;
            state = State.Death2;
        }
    }

    private void Death2()
    {
        animMark.SetBool("Jumping", false);
        diaScreenMark.SetActive(true);
        diaTextMark.SetText("SHIT SHIT SHIT");
        //markSound.Play();

        if (cooldown >= animTime)
        {
            cooldown = 0;
            state = State.Death3;
        }
    }

    private void Death3()
    {
        animMarkMove.SetBool("move4", false);
        diaScreenMark.SetActive(false);
        animMarkMove.SetBool("move5", true);
        animMark.SetBool("Walking", true);
        openEye.OpenTheEye();

        if (cooldown >= animTime)
        {
            cooldown = 0;
            state = State.Death4;
        }
    }

    private void Death4()
    {
        playerMovement.enabled = true;
        animMarkMove.SetBool("move5", false);
        animMark.SetBool("Walking", false);
        animMarkMove.SetBool("death", true);
        animMark.Play("Death");

        if (cooldown >= animTime)
        {
            cooldown = 0;
            state = State.End;
        }
    }

    private void Jump1()
    {
        //activate when get ability
        diaScreenPlayer.SetActive(true);
        diaTextPlayer.SetText("I CAN USE THESE TO JUMP");
        //playerSound.Play();

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Jump2;
        }
    }

    private void Jump2()
    {
        diaTextPlayer.SetText("PRESS SPACEBAR TO JUMP");
        //playerSound.Play();

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.End;
        }
    }

    private void End()
    {
        diaScreenMark.SetActive(false);
        diaScreenPlayer.SetActive(false);
        cooldown = 0;
    }
}
