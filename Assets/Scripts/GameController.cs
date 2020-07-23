using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject obstacleSpawner;
    public GameObject player;
    private ObstacleSpawnController spawnerController;

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
    }
}
