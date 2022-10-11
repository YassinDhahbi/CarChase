using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapSpot : MonoBehaviour
{
    [SerializeField]
    private bool right, left;

    [SerializeField]
    private PlayerMovement playerScript;

    [SerializeField]
    private List<SnapSpot> listOfStoppers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            playerScript.ableToMoveToTheLeft = left;
            playerScript.ableToMoveToTheRight = right;
            playerScript.ableToMove = true;
            gameObject.SetActive(false);
            for (int i = 0; i < listOfStoppers.Count; i++)
            {
                listOfStoppers[i].gameObject.SetActive(true);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        }
    }
}