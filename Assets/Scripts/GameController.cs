using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Canvas MenuCanvas;
    private bool isGameOver = false;

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
        if (spawnerController.waveFinished == true && !isGameOver)
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
            ProcessGameOver(isCollision);
        }
        else if (!isHarmful)
        {
            //To do, handle ramp
            bool isCollision = CalculateHeight(isAerial, height);
            ProcessRampJump(isCollision);
        }
    }

    private void ProcessGameOver(bool isCollision)
    {
        if (isCollision)
        {
            //something
            isGameOver = true;
            spawnerController.ForceStopSpawning();

            StartCoroutine(ReloadGame());            
        };
    }

    IEnumerator ReloadGame()
    {
        //Simple reload scene for testing, need to improve
        player.GetComponent<PlayerMovement>().moveSpeed = 0;
        player.GetComponent<PlayerMovement>().jumpAccelerationPerSecond = 0;
        yield return new WaitForSeconds(2f);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
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
        else if (!isAerial && player.GetComponent<PlayerMovement>().jumpHeight <= height)
        {
            return true;
        }

        return false;
    }

    public void StartGameBtn()
    {
        spawnerController.isSpawning = true;
        MenuCanvas.GetComponent<UIController>().HideMenu();
    }

    public void QuitGameBtn()
    {
        Application.Quit();
    }
}
