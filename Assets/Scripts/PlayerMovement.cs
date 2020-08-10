using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //move variables
    public float moveSpeed;
    public Rigidbody2D rb;
    public SpriteRenderer shadow;
    Vector2 movement;
    float xBounds = 8f;
    float yBounds = 2.5f;
    float objWidth;
    float objHeight;

    //jump variables
    public float jumpHeight = 0f;
    public float jumpAccelerationPerSecond;
    public float maxJumpHeight;
    public float maxRampJumpHeight;
    private float baseHeight = 0f;
    public float jumpCooldown;
    public GameObject body;
    float bodyPosition;
    public bool isJumping = false;
    public bool isLanding = false;
    public bool isGrounded = true;
    public bool isRampJump = false;

    private void Start()
    {
        bodyPosition = body.transform.position.y - this.transform.position.y;
        objWidth = shadow.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objHeight = shadow.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

    }

    //Update is called once per frame
    void Update()
    {
        MoveInput();
        JumpInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void JumpInput()
    {
        if (isRampJump)
        {
            RampJump();
        }
        else
        {
            Jump();
        }
    }

    private void MoveInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    

    private void Jump()
    {
        if (Input.GetButton("Jump") && !isLanding)
        {
            IncreaseJumpHeight(maxJumpHeight);
        }
        else if(!isGrounded)
        {
            DecreaseJumpHeight(maxJumpHeight);
        }
    }

    private void RampJump()
    {
        //Problem: when is landing cant do Ramp Jump, but seems to be working fine
        if (!isLanding)
        {
            IncreaseJumpHeight(maxRampJumpHeight);

        }
        else
        {
            DecreaseJumpHeight(maxRampJumpHeight);
        }
    }

    private void IncreaseJumpHeight(float jumpHeightLimit)
    {
        isJumping = true;
        isGrounded = false;

        if (jumpHeight < jumpHeightLimit)
        {
            jumpHeight = Mathf.Clamp(jumpHeight + jumpAccelerationPerSecond * Time.deltaTime, baseHeight, jumpHeightLimit);
            UpdateJumpPosition(jumpHeight);
        }
        else
        {
            DecreaseJumpHeight(jumpHeightLimit);
        }
    }

    private void DecreaseJumpHeight(float jumpHeightLimit)
    {
        isLanding = true;

        if (jumpHeight > baseHeight)
        {
            jumpHeight = Mathf.Clamp(jumpHeight - jumpAccelerationPerSecond * Time.deltaTime, baseHeight, jumpHeightLimit);
            UpdateJumpPosition(jumpHeight);
        }
        else
        {
            StartCoroutine(LandAndJumpDelay());
        }
    }

    private void UpdateJumpPosition(float jumpHeight)
    {
        body.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + bodyPosition + jumpHeight, this.transform.position.z);

    }

    IEnumerator LandAndJumpDelay()
    {
        isRampJump = false;
        isGrounded = true;
        yield return new WaitForSeconds(jumpCooldown);
        isLanding = false;
        isJumping = false;
    }

    private void Move()
    {
        Vector2 newPos;
        newPos.x = Mathf.Clamp(rb.position.x + movement.x * moveSpeed * Time.fixedDeltaTime, -xBounds + objWidth, xBounds - objWidth);
        newPos.y = Mathf.Clamp(rb.position.y + movement.y * moveSpeed * Time.fixedDeltaTime, -yBounds + objHeight, yBounds - objHeight);

        rb.MovePosition(newPos);

        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
