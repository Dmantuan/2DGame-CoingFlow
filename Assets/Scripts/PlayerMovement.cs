using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum MovementState { idle, running, jumping,  falling}

    private new Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSoundEffect;

    private float dirX;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float moveSpeed = 7f;


    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity = new Vector2(dirX * moveSpeed, rigidbody2D.velocity.y);

        if(Input.GetButtonDown("Jump") && IsGrounded() ) 
        {
            jumpSoundEffect.Play();
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if(dirX > 0f)
        {
            state = MovementState.running;
            spriteRenderer.flipX = false;
        }
        else if(dirX < 0f)
        {
            state = MovementState.running;
            spriteRenderer.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rigidbody2D.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if(rigidbody2D.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", ((int)state));
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
