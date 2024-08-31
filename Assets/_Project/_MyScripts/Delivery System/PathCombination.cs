using UnityEngine;

[System.Serializable]
public class PathCombination
{
    [SerializeField] private Transform _startPoint;
    public Transform StartPoint => _startPoint;
    [SerializeField] private Transform _endPoint;
    public Transform EndPoint => _endPoint;

    [SerializeField] private int _distance;

    public int Distance => _distance;

    public int Cost => _distance / 10;

    public PathCombination(Transform startPoint, Transform endPoint)
    {
        _startPoint = startPoint;
        _endPoint = endPoint;
        _distance = Mathf.RoundToInt(Vector3.Distance(startPoint.position, endPoint.position));
    }

    public void ActivatePath()
    {
        _startPoint.gameObject.SetActive(true);
        _endPoint.gameObject.SetActive(false);
        DeliverySystem.Instance.SetDestination(_startPoint);
    }



}
