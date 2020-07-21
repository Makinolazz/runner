using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePoolController : MonoBehaviour
{
    private static ObstaclePoolController _instance;
    public static ObstaclePoolController Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The ObstaclePoolController is NULL.");

            return _instance;
        }
    }

    [SerializeField]
    private GameObject obstacleContainer;
    [SerializeField]
    private GameObject obstaclePrefab;
    [SerializeField]
    private List<GameObject> obstaclePool;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        obstaclePool = GenerateObstacles(10);
    }

    private List<GameObject> GenerateObstacles(int amountOfObstacles)
    {
        for (int i = 0; i < amountOfObstacles; i++)
        {
            GameObject obstacle = Instantiate(obstaclePrefab);
            obstacle.transform.parent = obstacleContainer.transform;
            obstacle.SetActive(false);
            obstaclePool.Add(obstacle);
        }

        return obstaclePool;
    }

    public GameObject RequestObstacle()
    {
        foreach (var obstacle in obstaclePool)
        {
            if (obstacle.activeInHierarchy == false)
            {
                obstacle.SetActive(true);
                return obstacle;
            }
        }

        GameObject newObstacle = Instantiate(obstaclePrefab);
        newObstacle.transform.parent = obstacleContainer.transform;
        obstaclePool.Add(newObstacle);

        return newObstacle;
    }
}
