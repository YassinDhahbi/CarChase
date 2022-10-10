using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSpot : MonoBehaviour
{
    [SerializeField]
    private bool right, left;

    [SerializeField]
    private PlayerMovement playerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            playerScript.ableToMoveToTheLeft = left;
            playerScript.ableToMoveToTheRight = right;
        }
    }
}