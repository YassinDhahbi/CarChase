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
    private float environmentResetTimer = 5, initialTimer;

    private void Start()
    {
        initialTimer = environmentResetTimer;
        environmentInitialPos = environment.transform.position;
    }

    private void Update()
    {
        playerMovementScript.CarControls();
        ResetTimerManager();
    }

    private void ResetTimerManager()
    {
        if (environmentResetTimer > 0)
        {
            environmentResetTimer -= Time.deltaTime;
        }
        else
        {
            environmentResetTimer = initialTimer;
            environment.transform.position = environmentInitialPos;
        }
    }
}