using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Playables;
using UnityTimer;

public class AllDialogue : MonoBehaviour
{
    #region Variables

    [Header("Player")]
    public SpriteRenderer playerSprite;
    public GameObject screenPlayer;
    public TextMeshProUGUI textPlayer;
    public Animator animPlayer;
    public PlayerMovement2DPlatformer playerMovement;
    public Rigidbody2D playerRb;
    public PlayerHealth playerHealth;

    [Header("Mark")]
    public GameObject screenMark;
    public TextMeshProUGUI textMark;
    public Animator animMark;
    public Animator mainAnimMark;
    public GameObject mark;

    public GameObject screenMark2;
    public TextMeshProUGUI textMark2;

    [Header("State Machine")]
    private float cooldown = 0;
    public float textTime;
    public bool finishedDialogue1;
    public bool finishedDialogue2;
    public bool finishedDialogue3;

    public enum State { Nothing, 
                        Ship1, Ship2, Ship3, Ship4, 
                        Nav1, Nav2,
                        Mark1d1, Player1d2, Mark1d3, Player1d4, Mark1d5, Player1d6, Player1d7, Mark1d8, Mark1d9, Mark1d10, Mark1d11,
                        Mark2d1, Mark2d2, Mark2d3, Mark2d4, Player2d5, Mark2d6, Mark2d7, Mark2d8, Mark2d9, Mark2d10, Mark2d11, Mark2d12, Mark2d13,
                        Jump1, Jump2,
                        Mark3d1, Mark3d2, Mark3d3,
                        Chase, ChaseEnd,
                        Player4d1, Mark4d2, Mark4d3, Player4d4,
                        Pod1, Pod2, Pod3, Pod4, Pod5
    }
    public State state = State.Ship1;

    [Header("Trigger Colliders")]
    public GameObject triggerNav;
    public GameObject triggerMark1;
    public GameObject triggerMark2;
    public GameObject triggerMark3;
    public GameObject triggerEndChase;
    public GameObject triggerPod;

    [Header("Other")]
    public GameObject greenCard;
    public GameObject blueCard;

    public PlayableDirector chaseTimeline;
    public PlayableDirector mossterEndBit;
    public Door greydoor1;
    public Door greydoor2;
    public Door greydoor3;
    public Door greydoor4;
    public Door greydoor5;

    public GameObject door1;
    public GameObject door2;

    public PlayerInventory playerInv;
    public string wire1;
    public string wire2;
    public bool wireDone = false;

    public GameObject saveColliders;
    public SaveAndLoad saveLoad;
    public Transform chaseSavePoint;

    public GameObject chaseEnemy;
    public Transform chaseEnemyPos;

    public GameObject timerUI;
    public Timer chaseTimer;
    public TextMeshProUGUI timerText;

    public Timer markTimer;

    public bool markReady = false;
    public bool playerReady = false;

    public GameObject endScreen;

    public bool isChasing = false;

    [Header("Mark Positions")]
    public Transform markPosEvent2;

    public List<Transform> chasePositions;
    [SerializeField] private int currentPoint;

    public float markSpeed = 6;
    [SerializeField] private float markDistance;

    [SerializeField] private bool isMoving;

    #endregion

    void Start()
    {
        screenMark.SetActive(false);
        screenPlayer.SetActive(false);
        triggerNav.SetActive(true);
        triggerMark1.SetActive(true);
        triggerMark2.SetActive(false);
        triggerMark3.SetActive(false);
        triggerPod.SetActive(true);
        finishedDialogue1 = false;
        finishedDialogue2 = false;
        finishedDialogue3 = false;
        mark.SetActive(true);
        playerMovement.enabled = true;
        
        greydoor1.enabled = true;
        greydoor2.enabled = true;

        chaseEnemy.SetActive(false);
        timerUI.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            Transition2to3();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            state = State.Chase;
        }

        if (playerInv.keyItems.ContainsKey(wire1) && playerInv.keyItems.ContainsKey(wire2) && !wireDone)
        {
            wireDone = true;
            Transition2to3();
        }
    }

    private void CheckState()
    {
        switch (state)
        {
            case State.Nothing: Nothing(); break;

            case State.Ship1: Ship1(); break;
            case State.Ship2: Ship2(); break;
            case State.Ship3: Ship3(); break;
            case State.Ship4: Ship4(); break;

            case State.Nav1: Nav1(); break;
            case State.Nav2: Nav2(); break;

            case State.Mark1d1: Mark1d1(); break;
            case State.Player1d2: Player1d2(); break;
            case State.Mark1d3: Mark1d3(); break;
            case State.Player1d4: Player1d4(); break;
            case State.Mark1d5: Mark1d5(); break;
            case State.Player1d6: Player1d6(); break;
            case State.Player1d7: Player1d7(); break;
            case State.Mark1d8: Mark1d8(); break;
            case State.Mark1d9: Mark1d9(); break;
            case State.Mark1d10: Mark1d10(); break;
            case State.Mark1d11: Mark1d11(); break;

            case State.Mark2d1: Mark2d1(); break;
            case State.Mark2d2: Mark2d2(); break;
            case State.Mark2d3: Mark2d3(); break;
            case State.Mark2d4: Mark2d4(); break;
            case State.Player2d5: Player2d5(); break;
            case State.Mark2d6: Mark2d6(); break;
            case State.Mark2d7: Mark2d7(); break;
            case State.Mark2d8: Mark2d8(); break;
            case State.Mark2d9: Mark2d9(); break;
            case State.Mark2d10: Mark2d10(); break;
            case State.Mark2d11: Mark2d11(); break;
            case State.Mark2d12: Mark2d12(); break;
            case State.Mark2d13: Mark2d13(); break;

            case State.Jump1: Jump1(); break;
            case State.Jump2: Jump2(); break;

            case State.Mark3d1: Mark3d1(); break;
            case State.Mark3d2: Mark3d2(); break;
            case State.Mark3d3: Mark3d3(); break;

            case State.Chase: Chase(); break;
            case State.ChaseEnd: ChaseEnd(); break;

            case State.Player4d1: Player4d1(); break;
            case State.Mark4d2: Mark4d2(); break;
            case State.Mark4d3: Mark4d3(); break;
            case State.Player4d4: Player4d4(); break;

            case State.Pod1: Pod1(); break;
            case State.Pod2: Pod2(); break;
            case State.Pod3: Pod3(); break;
            case State.Pod4: Pod4(); break;
            case State.Pod5: Pod5(); break;
        }
    }

    private void Nothing()
    {
        screenMark.SetActive(false);
        screenPlayer.SetActive(false);
        playerMovement.enabled = true;
    }

    #region Ship

    private void Ship1()
    {
        screenPlayer.SetActive(true);
        textPlayer.SetText("What the hell did I just see?");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Ship2;
        }
    }

    private void Ship2()
    {
        screenPlayer.SetActive(true);
        textPlayer.SetText("I'd better find someone and get some answers.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Ship3;
        }
    }

    private void Ship3()
    {
        screenPlayer.SetActive(true);
        textPlayer.SetText("It seems like the gravity generator is no longer working. Luckily I came prepared.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Ship4;
        }
    }

    private void Ship4()
    {
        screenPlayer.SetActive(true);
        textPlayer.SetText("I can use WASD to walk across any surface: floors, walls and ceilings.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Nothing;
        }
    }

    #endregion

    #region Navigation

    public void TriggerNavigation()
    {
        state = State.Nav1;
    }

    private void Nav1()
    {
        screenPlayer.SetActive(true);
        textPlayer.SetText("What is that green stuff? That shouldn't be here.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Nav2;
        }
    }

    private void Nav2()
    {
        screenPlayer.SetActive(true);
        textPlayer.SetText("I'd better avoid it.");

        triggerNav.SetActive(false);

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Nothing;
        }
    }

    #endregion

    #region Mark1

    public void TriggerEvent1()
    {
        if (!finishedDialogue1)
        {
            state = State.Mark1d1;
        }
        else
        {
            state = State.Mark1d11;
        }
    }

    private void Mark1d1()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("STAY BACK! DON'T LOOK AT ME!");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Player1d2;
        }
    }

    private void Player1d2()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark.SetActive(false);
        screenPlayer.SetActive(true);
        textPlayer.SetText("Wow calm down! What's going on here? What happened?");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark1d3;
        }
    }

    private void Mark1d3()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("Oh thank god, you're not one of them.");

        finishedDialogue1 = true;

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Player1d4;
        }
    }

    private void Player1d4()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark.SetActive(false);
        screenPlayer.SetActive(true);
        textPlayer.SetText("One of what?");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark1d5;
        }
    }

    private void Mark1d5()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("You're... Doesn't matter. The name's Mark, I don't think I've seen you around here.");

        finishedDialogue1 = true;

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Player1d6;
        }
    }

    private void Player1d6()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark.SetActive(false);
        screenPlayer.SetActive(true);
        textPlayer.SetText("I'm Robin, I've been sent by HQ to fix your communication unit.");

        finishedDialogue1 = true;

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Player1d7;
        }
    }

    private void Player1d7()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark.SetActive(false);
        screenPlayer.SetActive(true);
        textPlayer.SetText("Could you tell me what happened here?");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark1d8;
        }
    }

    private void Mark1d8()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("There's too much to explain, all you need to know is that we need to get out of here asap.");

        finishedDialogue1 = true;

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark1d9;
        }
    }

    private void Mark1d9()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("They... Those <b>eyes</b> taken over the power station");

        finishedDialogue1 = true;

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark1d10;
        }
    }

    private void Mark1d10()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("We need to get the power going if we want to charge an escape pod and whatever ship you came with.");

        finishedDialogue1 = true;

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark1d11;
        }
    }

    private void Mark1d11()
    {
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("You can probably fix it right? You can find the room if you go south. I'll tell you more once we can see again.");

        finishedDialogue1 = true;

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Nothing;
        }
    }

    #endregion

    public void Transition1to2()
    {
        mark.transform.DOMove(markPosEvent2.position, 0.1f);
        triggerMark1.SetActive(false);
        triggerMark2.SetActive(true);
    }

    #region Mark2

    public void TriggerEvent2()
    {
        if (!finishedDialogue2)
        {
            state = State.Mark2d1;
        }
        else
        {
            state = State.Mark2d13;
        }
    }

    private void Mark2d1()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("You- You actually did it. I didn't expect-");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark2d2;
        }
    }

    private void Mark2d2()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("I mean, good job! Now we just have to fix navigation's computer and type in the coordinates to HQ.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark2d3;
        }
    }

    private void Mark2d3()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("Take this keycard. It should open up the eastern wing.");

        greenCard.SetActive(true);

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark2d4;
        }
    }

    private void Mark2d4()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("Go to storage and find the wires needed to fix the computer. The sooner it's done, the faster we can go.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Player2d5;
        }
    }

    private void Player2d5()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark.SetActive(false);
        screenPlayer.SetActive(true);
        textPlayer.SetText("Alright, but don't you owe me an explanation.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark2d6;
        }
    }

    private void Mark2d6()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("Fine. I'm part of the crew that was sent to Argus 108 to study the supposed lifeforms on its outer layer.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark2d7;
        }
    }

    private void Mark2d7()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("We took back some sample tissues, but something happened overnight.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark2d8;
        }
    }

    private void Mark2d8()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("The substance started... growing, at rapid speed.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark2d9;
        }
    }

    private void Mark2d9()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("It spread on chairs, walls, even my crewmembers...");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark2d10;
        }
    }

    private void Mark2d10()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("But that's not all. They started sprouting... <b>eyes</b>.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark2d11;
        }
    }

    private void Mark2d11()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("If they look at you, you become entranced and they'll get you too.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark2d12;
        }
    }

    private void Mark2d12()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("Don't look at anything that's green.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark2d13;
        }
    }

    private void Mark2d13()
    {
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("Now go to storage in the eastern wing, before it's too late...");

        finishedDialogue2 = true;

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Nothing;
        }
    }

    #endregion

    #region JumpUnlock

    public void ActivateJumpText()
    {
        state = State.Jump1;
    }

    private void Jump1()
    {
        screenPlayer.SetActive(true);
        textPlayer.SetText("Oh wow, some spare booster boots.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Jump2;
        }
    }

    private void Jump2()
    {
        screenPlayer.SetActive(true);
        textPlayer.SetText("I can use these to jump by pressing the Spacebar.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Nothing;
        }
    }

    #endregion

    public void Transition2to3()
    {
        //move mark
        triggerMark2.SetActive(false);
        triggerMark3.SetActive(true);
    }

    #region Mark3

    public void TriggerEvent3()
    {
        if (!finishedDialogue3)
        {
            state = State.Mark3d1;
            triggerMark3.SetActive(false);
        }
    }

    private void Mark3d1()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        playerSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("You got the wires? You're a legend, bud!");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark3d2;
        }
    }

    private void Mark3d2()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("Now all we have to do is-");

        chaseEnemy.SetActive(true);
        chaseTimeline.Play();

        if (cooldown >= 3)
        {
            cooldown = 0;
            state = State.Mark3d3;
        }
    }

    private void Mark3d3()
    {
        playerMovement.enabled = true;
        animPlayer.SetBool("Walking", false);
        screenMark.SetActive(true);
        screenPlayer.SetActive(false);
        textMark.SetText("OH FUCK RUN!!!");

        if (cooldown >= 1)
        {
            cooldown = 0;
            ActivateChase();
        }
    }

    #endregion

    #region Chase

    public void ActivateChase()
    {
        saveColliders.SetActive(false);
        saveLoad.SavePosition(chaseSavePoint);
        screenMark.SetActive(false);
        triggerEndChase.SetActive(true);

        state = State.Chase;

        greydoor1.closesAuto = false;
        greydoor2.closesAuto = false;
        greydoor3.closesAuto = false;
        greydoor4.closesAuto = false;
        greydoor5.closesAuto = false;

        chaseTimer = Timer.Register(45f, ChaseFail);
        markTimer = Timer.Register(33.2f, PutMarkReady);
        timerUI.SetActive(true);
        isChasing = true;
    }

    private void Chase()
    {
        float timeLeft = chaseTimer.GetTimeRemaining();
        timerText.SetText("" + timeLeft.ToString("#.00"));
    }

    public void TriggerEndChase()
    {
        state = State.ChaseEnd;
        chaseTimer.Cancel();
        timerUI.SetActive(false);
    }

    private void ChaseEnd()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        playerReady = true;

        if (playerReady && markReady)
        {
            cooldown = 0;
            state = State.Player4d1;
        }
    }

    private void Player4d1()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark.SetActive(false);
        screenPlayer.SetActive(true);
        textPlayer.SetText("What the fuck Mark?");

        mossterEndBit.Play();

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark4d2;
        }
    }

    private void Mark4d2()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark2.SetActive(true);
        screenPlayer.SetActive(false);
        textMark2.SetText("I'm sorry bud, ship's crowded enough as is.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Mark4d3;
        }
    }

    private void Mark4d3()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark2.SetActive(true);
        screenPlayer.SetActive(false);
        textMark2.SetText("Good luck.");

        //mark take-off

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Player4d4;
        }
    }

    private void Player4d4()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        screenMark2.SetActive(false);
        screenPlayer.SetActive(true);
        textPlayer.SetText("Shit... Wait is that a keycard?");

        blueCard.SetActive(true);

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Nothing;
        }
    }

    public void ChaseFail()
    {
        isChasing = false;
        playerHealth.ObstacleDeath();
        chaseTimeline.Stop();
        mossterEndBit.Stop();
        chaseTimer.Cancel();
        markTimer.Cancel();
        timerUI.SetActive(false);
        markReady = false;
        mark.transform.DOMove(markPosEvent2.position, 0.1f);
        finishedDialogue3 = true;
        triggerMark3.SetActive(false);
        state = State.Mark3d1;
    }

    private void PutMarkReady()
    {
        markReady = true;
        door1.SetActive(true);
        door2.SetActive(true);
    }

    #endregion

    #region EscapePod

    public void TriggerEscapePod()
    {
        state = State.Pod1;
    }

    private void Pod1()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenPlayer.SetActive(true);
        textPlayer.SetText("Oh my god...");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Pod2;
        }
    }

    private void Pod2()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenPlayer.SetActive(true);
        textPlayer.SetText("Fuck that guy.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Pod3;
        }
    }

    private void Pod3()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenPlayer.SetActive(true);
        textPlayer.SetText("It looks like this escape pod is missing a couple of things.");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Pod4;
        }
    }

    private void Pod4()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenPlayer.SetActive(true);
        textPlayer.SetText("Maybe I can fix it up and get out?");

        if (cooldown >= textTime || Input.GetKeyDown(KeyCode.E))
        {
            cooldown = 0;
            state = State.Pod5;
        }
    }

    private void Pod5()
    {
        playerMovement.enabled = false;
        playerRb.velocity = new Vector2(0, 0);
        animPlayer.SetBool("Walking", false);
        screenPlayer.SetActive(false);
        endScreen.SetActive(true);
    }

    #endregion
}
