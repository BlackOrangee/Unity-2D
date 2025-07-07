using System;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private Animator anim;

    public Transform PlayerTransform;

    private float moveInput;

    public float moveSpeed = 2f;

    public float jumpForce = 3f;

    public float deadZoneAnimationTrigger = 1.8f;

    public LayerMask groundMask;

    public float groundDistance = 0.5f;

    private bool isGrounded = false;

    private bool isRight = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(PlayerTransform.position, groundDistance, groundMask);

        moveInput = Input.GetAxis("Horizontal");
        rb2d.linearVelocity = new Vector2(moveInput * moveSpeed, rb2d.linearVelocity.y);
        
        if (moveInput > 0 && !isRight)
        {
            FlipX();
        }
        else if (moveInput < 0 && isRight)
        {
            FlipX();
        }

        AnimationRun();
        AnimationJump();
        AnimationFall();
    }

    private void AnimationRun()
    {
        if (moveInput != 0)
        {
            anim.SetBool("IsRun", true);
        }
        else
        {
            anim.SetBool("IsRun", false);
        }
    }

    private void AnimationJump()
    {
        if (rb2d.linearVelocity.y > deadZoneAnimationTrigger)
        {
            anim.SetBool("IsJump", true);
        }
        else if(rb2d.linearVelocity.y <= deadZoneAnimationTrigger)
        {
            anim.SetBool("IsJump", false);
        }
    }

    private void AnimationFall()
    {
        if (rb2d.linearVelocity.y < -deadZoneAnimationTrigger)
        {
            anim.SetBool("IsFall", true);
        }
        else if (rb2d.linearVelocity.y >= -deadZoneAnimationTrigger)
        {
            anim.SetBool("IsFall", false);
        }
    }

    private void FlipX()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isRight = !isRight;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown("Jump")) && isGrounded)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
        }
    }
}
