using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovementScript;

    private Vector3 environmentInitialPos;

    [SerializeField]
    private GameObject environment;

    [SerializeField]
    private float environmentResetTimer = 5, initialEnvironmentTimer;

    [SerializeField]
    private List<Transform> listOfSpawners;

    [SerializeField]
    private List<ObstacleScript> listOfObstacle;

    [SerializeField]
    private int maxSpeed;

    [SerializeField]
    private GameObject explosionPrefab;

    public int moveSpeed;
    private int score = 0;

    [SerializeField]
    private float spawningTimer, initialSpawnTimer;

    private void Start()
    {
        initialEnvironmentTimer = environmentResetTimer;
        initialSpawnTimer = spawningTimer;
        environmentInitialPos = environment.transform.position;
        SpawnningSystem();
    }

    private void Update()
    {
        playerMovementScript.CarControls();
        ResetEnvironementTimerManager();
    }

    private void ResetEnvironementTimerManager()
    {
        if (environmentResetTimer > 0)
        {
            environmentResetTimer -= Time.deltaTime;
        }
        else
        {
            environmentResetTimer = initialEnvironmentTimer;
            environment.transform.position = environmentInitialPos;
        }
    }

    private void SpawnningSystem()
    {
        int randomObstaclePicker, randomSpawnerPicker;
        randomObstaclePicker = Random.Range(0, listOfObstacle.Count);
        randomSpawnerPicker = Random.Range(0, listOfSpawners.Count);
        listOfObstacle[randomObstaclePicker].transform.position = listOfSpawners[randomSpawnerPicker].transform.position;
        spawningTimer = initialSpawnTimer;
    }

    public void ObstacleResetBehaviour()
    {
        SpawnningSystem();
        if (moveSpeed < maxSpeed)
        {
            moveSpeed++;
        }
        score += 10 * moveSpeed;
        if (spawningTimer > 1)
        {
            spawningTimer--;
        }
    }

    public void LoseBehaviour()
    {
        print("You lost! Your score is " + score);
        moveSpeed = 0;
        explosionPrefab.SetActive(true);
        playerMovementScript.ableToMove = false;
    }
}