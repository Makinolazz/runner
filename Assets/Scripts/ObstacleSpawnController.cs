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

    //public GameObject obstacle;
    public List<GameObject> laneSpawners;
    public List<GameObject> wavePatternList;
        
    float defaultObstacleSpeed = 5f;
    public float increaseSpeedBy = 1f;
    float speedAmountToAdd;
    public float maxObstacleSpeed;
    
    public bool isSpawning = false;
    public bool waveFinished = false;
    private bool isGameOver = false;

    public float timeBtwSpawn;

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
        var pattern = wavePatternList[patternIndex].GetComponent<WavePattern>().pattern;
        var obstacle = wavePatternList[patternIndex].GetComponent<WavePattern>();

        //StartCoroutine(StartObstacleSpawn(pattern));
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
            yield return new WaitForSeconds(1);
        }

        StartCoroutine(DelayForNextWave());
    }
    
    IEnumerator DelayForNextWave()
    {
        yield return new WaitForSeconds(2);
        waveFinished = true;
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

        foreach (var obstacle in obstacleList)
        {            
            obstacle.StopMovement();
        }
    }

}
