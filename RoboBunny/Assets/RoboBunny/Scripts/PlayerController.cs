using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// https://www.youtube.com/watch?v=FXpUb-H54Oc
// https://www.youtube.com/watch?v=O6VX6Ro7EtA

public class PlayerController : MonoBehaviour
{
    [Header("For Movement")]
    [SerializeField] float moveSpeed = 10f;
    private float XDirectionalInput;
    private bool facingRight = true;
    private bool isMoving;

    [Header("For Jumping")]
    [SerializeField] float jumpForce = 16f;
    [SerializeField] int extraJumpsValue = 1;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Vector2 groundCheckSize;
    private bool grounded;
    private bool jumpInput;
    private int extraJumps;

    [Header("For WallSliding")]
    [SerializeField] float wallSlideSpeed;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Vector2 wallCheckSize;
    private bool isTouchingWall;
    private bool isWallSliding;

    [Header("For WallJumping")]
    [SerializeField] Vector2 wallJumpingPower = new Vector2(8f, 16f);
    [SerializeField] float wallJumpingDirection = -1;
    private bool isWallJumping;
    private float wallJumpingDuration = 0.4f;

    [Header("For Dashing")]
    [SerializeField] TrailRenderer tr;
    [SerializeField] float dashingPower = 24f;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float dashingCooldown = 1f;
    [SerializeField] float dashDirection = 1;
    private bool canDash = true;
    private bool isDashing = false;
    private bool dashInput;

    [Header("Other")]
    [SerializeField] Animator anim;
    private Rigidbody2D rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr.emitting = false;
        tr.sortingOrder = 99;
        tr.sortingLayerName = "Background";
    }

    private void Update()
    {
        Inputs();
        CheckWorld();

        Debug.Log(extraJumps);
        
        Movement();
        Jump();
        WallSlide();
        WallJump();
        Dash();

        // AnimationControl();
    }

    void Inputs()
    {
        XDirectionalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
        dashInput = Input.GetButtonDown("Fire3");
    }
    void CheckWorld()
    {
        grounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);

        if (grounded || isTouchingWall)
        {
            extraJumps = extraJumpsValue;
        }
    }

    void Movement()
    {
        if (isWallJumping || isDashing)
        {
            return;
        }

        //for Animation
        if (XDirectionalInput != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        //for movement
        rb.velocity = new Vector2(XDirectionalInput * moveSpeed, rb.velocity.y);

        //for fliping
        if (XDirectionalInput < 0 && facingRight)
        {
            Flip();
        }
        else if (XDirectionalInput > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        if (isWallSliding)
        {
            return;
        }

        wallJumpingDirection *= -1;
        dashDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

    }

    void Jump()
    {
        if (isDashing || isWallSliding)
        {
            return;
        }

        if (jumpInput && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (jumpInput && extraJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            extraJumps--;
        }

        if (jumpInput && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    void WallSlide()
    {
        if (isDashing)
        {
            return;
        }

        if (isTouchingWall && !grounded)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
    }
    void WallJump()
    {
        if (isDashing)
        {
            return;
        }

        if (isWallSliding)
        {
            isWallJumping = false;
            CancelInvoke(nameof(StopWallJumping));
        }

        if (jumpInput && isWallSliding)
        {
            isWallJumping = true;
            isWallSliding = false;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);

            Flip();

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    void Dash()
    {
        if (dashInput && canDash)
        {
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(dashDirection * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void AnimationControl()
    {
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", grounded);
        anim.SetBool("isSliding", isTouchingWall);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);

    }

}
