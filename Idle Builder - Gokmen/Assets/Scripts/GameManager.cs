using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int xPos;
    public int zPos;
    public int workerToBeSpawned;

    [Header("Workers")]

    [SerializeField] private GameObject worker;
    public int workerCount = 5;
    public static GameManager instance;

    void Awake() 
    {
        instance = this;
    }
    void Start()
    {
        WorkerSpawn();
    }

    void Update()
    {
        
    }

    public void OnLevelStarted()
    {
        SceneManager.LoadScene(0);
    }

    public void OnLevelEnded() // Game Over?
    {
        Application.Quit();
    }

    public void OnLevelCompleted() // Loads the next level
    {
        
    }

    public void GoToMenu() // Loads the menu
    {
        SceneManager.LoadScene(0);
    }

    public void OnLevelFailed() // Loads the current scene back
    {
        
    }

    void WorkerSpawn()
    {
        while (workerToBeSpawned < workerCount)
        {
            xPos = Random.Range(-5,10);
            zPos = Random.Range(15, 25);
            Instantiate(worker, new Vector3(xPos, 0, zPos), Quaternion.Euler(0,180,0));
            workerToBeSpawned++;
        }
        
    }
}
