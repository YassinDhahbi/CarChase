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

    public void ManageNextTarget(Transform _reachedPoint)
    {
        Transform next = null;
        if (_reachedPoint == _startPoint)
        {
            _reachedPoint.gameObject.SetActive(false);
            EndPoint.gameObject.SetActive(true);
            next = _endPoint;
        }
        if (_reachedPoint == _endPoint)
        {
            _reachedPoint.gameObject.SetActive(false);
            Debug.Log("End reached");
        }
        DeliverySystem.instance.SetDestination(next);

    }

}
