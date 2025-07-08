using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizablePlayerControllerScript : MonoBehaviour
{
    enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Fall,
        WallJump,
        TakeHit,
        DoubleJump,
        NoState
    }

    private Rigidbody2D rb2d;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    public LayerMask groundMask;

    public Transform GroundCheckBody;

    public Transform WallCheckBodyRight;

    public Transform WallCheckBodyLeft;

    private float moveInput;

    private bool isRight = true;

    private bool isGrounded = false;

    private bool isWallSliding = false;

    private bool isWallJumping = false;

    public float wallJumpDuration = 0.2f;

    private bool isTouchingWallRight = false;

    private bool isTouchingWallLeft = false;

    private bool canDoubleJump = false;

    private PlayerState currentState = PlayerState.NoState;

    public float groundDistance = 0.2f;

    public float wallDistance = 0.2f;

    public float moveSpeed = 10f;

    public float jumpForce = 15f;

    public float deadZoneAnimationTrigger = 0.1f;

    public string idleTriggerName = "idle";

    public string wallJumpTriggerName = "wallJump";

    public string fallTriggerName = "fall";

    public string runTriggerName = "run";

    public string takeHitTriggerName = "takeHit";

    public string doubleJumpTriggerName = "doubleJump";

    public string jumpTriggerName = "jump";


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown("Jump"))
    && (isGrounded || canDoubleJump || isWallSliding))
        {
            float jumpVelocityY = jumpForce;
            float jumpVelocityX = rb2d.linearVelocity.x;

            if (isWallSliding)
            {
                isWallJumping = true;

                if (isTouchingWallRight)
                {
                    jumpVelocityX = -1 -moveSpeed;
                }
                else if (isTouchingWallLeft)
                {
                    jumpVelocityX = 1 + moveSpeed;
                }

                isWallSliding = false;

                StartCoroutine(ResetWallJump());
            }

            if (!isGrounded)
            {
                canDoubleJump = false;
            }

            rb2d.linearVelocity = new Vector2(jumpVelocityX, jumpVelocityY);
        }
    }

    private void FixedUpdate()
    {
        GroundCheck();

        moveInput = Input.GetAxis("Horizontal");

        WallCheck();
        WallGrab();
        Move();
        
        if (moveInput > 0 && !isRight)
        {
            FlipX();
        }
        else if (moveInput < 0 && isRight)
        {
            FlipX();
        }

        AnimationSwitcher();
    }

    private void WallGrab()
    {
        bool wasSliding = isWallSliding;

        if (isWallJumping)
        {
            isWallSliding = false;
            return;
        }

        isWallSliding = ((isTouchingWallRight && moveInput > 0.01f) ||
                         (isTouchingWallLeft && moveInput < -0.01f)) && !isGrounded;

        if (wasSliding && !isWallSliding && !isGrounded && !isWallJumping)
        {
            animator.SetTrigger(fallTriggerName);
            currentState = PlayerState.Fall;
        }
    }

    private void Move()
    {
        float horizontalVelocity = moveInput * moveSpeed;
        float verticalVelocity = rb2d.linearVelocity.y;

        if (isWallSliding)
        {
            horizontalVelocity = 0f;
            verticalVelocity = -2f;
        }

        rb2d.linearVelocity = new Vector2(horizontalVelocity, verticalVelocity);
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheckBody.position, groundDistance, groundMask);

        if (isGrounded)
        {
            canDoubleJump = true;
        }
    }

    private void WallCheck()
    {
        isTouchingWallRight = Physics2D.OverlapCircle(WallCheckBodyRight.position, wallDistance, groundMask);
        isTouchingWallLeft = Physics2D.OverlapCircle(WallCheckBodyLeft.position, wallDistance, groundMask);
    }

    private void AnimationSwitcher()
    {
        if (moveInput == 0
            && isGrounded
            && currentState != PlayerState.Idle)
        {
            animator.SetTrigger(idleTriggerName);
            currentState = PlayerState.Idle;
        }
        else if (moveInput != 0
            && isGrounded
            && currentState != PlayerState.Run)
        {
            animator.SetTrigger(runTriggerName);
            currentState = PlayerState.Run;
        }
        else if (isWallSliding
            && currentState != PlayerState.WallJump)
        {
            animator.SetTrigger(wallJumpTriggerName);
            currentState = PlayerState.WallJump;
        }
        else if (rb2d.linearVelocity.y > deadZoneAnimationTrigger
            && !isGrounded
            && !canDoubleJump
            && currentState != PlayerState.DoubleJump)
        {
            animator.SetTrigger(doubleJumpTriggerName);
            currentState = PlayerState.DoubleJump;
        }
        else if (rb2d.linearVelocity.y > deadZoneAnimationTrigger
            && !isGrounded
            && canDoubleJump
            && currentState != PlayerState.Jump)
        {
            animator.SetTrigger(jumpTriggerName);
            currentState = PlayerState.Jump;
        }
        else if (rb2d.linearVelocity.y < -deadZoneAnimationTrigger
            && !isGrounded
            && canDoubleJump
            && !isWallSliding
            && currentState != PlayerState.Fall)
        {
            animator.SetTrigger(fallTriggerName);
            currentState = PlayerState.Fall;
        }
    }

    private void FlipX()
    {
        isRight = !isRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private IEnumerator ResetWallJump()
    {
        yield return new WaitForSeconds(wallJumpDuration);
        isWallJumping = false;
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.blue;
        if (WallCheckBodyLeft != null)
        {
            Gizmos.DrawWireSphere(WallCheckBodyLeft.position, wallDistance);
        }

        Gizmos.color = Color.red;
        if (WallCheckBodyRight != null)
        {
            Gizmos.DrawWireSphere(WallCheckBodyRight.position, wallDistance);
        }

        Gizmos.color = Color.green;
        if (GroundCheckBody != null)
        {
            Gizmos.DrawWireSphere(GroundCheckBody.position, groundDistance);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap"))
        {
            animator.SetTrigger(takeHitTriggerName);
        }
    }

    public void StuckByGettingDamage()
    {
        rb2d.linearVelocity = new Vector2(0, 0);
    }
}
