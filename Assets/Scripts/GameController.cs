﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The GameController is NULL.");

            return _instance;
        }
    }

    public GameObject obstacleSpawner;
    public GameObject player;
    private ObstacleSpawnController spawnerController;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnerController = obstacleSpawner.GetComponent<ObstacleSpawnController>();
    }

    // Update is called once per frame
    void Update()
    {
        //to test spawning activation with jump key pressing
        if (Input.GetMouseButtonDown(0))
        {
            //obstacleSpawner.GetComponent<ObstacleSpawnController>().isSpawning = true;
            spawnerController.isSpawning = true;
        }

        if (spawnerController.waveFinished == true)
        {
            spawnerController.isSpawning = true;
        }
    }

    public void ProcessCollisions(bool isHarmful, bool isAerial, float height)
    {
        if (isHarmful)
        {
            //To do, handle collision
            bool isCollision = CalculateHeight(isAerial, height);
            Debug.Log(isCollision);
        }
        else if (!isHarmful)
        {
            //To do, handle ramp
            bool isCollision = CalculateHeight(isAerial, height);
            ProcessRampJump(isCollision);
        }
    }

    private void ProcessRampJump(bool isCollision)
    {
        if (isCollision)
        {
            //something
            player.GetComponent<PlayerMovement>().isRampJump = true;
        }

        return;
    }

    private bool CalculateHeight(bool isAerial, float height)
    {
        if (isAerial && player.GetComponent<PlayerMovement>().jumpHeight > height)
        {
            return true;
        }
        else if (!isAerial && player.GetComponent<PlayerMovement>().jumpHeight < height)
        {
            return true;
        }

        return false;
    }
}
