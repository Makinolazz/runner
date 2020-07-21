using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    public Rigidbody2D rb;
    public SpriteRenderer sp;

    Vector2 movement;

    float xBounds = 8f;
    float yBounds = 2.5f;
    float objWidth;
    float objHeight;

    private void Start()
    {
        objWidth = sp.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objHeight = sp.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }
        
    private void FixedUpdate()
    {
        Vector2 newPos;
        newPos.x = Mathf.Clamp(rb.position.x + movement.x * moveSpeed * Time.fixedDeltaTime, -xBounds + objWidth, xBounds - objWidth);
        newPos.y = Mathf.Clamp(rb.position.y + movement.y * moveSpeed * Time.fixedDeltaTime, -yBounds + objHeight, yBounds - objHeight);

        rb.MovePosition(newPos);

        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

}
