using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerBody;
    public bool ableToMoveToTheRight = true, ableToMoveToTheLeft = true, ableToMove;

    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    private void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        CarControls();
    }

    public void CarControls()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && ableToMoveToTheRight && ableToMove)
        {
            playerBody.velocity += Vector3.right * gameManager.moveSpeed;
            ableToMove = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && ableToMoveToTheLeft && ableToMove)
        {
            playerBody.velocity += Vector3.left * gameManager.moveSpeed;
            ableToMove = false;
        }
    }
}