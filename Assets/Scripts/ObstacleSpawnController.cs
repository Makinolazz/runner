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

    public GameObject obstacle;
    public List<GameObject> laneSpawners;
    public List<GameObject> wavePatternList;
        
    float defaultObstacleSpeed = 5f;
    public float increaseSpeedBy = 1f;
    float speedAmountToAdd;
    public float maxObstacleSpeed;

    int randomLaneIndex = 0;
    public int obstaclesPerWave;
    int obstacleCounter;

    public bool isSpawning = false;
    bool waveFinished = false;
    bool waveReady = false;

    private float spawnTimer;
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
        //SpawnObstacle();

        //when mouse click, GameController send the order to start spawning
        if (isSpawning)
        {            
            StartSpawningProcess();
            isSpawning = false;
        }
                
    }

    private void StartSpawningProcess()
    {
        //selects a pattern randomly
        int patternIndex = UnityEngine.Random.Range(0, wavePatternList.Count - 1);
        var pattern = wavePatternList[patternIndex].GetComponent<WavePattern>().pattern;

        StartCoroutine(StartObstacleSpawn(pattern));
    }

    IEnumerator StartObstacleSpawn(List<LanePosition> lanePosition)
    {
        foreach (var lane in lanePosition)
        {
            GetObstacleFromPool(lane);
            yield return new WaitForSeconds(1);
        }               
        
    }

    private void GetObstacleFromPool(LanePosition lanePosition)
    {
        GameObject obstacle = ObstaclePoolController.Instance.RequestObstacle();
        obstacle.GetComponent<Obstacle>().SetSpeed(speedAmountToAdd);
        obstacle.transform.position = laneSpawners[(int)lanePosition].transform.position;
    }

    /*
    void SpawnObstacle()
    {
        if (spawnTimer <= 0)
        {
            PlaceObstacle();
            spawnTimer = timeBtwSpawn;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }

    }

    private void PlaceObstacle()
    {
        if (!waveFinished)
        {
            //reduce speed to default if max speed is reached
            if (speedAmountToAdd > maxObstacleSpeed)
            {
                speedAmountToAdd = defaultObstacleSpeed;
            }

            if (obstacleCounter < obstaclesPerWave)
            {
                GetObstacleFromPool();
                obstacleCounter++;
            }
            else if (obstacleCounter == obstaclesPerWave)
            {
                waveFinished = true;
                waveReady = false;
            }
        }
        else if (waveFinished && !waveReady)
        {
            StartCoroutine(PrepareNextWave());
        }
        
    }

    private void GetObstacleFromPool()
    {
        //Random lane select
        randomLaneIndex = UnityEngine.Random.Range(0, laneSpawners.Count);

        //int laneIndex = wavePatternObject.GetComponent<WavePattern>().GetPosValue(obstacleCounter);

        GameObject obstacle = ObstaclePoolController.Instance.RequestObstacle();
        obstacle.GetComponent<Obstacle>().SetSpeed(speedAmountToAdd);
        obstacle.transform.position = laneSpawners[randomLaneIndex].transform.position;
    }

    IEnumerator PrepareNextWave()
    {
        waveReady = true;
        yield return new WaitForSeconds(3f);
        SetWaveValues();
    }

    private void SetWaveValues()
    {
        obstacleCounter = 0;
        speedAmountToAdd += increaseSpeedBy;
        waveFinished = false;
    }
    */

}
