using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DIRECTION
{
    RIGHT,
    LEFT,
    UP,
    DOWN,
    NONE
}
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
    [SerializeField] DIRECTION holdDirection, groundedDirection, prevGroundedDirection, prevInputDirection;

    [SerializeField] float horizontalInput;
    [SerializeField] float verticalInput;
    [SerializeField] bool usingPreviousInput;

    public float speed = 0.03f;
    public float jumpForce = 0.06f;
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
    public GameObject guideTextA;
    bool respawning, resettingSpeed;

    private void Start()
    {
        groundedDirection = DIRECTION.DOWN;
    }

    void Update()
    {
        GetInputs();
        CheckGround();
        ChangeGravity();
        FlipSprite();
        Animations();

        //horizontalDown = Input.GetAxisRaw("HorizontalDown");
        //horizontalUp = Input.GetAxisRaw("HorizontalUp");
        //verticalRight = Input.GetAxisRaw("VerticalRight");
        //verticalLeft = Input.GetAxisRaw("VerticalLeft");

        if (Input.GetKeyDown(KeyCode.Space) && unlockedJump)
        {
            Jump();
        }

        if (groundedDirection != DIRECTION.NONE)
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

        if (groundedDirection == DIRECTION.DOWN && groundedDirection == DIRECTION.DOWN || groundedDirection == DIRECTION.DOWN && groundedDirection == DIRECTION.RIGHT 
            || groundedDirection == DIRECTION.UP && groundedDirection == DIRECTION.LEFT || groundedDirection == DIRECTION.UP && groundedDirection == DIRECTION.RIGHT)
        {
            doubleGrounded = true;
        }
        else
        {
            doubleGrounded = false;
        }
    }

    public IEnumerator LowerSpeedTemporarily()
    {
        if (!resettingSpeed)
        {
            resettingSpeed = true;
            float prevSpeed = speed;
            speed = 0;
            yield return new WaitForSeconds(0.5f);
            speed = prevSpeed;
            resettingSpeed = false;
        }
    }

    public void ResetMovement()
    {
        usingPreviousInput = false;
        prevInputDirection = DIRECTION.NONE;
        prevGroundedDirection = DIRECTION.DOWN;
        holdDirection = DIRECTION.NONE;
        horizontalInput = 0;
        verticalInput = 0;
        respawning = true;
    }

    void GetInputs()
    {
        //MAKE IT SO IT CONTINUES USING THE INPUT FROM BEING ON THE OTHER WALL GALAXY STYLE
        if(usingPreviousInput)
        {
            if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                ResetMovement();
            }

            if(groundedDirection == DIRECTION.LEFT)
            {
                if(prevGroundedDirection == DIRECTION.UP)
                {
                    if (prevInputDirection == DIRECTION.LEFT) verticalInput = -1; holdDirection = DIRECTION.DOWN;
                }
                else if (prevGroundedDirection == DIRECTION.DOWN)
                {
                    if (prevInputDirection == DIRECTION.LEFT) verticalInput = 1; holdDirection = DIRECTION.UP;
                }
            }
            else if (groundedDirection == DIRECTION.RIGHT)
            {
                if (prevGroundedDirection == DIRECTION.UP)
                {
                    if (prevInputDirection == DIRECTION.RIGHT) verticalInput = -1; holdDirection = DIRECTION.DOWN;
                }
                else if (prevGroundedDirection == DIRECTION.DOWN)
                {
                    if (prevInputDirection == DIRECTION.RIGHT) verticalInput = 1; holdDirection = DIRECTION.UP;
                }
            }
            else if (groundedDirection == DIRECTION.DOWN)
            {
                if (prevGroundedDirection == DIRECTION.LEFT)
                {
                    if (prevInputDirection == DIRECTION.DOWN) horizontalInput = 1; holdDirection = DIRECTION.RIGHT;
                }
                else if (prevGroundedDirection == DIRECTION.RIGHT)
                {
                    if (prevInputDirection == DIRECTION.DOWN) horizontalInput = -1; holdDirection = DIRECTION.LEFT;
                }
            }
            else if (groundedDirection == DIRECTION.UP)
            {
                if (prevGroundedDirection == DIRECTION.LEFT)
                {
                    if (prevInputDirection == DIRECTION.UP) horizontalInput = 1; holdDirection = DIRECTION.RIGHT;
                }
                else if (prevGroundedDirection == DIRECTION.RIGHT)
                {
                    if (prevInputDirection == DIRECTION.UP) horizontalInput = -1; holdDirection = DIRECTION.LEFT;
                }
            }

            return;
        }

        if (groundedDirection == DIRECTION.DOWN || groundedDirection == DIRECTION.UP)
        {
            //if (Input.GetKey(KeyCode.W)) { holdDirection = HOLD_DIRECTION.RIGHT; }
            //else if (Input.GetKey(KeyCode.S)) { holdDirection = HOLD_DIRECTION.LEFT; }
            if (Input.GetKey(KeyCode.A)) { holdDirection = DIRECTION.LEFT; }
            else if (Input.GetKey(KeyCode.D)) { holdDirection = DIRECTION.RIGHT; }
            else holdDirection = DIRECTION.NONE;

            if(holdDirection == DIRECTION.LEFT) { horizontalInput = -1; }
            else if(holdDirection == DIRECTION.RIGHT) { horizontalInput = 1;  }
            else if(holdDirection == DIRECTION.NONE) { horizontalInput = 0;  }
        }
        if(groundedDirection == DIRECTION.LEFT || groundedDirection == DIRECTION.RIGHT)
        {
            if (Input.GetKey(KeyCode.W)) { holdDirection = DIRECTION.UP; }
            else if (Input.GetKey(KeyCode.S)) { holdDirection = DIRECTION.DOWN; }
            //else if (Input.GetKey(KeyCode.A)) { holdDirection = HOLD_DIRECTION.UP; }
            //else if (Input.GetKey(KeyCode.D)) { holdDirection = HOLD_DIRECTION.DOWN; }
            else holdDirection = DIRECTION.NONE;

            if(holdDirection == DIRECTION.DOWN) { verticalInput = -1; }
            else if(holdDirection == DIRECTION.UP) { verticalInput = 1; }
            else if(holdDirection == DIRECTION.NONE) { verticalInput = 0; }
        }
    }

    private void FixedUpdate() // movement
    {
        if (groundedDirection == DIRECTION.DOWN)
        {
            health = playerHealth.currentHealth;
            if (doubleGrounded || isJumping)
            {
                rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(horizontalInput * speed, 0);
            }
        }

        if (groundedDirection == DIRECTION.UP)
        {
            health = playerHealth.currentHealth;
            if (doubleGrounded || isJumping)
            {
                rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(horizontalInput * speed, 0);
            }
        }

        if (groundedDirection == DIRECTION.RIGHT)
        {
            health = playerHealth.currentHealth;
            if (doubleGrounded || isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, verticalInput * speed);
            }
            else
            {
                rb.velocity = new Vector2(0, verticalInput * speed);
            }
        }

        if (groundedDirection == DIRECTION.LEFT)
        {
            health = playerHealth.currentHealth;
            if (doubleGrounded || isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, verticalInput * speed);
            }
            else
            {
                rb.velocity = new Vector2(0, verticalInput * speed);

            }
        }
    }

    private void CheckGround()
    {
        // do this with raycasts
        bool _grounded = false;
        // check up
        if (Physics2D.OverlapCircle(groundCheckUp1.position, groundDistance, groundLayer))
        {
            _grounded = true;
            if (respawning || ((holdDirection == DIRECTION.UP || !isGrounded) && groundedDirection != DIRECTION.UP))
            {
                if(groundedDirection != DIRECTION.NONE) prevGroundedDirection = groundedDirection;
                prevInputDirection = holdDirection;
                groundedDirection = DIRECTION.UP;
                if (Input.GetKey(KeyCode.W)) usingPreviousInput = true;
                respawning = false;
                return;
            }
        }

        // check down
        if (Physics2D.OverlapCircle(groundCheckDown1.position, groundDistance, groundLayer))
        {
            _grounded = true;
            if (respawning || ((holdDirection == DIRECTION.DOWN || !isGrounded) && groundedDirection != DIRECTION.DOWN))
            {
                if (groundedDirection != DIRECTION.NONE) prevGroundedDirection = groundedDirection;
                prevInputDirection = holdDirection;
                groundedDirection = DIRECTION.DOWN;
                if (Input.GetKey(KeyCode.S)) usingPreviousInput = true;
                respawning = false;
                return;
            }
        }

        // check left
        if (Physics2D.OverlapCircle(groundCheckLeft1.position, groundDistance, groundLayer))
        {
            _grounded = true;
            if (respawning || ((holdDirection == DIRECTION.LEFT || !isGrounded) && groundedDirection != DIRECTION.LEFT))
            {
                if (groundedDirection != DIRECTION.NONE) prevGroundedDirection = groundedDirection;
                prevInputDirection = holdDirection;
                groundedDirection = DIRECTION.LEFT;
                if(Input.GetKey(KeyCode.A)) usingPreviousInput = true;
                respawning = false;
                return;
            }
        }

        //check right
        if (Physics2D.OverlapCircle(groundCheckRight1.position, groundDistance, groundLayer))
        {
            _grounded = true;
            if (respawning || ((holdDirection == DIRECTION.RIGHT || !isGrounded) && groundedDirection != DIRECTION.RIGHT))
            {
                if (groundedDirection != DIRECTION.NONE) prevGroundedDirection = groundedDirection;
                prevInputDirection = holdDirection;
                groundedDirection = DIRECTION.RIGHT;
                if (Input.GetKey(KeyCode.D)) usingPreviousInput = true;
                respawning = false;
                return;
            }
        }

        if (!_grounded)
        {
            if(groundedDirection == DIRECTION.LEFT) prevGroundedDirection = DIRECTION.RIGHT;
            if (groundedDirection == DIRECTION.RIGHT) prevGroundedDirection = DIRECTION.LEFT;
            if (groundedDirection == DIRECTION.UP) prevGroundedDirection = DIRECTION.DOWN;
            if (groundedDirection == DIRECTION.DOWN) prevGroundedDirection = DIRECTION.UP;
            groundedDirection = DIRECTION.NONE;
        }
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

        //if (!alreadyGrounded)
        //{
            if (groundedDirection == DIRECTION.DOWN)
            {
                yAxisMove = false;
                xAxisMove = true;
                spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 0);
                guideTextD.transform.rotation = Quaternion.Euler(0, 0, 0);
                guideTextA.transform.rotation = Quaternion.Euler(0, 0, 0);
            //spriteRenderer.flipY = false;
        }
            
            if (groundedDirection == DIRECTION.UP)
            {
                yAxisMove = false;
                xAxisMove = true;
                spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 180);
                guideTextD.transform.rotation = Quaternion.Euler(0, 0, 0);
                guideTextA.transform.rotation = Quaternion.Euler(0, 0, 0);
            //spriteRenderer.flipY = true;
        }

            if (groundedDirection == DIRECTION.RIGHT)
            {
                xAxisMove = false;
                yAxisMove = true;
                spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 90);
                guideTextD.transform.rotation = Quaternion.Euler(0, 0, 0);
                guideTextA.transform.rotation = Quaternion.Euler(0, 0, 0);
            //spriteRenderer.flipY = false;
        }

            if (groundedDirection == DIRECTION.LEFT)
            {
                xAxisMove = false;
                yAxisMove = true;
                spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 270);
                guideTextD.transform.rotation = Quaternion.Euler(0, 0, 0);
                guideTextA.transform.rotation = Quaternion.Euler(0, 0, 0);
            //spriteRenderer.flipY = false;
        }
        //}
    }

    private void FlipSprite() // flip colliders as well
    {
        //DOESNT FLIP CORRECTLY WHEN UPSIDE DOWN AND ON ONE OF THE WALLS
        if (isGrounded)
        {
            if(groundedDirection == DIRECTION.UP) 
            {
                if (Input.GetKey(KeyCode.A))
                {
                    spriteRenderer.flipX = false;
                    colliders.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    spriteRenderer.flipX = true;
                    colliders.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            else if (groundedDirection == DIRECTION.DOWN)
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
            else if (groundedDirection == DIRECTION.LEFT)
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
            else if (groundedDirection == DIRECTION.RIGHT)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    spriteRenderer.flipX = false;
                    colliders.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    spriteRenderer.flipX = true;
                    colliders.transform.localScale = new Vector3(-1, 1, 1);
                }
            }
        }
    }

    private void Jump() // tried to make slower jump
    {
        if (groundedDirection == DIRECTION.DOWN)
        {
            isJumping = true;
            health = playerHealth.currentHealth;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            alreadyGrounded = true;
        }

        if (groundedDirection == DIRECTION.UP)
        {
            isJumping = true;
            health = playerHealth.currentHealth;
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
            alreadyGrounded = true;
        }

        if (groundedDirection == DIRECTION.RIGHT)
        {
            isJumping = true;
            health = playerHealth.currentHealth;
            rb.velocity = new Vector2(-jumpForce, rb.velocity.y);
            alreadyGrounded = true;
        }

        if (groundedDirection == DIRECTION.LEFT)
        {
            isJumping = true;
            health = playerHealth.currentHealth;
            rb.velocity = new Vector2(jumpForce, rb.velocity.y);
            alreadyGrounded = true;
        }
    }

    private void Animations()
    {
        if (isGrounded)
        {
            anim.SetBool("Jumping", false);
        }
        else
        {
            anim.SetBool("Jumping", true);
            anim.SetBool("Walking", false);
        }

        if (groundedDirection == DIRECTION.DOWN && rb.velocity.x > velocityForAnim || groundedDirection == DIRECTION.DOWN && rb.velocity.x < -velocityForAnim 
            || groundedDirection == DIRECTION.UP && rb.velocity.x > velocityForAnim 
            || groundedDirection == DIRECTION.UP && rb.velocity.x < -velocityForAnim)
        {
            anim.SetBool("Walking", true);
        }
        else if (groundedDirection == DIRECTION.RIGHT && rb.velocity.y > velocityForAnim || groundedDirection == DIRECTION.RIGHT && rb.velocity.y < -velocityForAnim 
            || groundedDirection == DIRECTION.LEFT && rb.velocity.y > velocityForAnim
            || groundedDirection == DIRECTION.LEFT && rb.velocity.y < -velocityForAnim)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }
}
