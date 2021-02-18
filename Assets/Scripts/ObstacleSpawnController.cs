using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnController : MonoBehaviour
{
    private static ObstacleSpawnController _instance;
    public static ObstacleSpawnController Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The ObstacleSpawnController is NULL.");

            return _instance;
        }
    }

    private const float DELAY_FOR_NEXT_WAVE = 3f;
    private const float DELAY_FOR_NEXT_ROUND = 5f;
    private const float DELAY_FOR_NEXT_OBSTACLE = 1.5f;

    public List<GameObject> laneSpawners;
    public List<GameObject> wavePatternList;
        
    float defaultObstacleSpeed = 5f;
    public float increaseSpeedValue = 1f;
    public float speedAmountToAdd;
    public float maxObstacleSpeed;
    
    public bool isSpawning = false;
    public bool waveFinished = false;
    private bool isGameOver = false;

    public int waveCounter = 0;
    public int maxWavesPerRound = 3;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        speedAmountToAdd = defaultObstacleSpeed;
    }

    void Update()
    {
        //when mouse click, GameController send the order to start spawning
        if (isSpawning)
        {
            waveFinished = false;
            StartSpawningProcess();
            isSpawning = false;
        }
                
    }

    private void StartSpawningProcess()
    {
        //selects a pattern randomly
        int patternIndex = UnityEngine.Random.Range(0, wavePatternList.Count);
        var obstacle = wavePatternList[patternIndex].GetComponent<WavePattern>();

        StartCoroutine(StartObstacleSpawn(obstacle));
    }
    
    IEnumerator StartObstacleSpawn(WavePattern wavePattern)
    {

        for (int i = 0; i < wavePattern.amountOfObstacles; i++)
        {
            var lanePosition = wavePattern.pattern[i];
            var obstacleType = wavePattern.obstacle[i];
            if (!isGameOver)
            {
                GetObstacleFromPool(lanePosition, obstacleType);
            }
            yield return new WaitForSeconds(DELAY_FOR_NEXT_OBSTACLE);
        }

        StartCoroutine(DelayForNextWave());
    }
    
    IEnumerator DelayForNextWave()
    {
        waveCounter++;
        yield return new WaitForSeconds(DELAY_FOR_NEXT_WAVE);

        if (waveCounter == maxWavesPerRound)
        {
            yield return new WaitForSeconds(DELAY_FOR_NEXT_ROUND);
            PrepareNextRound();
        }

        waveFinished = true;
    }

    private void PrepareNextRound()
    {
        waveCounter = 0;
        speedAmountToAdd += increaseSpeedValue;
        CheckObstacleMaxSpeedLimit();
    }

    private void CheckObstacleMaxSpeedLimit()
    {
        if (speedAmountToAdd > maxObstacleSpeed)
        {
            ResetObstaclesSpeed();
        }
    }

    private void ResetObstaclesSpeed()
    {
        speedAmountToAdd = defaultObstacleSpeed;
    }

    private void GetObstacleFromPool(LanePosition lanePosition, ObstacleType obstacleType)
    {
        GameObject obstacle = ObstaclePoolController.Instance.RequestObstacle(obstacleType);
        obstacle.GetComponent<Obstacle>().SetSpeed(speedAmountToAdd);
        obstacle.transform.position = laneSpawners[(int)lanePosition].transform.position;
    }

    public void ForceStopSpawning()
    {
        isGameOver = true;
        var obstacleList = FindObjectsOfType<Obstacle>();
        isSpawning = false;
        waveFinished = false;
        speedAmountToAdd = 0f;

        foreach (var obstacle in obstacleList)
        {            
            obstacle.StopMovement();
        }
    }

}
