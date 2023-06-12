using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://www.youtube.com/watch?v=FXpUb-H54Oc
// https://www.youtube.com/watch?v=O6VX6Ro7EtA

public class PlayerController : MonoBehaviour
{
    #region Parameters
    [Header("For Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] private ParticleSystem dust;
    private float XDirectionalInput;
    private bool facingRight = true;
    private bool isMoving;
    private int facingDirection = 1; // 1 is right, -1 is left

    [Header("For Jumping")]
    [SerializeField] float jumpForce = 16f;
    [SerializeField] int extraJumpsValue = 1;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] float jumpCoyoteTime = 0.15f;
    [SerializeField] float jumpBufferTime = 0.10f;
    [SerializeField] float jumpCutMultiplier = 0.5f;
    [SerializeField] float fallGravityMultiplier = 1.2f;
    [SerializeField] float gravityScale = 1.0f;
    [SerializeField] float maxFallVelocity = -40.0f;
    private bool grounded;
    private bool jumpInput;
    private bool jumpInputUp;
    private int extraJumps;
    private float lastGroundedTime;
    private float lastJumpTime;

    [Header("For PogoJumping")]
    [SerializeField] float pogoForce = 18f;
    [SerializeField] private LayerMask pogoAble;
    private bool pogoInput;
    

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
    private bool finishDashCooldown = true;
    private bool isDashing = false;
    private bool dashInput;

    [Header("For Hit")]
    [SerializeField] Vector2 hitKnockbackPower = new Vector2(3f, 3f);
    [SerializeField] float hitDuration = 0.15f;
    [SerializeField] float hitCooldown = 0.5f;
    [SerializeField] float frictionAmount = 0.35f;
    private bool finishHitCooldown = true;
    private bool isHit = false;
    private bool isColliding = false;
    private float hitDirection;

    [Header("For Death")]
    [SerializeField] float deathDuration = 5f;
    private bool isDead = false;

    [Header("For Health")]
    [SerializeField] int maxHealth = 3;
    private int currentHealth;

    [Header("Other")]
    [SerializeField] Animator anim;
    private Rigidbody2D rb;
    #endregion
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        AnimationControl();
        GatherInput();
        if (isDashing || isHit || isWallJumping)
        {
            return;
        }
        CheckWorld();
        Movement();
    }

    void GatherInput()
    {
        XDirectionalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");
        jumpInputUp = Input.GetButtonUp("Jump");
        dashInput = Input.GetButtonDown("Fire3");
        pogoInput = Input.GetButtonDown("Fire1");
    }
    void CheckWorld()
    {
        grounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);
        
        if (grounded)
        {
            lastGroundedTime = jumpCoyoteTime;
            extraJumps = extraJumpsValue;
            canDash = true;
            
            isWallSliding = false;
        }
        else if (isTouchingWall)
        {
            extraJumps = extraJumpsValue;
            canDash = true;
            
            isWallSliding = true;
            // Debug.Log("Wall sliding");
            if (rb.velocity.y < 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            }
        }
        else // in the air
        {
            // Debug.Log("in the air");
            isWallSliding = false;
            lastGroundedTime -= Time.deltaTime;
            HandleAirMovement();
        }
    }

    void Movement()
    {
        // What actually moves the player
        rb.velocity = new Vector2(XDirectionalInput * moveSpeed, rb.velocity.y);
        
        // Update direction player is facing
        if (XDirectionalInput < 0 && facingDirection == 1)
        {
            Debug.Log("1 Flipped");
            Flip();
        }
        else if (XDirectionalInput > 0 && facingDirection == -1)
        {
            Debug.Log("-1 Flipped");
            Flip();
        }
        
        // Update animation if player is moving
        isMoving = XDirectionalInput != 0;
        // Dashing
        if (dashInput && canDash && finishDashCooldown)
        {
            StartCoroutine(DashRoutine());
        }
        
        // Jumping
        if (jumpInput)
        {
            lastJumpTime = jumpBufferTime;
            TryJump();
        }
        else
        {
            lastJumpTime -= Time.deltaTime;
        }
        
    }

    void Flip()
    {
        Debug.Log("flipping");
        facingDirection *= -1;
        transform.Rotate(0, 180, 0);
        if (grounded)
        {   
            dust.Play();
        }
    }

    void TryJump()
    {
        // Perform wall jump
        if (isWallSliding)
        {
            StartCoroutine(WallJumpRoutine());
        }
        
        // Perform regular jump
        else if (lastJumpTime > 0 && lastGroundedTime > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
            lastJumpTime = 0;

            anim.SetTrigger("Jump");
            dust.Play();
        }
        // Double jump
        else if (extraJumps > 0 && !grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            extraJumps--;

            anim.SetTrigger("DoubleJump");
        }
    }

    private IEnumerator WallJumpRoutine()
    {
        Debug.Log("Start wall jump");
        rb.velocity = new Vector2(facingDirection * -1 * wallJumpingPower.x, wallJumpingPower.y);
        isWallSliding = false;
        anim.SetTrigger("Jump");
        isWallJumping = true;
        Flip();
        yield return new WaitForSeconds(wallJumpingDuration);
        isWallJumping = false;
        Debug.Log("End wall jump");
    }

    void HandleAirMovement()
    {
        if (jumpInputUp && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier);

            lastGroundedTime = 0f;
        }

        if (rb.velocity.y < 0f)
        {
            rb.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }

        rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, maxFallVelocity)); 
    }

    // void PogoJump()
    // {
    //     if (isDashing || isWallSliding || isHit || grounded)
    //     {
    //         return;
    //     }
    //
    //     if (pogoInput)
    //     {
    //         if (Physics2D.OverlapBox(groundCheckPoint.position, pogoCheckSize, 0, pogoAble))
    //         {
    //             
    //         }
    //     }
    // }
    

    void Dash()
    {
        if (isHit)
        {
            return;
        }

        if (dashInput && canDash && finishDashCooldown)
        {
            if (isWallSliding ||
                (XDirectionalInput < 0 && facingRight) || 
                (XDirectionalInput > 0 && !facingRight))
            {
                Flip();
            }

            isWallSliding = false;
            isWallJumping = false;
            // CancelInvoke(nameof(StopWallJumping));

            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine()
    {
        canDash = false;
        finishDashCooldown = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(facingDirection * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        finishDashCooldown = true;
    }

    void Hit()
    {
        if (!finishHitCooldown)
        {
            return;
        }
        
        currentHealth--;
        if (currentHealth <= 0)
        {
            enabled = false;
            isDead = true;
            StartCoroutine(DeathRoutine());
        }
        else
        {
            StartCoroutine(HitRoutine());
        }

        if (grounded)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(rb.velocity.x);

            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    private IEnumerator HitRoutine()
    {
        isHit = true;
        anim.SetBool("Hit", isHit);
        finishHitCooldown = false;
        rb.velocity = new Vector2(hitDirection * hitKnockbackPower.x, hitKnockbackPower.y);
        Debug.Log(rb.velocity);
        yield return new WaitForSeconds(hitDuration);
        isHit = false;
        yield return new WaitForSeconds(hitCooldown);
        finishHitCooldown = true;
    }

    private IEnumerator DeathRoutine()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetBool("Moving", false);
        anim.SetBool("WallSlide", false);
        anim.SetFloat("AirSpeedY", 0);
        anim.SetBool("Grounded", true);
        anim.SetBool("Dashing", false);
        anim.SetBool("Hit", false);
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(deathDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (transform.position.x < collision.gameObject.transform.position.x)
            {
                hitDirection = -1;
            }
            else
            {
                hitDirection = 1;
            }

            Hit();
        }
        else if (collision.gameObject.CompareTag("Death"))
        {
            
            enabled = false;
            isDead = true;
            StartCoroutine(DeathRoutine());
        }
    }

    // void OnTriggerExit2D(UnityEngine.Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Trap"))
    //     {
    //         isColliding = false;
    //     }
    // }

    void AnimationControl()
    {
        if (isDead) return;

        anim.SetBool("Moving", isMoving);
        anim.SetBool("WallSlide", isWallSliding);
        anim.SetFloat("AirSpeedY", rb.velocity.y);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Dashing", isDashing);
        anim.SetBool("Hit", isHit);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);

    }

}
