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
    private List<GameObject> obstaclePrefabList;
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
        foreach (var obstacleItem in obstaclePrefabList)
        {
            for (int i = 0; i < amountOfObstacles; i++)
            {
                GameObject obstacle = Instantiate(obstacleItem);
                obstacle.transform.parent = obstacleContainer.transform;
                obstacle.SetActive(false);
                obstaclePool.Add(obstacle);
            }
        }

        return obstaclePool;
    }

    public GameObject RequestObstacle(ObstacleType obstacleType)
    {
        GameObject obstacle = null;
        foreach (var obstacleItem in obstaclePool)
        {
            if (obstacleItem.GetComponent<Obstacle>().obstacleType == obstacleType)
            {
                if (obstacleItem.activeInHierarchy == false)
                {
                    obstacleItem.SetActive(true);
                    return obstacleItem;
                }
                else
                {
                    obstacle = obstacleItem;
                }                
            }
        }

        GameObject newObstacle = Instantiate(obstacle);
        newObstacle.transform.parent = obstacleContainer.transform;
        obstaclePool.Add(newObstacle);

        return newObstacle;
    }
}
