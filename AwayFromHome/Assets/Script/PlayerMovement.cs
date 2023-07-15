using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Fields
    Rigidbody2D rb;
    Animator animator;

    //Ground
    const float OVERHEAD_CHECK_RADIOUS = 0.1f;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] Collider2D standingCollider, crouchingCollider;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] Transform overheadCheckCollider;
    float groundCheckRadius = 0.3f;
    bool isGrounded = true;

    //Jump
    [SerializeField] float jumpPower = 11;
    [SerializeField] float jumpDuration = 0.1f;
    bool isJumping = false;
    float jumpTimer = 0;

    //Crouch
    bool isCrouching;
    float CrouchSpeedModifier = 0.5f;

    //Moving
    [SerializeField] float speed = 210f;
    float horizontalMove;
    bool facingRight = true;

    //Paused
    bool isPaused = false;

    //Hurt
    bool hurt = false;
    float hurtForce = 15f;

    //Awake is the first thing will run
    void Awake()
    {
        //Get components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //Unfreeze time
        Time.timeScale = 1f;
    }

    void Start()
    {
        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        //Make it invisible
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = GroundCheck();

        //We get the horizontal input 
        horizontalMove = Input.GetAxis("Horizontal");
        
        //Affect the animator transition by making the horizontalMove always possitive with Mathf.Abs
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        //If we press jump button down and is grouned, it activates the bool jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            jumpTimer = jumpDuration;
        }

        //If we presss crouch button down, it activates the bool iscrouching
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        }

        //Otherwise disable it
        if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }

        //Set the yVelocity  value
        if (rb.velocity.y <= -0.01f)
        {
            animator.SetFloat("yVelocity", -1f);
        }
        else if (rb.velocity.y >= 0.2f)
        {
            animator.SetFloat("yVelocity", 1f);
        }
        else
        {
            animator.SetFloat("yVelocity", 0f);
        }

        //As long as we are grounded the "Jump" bool  animator is disabled
        animator.SetBool("isJumping", !isGrounded);
    }

    void FixedUpdate()
    {
        //Call Move function
        if (!hurt)
        {
            Move(horizontalMove, isJumping, isCrouching);
        }
    }

    bool GroundCheck()
    {
        //Check if the GroundCheckObject is colliding with other colliders that are in the "Ground" Layer
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.transform.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Move(float direction, bool jump, bool crouch)
    {
        #region Jump
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.fixedDeltaTime;
        }
        else
        {
            isJumping = false;
        }
        if (isJumping)
        {
            rb.velocity = new Vector2(0f, 1 * jumpPower);
        }
        #endregion

        #region Crouch
        //Check overhead for collision with Ground items
        //If there are any, remain crouched, otherwise un-crouch
        /*if (!crouch)
        {
            if (Physics2D.OverlapCircle(overheadCheckCollider.transform.position, OVERHEAD_CHECK_RADIOUS, groundLayer))
            {
                crouch = true;
            }
        }*/
        //When we crouch play the crouch animation
        animator.SetBool("isCrouching", crouch);
        //If we dont crouch enable the standing collider
        standingCollider.enabled = !crouch;
        //If we crouch enable the crouch collider
        crouchingCollider.enabled = crouch;
        #endregion

        #region Movement
        //Set value of x using direction and speed
        float xValue = direction * speed * Time.fixedDeltaTime;

        //If we are crouching multiply with the crouching speed modifier
        if (crouch)
        {
            xValue *= CrouchSpeedModifier;
        }

        //Create Vector2 for the velocity
        Vector2 targetVelocity = new Vector2(xValue, rb.velocity.y);

        //Set the player's velocity
        rb.velocity = targetVelocity;

        //Flip of the character

        // If the player is moving right and the player is facing left...
        if (xValue > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the player is moving left and the player is facing right...
        else if (xValue < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }
        #endregion
    }

    void Flip()
    {
        //Switch the way the player is facing.
        facingRight = !facingRight;

        //Multiply the player's x local scale by -1.
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void HandlePause()
    {
        PressedEscape();
    }

    void PressedEscape()
    {
        //If the game ispaused them
        if (isPaused)
        {
            //Lock the cursor and make it invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isPaused = false;
            return;
        }
        else
        {
            //Otherwise unlock the cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isPaused = true;
            return;
        }
    }

    public void canMove()
    {
        hurt = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //When player will collide with enemy tag .. will play a hurt sound and lose a life
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (rb.velocity.y <= -0.01f)
            {
                rb.velocity = new Vector2(0f, 1 * jumpPower);
                enemy.Death();
            }
            else
            {
                hurt = true;

                animator.SetTrigger("isHurt");
                AudioManager.audioManager.PlaySFX("got_hurt");
                FindObjectOfType<PlayerHealth>().LoseLife();

                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (rb.velocity.y <= -0.01f)
            {
                rb.velocity = new Vector2(0f, 1 * jumpPower);
                enemy.Death();
            }
            else
            {
                hurt = true;

                animator.SetTrigger("isHurt");
                AudioManager.audioManager.PlaySFX("got_hurt");
                FindObjectOfType<PlayerHealth>().LoseLife();

                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }
}