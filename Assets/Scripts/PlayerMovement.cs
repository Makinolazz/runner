﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpHeight = 0f;
    public float jumpAccelerationPerSecond;
    public float maxJumpHeight;
    private float minJumpHeight = 0f;

    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public GameObject body;

    Vector2 movement;
    float bodyPosition;

    float xBounds = 8f;
    float yBounds = 2.5f;
    float objWidth;
    float objHeight;

    public bool isJumping = false;
    public bool isLanding = false;
    public bool isRampJump = false;

    private void Start()
    {
        bodyPosition = body.transform.position.y - this.transform.position.y;
        objWidth = sp.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objHeight = sp.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

    }

    //Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (isRampJump)
        {
            RampJump();
        }
        else
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Jump()
    {
        if (Input.GetButton("Jump") && !isLanding)
        {
            IncreaseJumpHeight(maxJumpHeight);
        }
        else
        {
            DecreaseJumpHeight(maxJumpHeight);
        }
    }

    private void RampJump()
    {
        //Problem: when is landing cant do Ramp Jump, but seems to be working fine
        if (!isLanding)
        {
            IncreaseJumpHeight(5f);

        }
        else
        {
            DecreaseJumpHeight(5f);

        }
    }

    private void IncreaseJumpHeight(float jumpHeightLimit)
    {
        isJumping = true;

        if (jumpHeight < jumpHeightLimit)
        {
            jumpHeight = Mathf.Clamp(jumpHeight + jumpAccelerationPerSecond * Time.deltaTime, minJumpHeight, jumpHeightLimit);
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

        if (jumpHeight > 0)
        {
            jumpHeight = Mathf.Clamp(jumpHeight - jumpAccelerationPerSecond * Time.deltaTime, minJumpHeight, jumpHeightLimit);
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
        yield return new WaitForSeconds(0.15f);
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
