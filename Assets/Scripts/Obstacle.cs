using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    public ObstacleType obstacleType;
    public float speed = 0;
    public float height;
    public bool isHarmful;
    public bool isAerial;
    
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //HideAndRecycle();
        if (collision.CompareTag("Player"))
        {
            GameController.Instance.ProcessCollisions(isHarmful, isAerial, height);
        }
        else
        {
            HideAndRecycle();
        }
        
    }

    private void HideAndRecycle()
    {
        this.gameObject.SetActive(false);
    }
    
    public void SetSpeed(float amount)
    {
        speed = amount;
    }

    public void StopMovement()
    {
        speed = 0f; ;    
    }
}
