﻿using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public float moveForce = 40;
    public float jumpForce;
    public float maxJumpVelocity;
    public float maxMoveVelocity;

    private Rigidbody2D rigidBody;
    private Vector2 movementVector;
    private enum JumpState
    {
        Ground,
        SingleJump,
        DoubleJump
    };
    private JumpState jumpState;

    //player faces right by default
    private bool isFacingRight = true;
    private bool isGrounded = false;

    private SpriteRenderer spriteRenderer;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        movementVector = new Vector2();
        jumpState = JumpState.Ground;
        jumpForce = rigidBody.mass * 200 * jumpForce;
        maxJumpVelocity = rigidBody.mass * 150;
        //excitementJump();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isPlayerAlive)
        {
            if (groundedCheck())
            {
                jumpState = JumpState.Ground;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                //Debug.Log("left");
                moveLeft();
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                //Debug.Log("right");
                moveRight();
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && jumpState != JumpState.SingleJump)
            {
                //Debug.Log("jump");
                jump();
            }
        }

        if (rigidBody.velocity == Vector2.zero)
        {
            anim.SetBool("Walking", false);
        }
    }

    bool groundedCheck()
    {
        RaycastHit2D rch;
        rch = Physics2D.Linecast(transform.position - (transform.right * 0.25f), transform.position - (transform.up * 1.05f) - (transform.right * 0.25f), 1);
        if (rch.collider != null)
        {
            return true;
        }

        rch = Physics2D.Linecast(transform.position + (transform.right * 0.25f), transform.position - (transform.up * 1.05f) + (transform.right * 0.25f), 1);
        if (rch.collider != null)
        {
            //Debug.Log(rch.collider.name);
            return true;
        }

        return false;
    }

    void moveLeft()
    {
        movementVector.Set(-moveForce, 0);
        rigidBody.AddForce(movementVector);

        //orient player to the direction of movement
        if (isFacingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = false;
        }
        if (rigidBody.velocity.x < -maxMoveVelocity)
        {
            rigidBody.velocity = new Vector2(-maxMoveVelocity, rigidBody.velocity.y);
        }

        anim.SetBool("Walking", true);
    }

    void moveRight()
    {
        movementVector.Set(moveForce, 0);
        rigidBody.AddForce(movementVector);

        //orient player to the direction of movement
        if (!isFacingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = true;
        }
        if (rigidBody.velocity.x > maxMoveVelocity)
        {
            rigidBody.velocity = new Vector2(maxMoveVelocity, rigidBody.velocity.y);
        }

        anim.SetBool("Walking", true);
    }

    void jump()
    {
        movementVector.Set(0, jumpForce);
        switch (jumpState)
        {
            case JumpState.Ground:
                jumpState = JumpState.SingleJump;
                rigidBody.AddForce(movementVector);

                AudioManagerr.playAudioClip(3);
                break;
            case JumpState.SingleJump:
                jumpState = JumpState.DoubleJump;
                rigidBody.AddForce(movementVector);

                AudioManagerr.playAudioClip(3);
                break;
            case JumpState.DoubleJump:
            default:
                break;
        }
        if (rigidBody.velocity.y > maxJumpVelocity)
        {
            print("limiting velocity");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, maxJumpVelocity);
        }
        //character stops walking motion when jumping
        anim.SetBool("Walking", false);
    }

    void excitementJump()
    {
        jump();
        jump();
    }

    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (jumpState == JumpState.SingleJump || jumpState == JumpState.DoubleJump)
        {
            jumpState = JumpState.Ground;
        }
    }
    */
}
