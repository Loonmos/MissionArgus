using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement2DPlatformer : MonoBehaviour
{
    public Rigidbody2D rb;

    public Transform groundCheckUp1;
    public Transform groundCheckUp2;

    public Transform groundCheckDown1;
    public Transform groundCheckDown2;

    public Transform groundCheckLeft1;
    public Transform groundCheckLeft2;

    public Transform groundCheckRight1;
    public Transform groundCheckRight2;

    public LayerMask groundLayer;

    private float horizontalDown;
    private float horizontalUp;
    private float verticalRight;
    private float verticalLeft;
    public float speed = 0.03f;
    public float jumpForce = 0.06f;
    [SerializeField] private bool isGroundedUp;
    [SerializeField] private bool isGroundedDown;
    [SerializeField] private bool isGroundedLeft;
    [SerializeField] private bool isGroundedRight;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool doubleGrounded;
    public float groundDistance = 0.15f;

    [SerializeField] private bool xAxisMove;
    [SerializeField] private bool yAxisMove;

    public SpriteRenderer spriteRenderer;

    public PlayerHealth playerHealth;
    private int health;

    [SerializeField] private bool alreadyGrounded = true;
    public float changePeriod = 0.5f;

    public GameObject colliders;

    public Animator anim;
    public float velocityForAnim = 0.1f;

    public bool unlockedJump = false;
    [SerializeField] private bool isJumping;

    public WalkSounds walkSounds;
    public float walkSoundDelay;
    public AudioSource jumpSound;

    public GameObject guideTextD;
    public TextMeshProUGUI textD;
    public GameObject guideTextA;
    public TextMeshProUGUI textA;

    void Update()
    {
        CheckGround();
        ChangeGravity();
        FlipSprite();
        Animations();

        horizontalDown = Input.GetAxisRaw("HorizontalDown");
        horizontalUp = Input.GetAxisRaw("HorizontalUp");
        verticalRight = Input.GetAxisRaw("VerticalRight");
        verticalLeft = Input.GetAxisRaw("VerticalLeft");

        if (Input.GetKeyDown(KeyCode.Space) && unlockedJump)
        {
            Jump();
        }

        if (isGroundedDown || isGroundedLeft || isGroundedRight || isGroundedUp)
        {
            isGrounded = true;
            if ((Input.GetKey("a") || Input.GetKey("d")) && !walkSounds.playing)
            {
                walkSounds.Play(walkSoundDelay);
            }
            if (!Input.GetKey("d") && !Input.GetKey("a") || !isGrounded)
            {
                walkSounds.Stop();
            }
        }
        else
        {
            isGrounded = false;
            walkSounds.Stop();
        }

        if (isGroundedDown && isGroundedLeft || isGroundedDown && isGroundedRight || isGroundedUp && isGroundedLeft || isGroundedUp && isGroundedRight)
        {
            doubleGrounded = true;
        }
        else
        {
            doubleGrounded = false;
        }
    }

    private void FixedUpdate() // movement
    {
        if (isGroundedDown)
        {
            health = playerHealth.currentHealth;
            if (doubleGrounded || isJumping)
            {
                rb.velocity = new Vector2(horizontalDown * speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(horizontalDown * speed, 0);
            }
        }

        if (isGroundedUp)
        {
            health = playerHealth.currentHealth;
            if (doubleGrounded || isJumping)
            {
                rb.velocity = new Vector2(horizontalUp * speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(horizontalUp * speed, 0);
            }
        }

        if (isGroundedRight)
        {
            health = playerHealth.currentHealth;
            if (doubleGrounded || isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, verticalRight * speed);
            }
            else
            {
                rb.velocity = new Vector2(0, verticalRight * speed);
            }
        }

        if (isGroundedLeft)
        {
            health = playerHealth.currentHealth;
            if (doubleGrounded || isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, verticalLeft * speed);
            }
            else
            {
                rb.velocity = new Vector2(0, verticalLeft * speed);

            }
        }
    }

    private void CheckGround()
    {
        // do this with raycasts

        // check up
        isGroundedUp = Physics2D.OverlapCircle(groundCheckUp1.position, groundDistance, groundLayer);

        // check down
        isGroundedDown = Physics2D.OverlapCircle(groundCheckDown1.position, groundDistance, groundLayer);

        // check left
        isGroundedLeft = Physics2D.OverlapCircle(groundCheckLeft1.position, groundDistance, groundLayer);

        //check right
        isGroundedRight = Physics2D.OverlapCircle(groundCheckRight1.position, groundDistance, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            jumpSound.Play();
            StartCoroutine(ChangeTime());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            alreadyGrounded = false;
        }
    }

    IEnumerator ChangeTime()
    {
        alreadyGrounded = false;
        isJumping = true;

        yield return new WaitForSeconds(changePeriod);

        alreadyGrounded = true;
        isJumping = false;
    }

    private void ChangeGravity()
    {
        //set rotation and set x or y axis movement

        if (isGroundedDown)
        {
            yAxisMove = false;
            xAxisMove = true;
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
            guideTextD.transform.rotation = Quaternion.Euler(0, 0, 0);
            //textD.SetText("D");
            guideTextA.transform.rotation = Quaternion.Euler(0, 0, 0);
            //textA.SetText("A");
        }

        if (isGroundedUp)
        {
            yAxisMove = false;
            xAxisMove = true;
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 180);
            guideTextD.transform.rotation = Quaternion.Euler(0, 0, 0);
            //textD.SetText("A");
            guideTextA.transform.rotation = Quaternion.Euler(0, 0, 0);
            //textA.SetText("D");
        }

        if (isGroundedRight)
        {
            xAxisMove = false;
            yAxisMove = true;
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 90);
            guideTextD.transform.rotation = Quaternion.Euler(0, 0, 0);
            //            textD.SetText("W");
            guideTextA.transform.rotation = Quaternion.Euler(0, 0, 0);
            //            textA.SetText("S");
        }

        if (isGroundedLeft)
        {
            xAxisMove = false;
            yAxisMove = true;
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 270);
            guideTextD.transform.rotation = Quaternion.Euler(0, 0, 0);
            //            textD.SetText("S");
            guideTextA.transform.rotation = Quaternion.Euler(0, 0, 0);
            //            textA.SetText("W");
        }
    }

    private void FlipSprite() // flip colliders as well
    {
        if (isGroundedDown)
        {
            if (Input.GetKey(KeyCode.A))
            {
                spriteRenderer.flipX = true;
                colliders.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                spriteRenderer.flipX = false;
                colliders.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (isGroundedUp)
        {
            if (Input.GetKey(KeyCode.D))
            {
                spriteRenderer.flipX = true;
                colliders.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                spriteRenderer.flipX = false;
                colliders.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (isGroundedLeft)
        {
            if (Input.GetKey(KeyCode.W))
            {
                spriteRenderer.flipX = true;
                colliders.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                spriteRenderer.flipX = false;
                colliders.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (isGroundedRight)
        {
            if (Input.GetKey(KeyCode.S))
            {
                spriteRenderer.flipX = true;
                colliders.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                spriteRenderer.flipX = false;
                colliders.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void Jump() // tried to make slower jump
    {
        if (isGroundedDown)
        {
            isJumping = true;
            health = playerHealth.currentHealth;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            alreadyGrounded = true;
        }

        if (isGroundedUp)
        {
            isJumping = true;
            health = playerHealth.currentHealth;
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
            alreadyGrounded = true;
        }

        if (isGroundedRight)
        {
            isJumping = true;
            health = playerHealth.currentHealth;
            rb.velocity = new Vector2(-jumpForce, rb.velocity.y);
            alreadyGrounded = true;
        }

        if (isGroundedLeft)
        {
            isJumping = true;
            health = playerHealth.currentHealth;
            rb.velocity = new Vector2(jumpForce, rb.velocity.y);
            alreadyGrounded = true;
        }
    }

    private void Animations()
    {
        if (isGroundedDown || isGroundedLeft || isGroundedRight || isGroundedUp)
        {
            anim.SetBool("Jumping", false);
        }
        else
        {
            anim.SetBool("Jumping", true);
            anim.SetBool("Walking", false);
        }

        if (isGroundedDown && rb.velocity.x > velocityForAnim || isGroundedDown && rb.velocity.x < -velocityForAnim || isGroundedUp && rb.velocity.x > velocityForAnim
            || isGroundedUp && rb.velocity.x < -velocityForAnim)
        {
            anim.SetBool("Walking", true);
        }
        else if (isGroundedRight && rb.velocity.y > velocityForAnim || isGroundedRight && rb.velocity.y < -velocityForAnim || isGroundedLeft && rb.velocity.y > velocityForAnim
            || isGroundedLeft && rb.velocity.y < -velocityForAnim)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }
}
