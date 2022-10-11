using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private Rigidbody obstacleBody;

    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    private void Awake()
    {
        obstacleBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        obstacleBody.velocity = Vector3.back * gameManager.moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObstacleSpawnResetter"))
        {
            gameManager.ObstacleResetBehaviour();
        }
        if (other.CompareTag("Player"))
        {
            gameManager.LoseBehaviour();
        }
    }
}