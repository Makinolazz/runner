using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    float jumpHeight = 0f;
    public float jumpAccelerationPerSecond;
    public float maxJumpHeight;
    float minJumpHeight = 0f;

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

    private void Start()
    {
        bodyPosition = body.transform.position.y - this.transform.position.y;
        objWidth = sp.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objHeight = sp.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Jump();
    }
        
    private void FixedUpdate()
    {
        Move();
        //Jump();
    }

    private void Jump()
    {
        if (Input.GetButton("Jump") && !isLanding)
        {
            IncreaseJumpHeight();
        }
        else
        {
            DecreaseJumpHeight();
        }
    }    

    private void IncreaseJumpHeight()
    {
        isJumping = true;

        if (jumpHeight < maxJumpHeight)
        {
            jumpHeight = Mathf.Clamp(jumpHeight + jumpAccelerationPerSecond * Time.deltaTime, minJumpHeight, maxJumpHeight);
            UpdateJumpPosition(jumpHeight);
        }
        else
        {
            DecreaseJumpHeight();
        }
    }       

    private void DecreaseJumpHeight()
    {
        isLanding = true;

        if (jumpHeight > 0)
        {
            jumpHeight = Mathf.Clamp(jumpHeight - jumpAccelerationPerSecond * Time.deltaTime, minJumpHeight, maxJumpHeight);
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
