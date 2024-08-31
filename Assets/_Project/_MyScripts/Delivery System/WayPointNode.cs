using UnityEngine;

public class WayPointNode : MonoBehaviour
{
    [SerializeField] private WayPointNodeType _nodeType;
    private void Start()
    {
        DeliverySystem.instance.AddNode(transform, _nodeType);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
           DeliverySystem.instance.PathCombination.ManageNextTarget(transform);
        }
    }
}

public enum WayPointNodeType
{
    Start,
    End
}
