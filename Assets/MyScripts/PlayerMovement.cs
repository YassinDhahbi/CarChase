using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerBody;
    public bool ableToMoveToTheRight = true, ableToMoveToTheLeft = true;

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
        if (Input.GetKeyDown(KeyCode.RightArrow) && ableToMoveToTheRight)
        {
            playerBody.velocity += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && ableToMoveToTheLeft)
        {
            playerBody.velocity += Vector3.left;
        }
    }
}