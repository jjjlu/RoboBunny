using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState
    {
        idle=0,
        running,
        jumping,
        falling
    }

    private MovementState state = MovementState.idle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {


        MovementState state;
        float inputX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputX * 7.0f, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0.0f, Vector2.down, 0.1f, jumpableGround))
        {
            rb.velocity = new Vector2(rb.velocity.x, 14.0f);
        }

        if (inputX > 0.0f)
        {
            sprite.flipX = false;
            state = MovementState.running;
        }
        else if (inputX < 0.0f)
        {
            sprite.flipX = true;
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);


        

    }
}
