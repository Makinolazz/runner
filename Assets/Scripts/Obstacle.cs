using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    public ObstacleType obstacleType;
    public float speed = 0;
    public float height;
    
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HideAndRecycle();
    }

    private void HideAndRecycle()
    {
        this.gameObject.SetActive(false);
    }
    
    public void SetSpeed(float amount)
    {
        speed = amount;
    }
}
