using ArcadeVehicleController;
using UnityEngine;

public class WayPointNode : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Vehicle vehicle))
        {
            DeliverySystem.Instance.ManageDestinations(transform);
            gameObject.SetActive(false);
        }
    }
}


