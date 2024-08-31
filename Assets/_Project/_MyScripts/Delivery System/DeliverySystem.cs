using System.Collections.Generic;
using UnityEngine;

public class DeliverySystem : MonoBehaviour
{
    public static DeliverySystem instance;
    [Header("Path Configuration")]
    [SerializeField] private int _numberOfPaths;
    [SerializeField] private bool _isDestinationReached;
    [SerializeField] private PathCombination _currentPathCombination;
    [Header("Player Details ")]
    [SerializeField] private Transform _playerTransform;
    [Header("Destination Arrow Configuration")]
    [SerializeField] private Transform _destinationArrow;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _destination;
    [Header("NPC Settings")]
    [SerializeField] private List<SpawnlableNPC> _listOfSpawnlableNPCs = new List<SpawnlableNPC>();
    private List<PathCombination> _listOfPathCombinations = new List<PathCombination>();
    public PathCombination PathCombination => _currentPathCombination;

    private List<Transform> _listOfStartPoints = new List<Transform>();
    private List<Transform> _listOfEndPoints = new List<Transform>();

    private void Awake()
    {
        instance = this;
        Invoke("GeneratePathsList", 1f);
        // InvokeRepeating("CalulateDistance", 1.5f, 1f);
    }
    void CalulateDistance()
    {
        var startPoint = _currentPathCombination.StartPoint;
        var endPoint = _currentPathCombination.EndPoint;

        if (_playerTransform != null)
        {
            var distance = 0f;
            var isCloseToStart = Vector3.Distance(_playerTransform.position, startPoint.position) < 1f;
            if (isCloseToStart)
            {
                _isDestinationReached = true;
            }
            if (!isCloseToStart)
            {
                distance = Vector3.Distance(_playerTransform.position, startPoint.position);

            }
            if (_isDestinationReached)
            {
                distance = Vector3.Distance(_playerTransform.position, endPoint.position);

            }

        }
    }
    private void Update()
    {
        if (_destination != null)
            HandleArrowDirection();
    }
    public void AddNode(Transform nodeTransform, WayPointNodeType nodeType)
    {
        if (nodeType == WayPointNodeType.Start)
        {

            _listOfStartPoints.Add(nodeTransform);
        }
        else
        {

            _listOfEndPoints.Add(nodeTransform);
        }
    }

    void GeneratePathsList()
    {

        for (int i = 0; i < _numberOfPaths; i++)
        {
            var randomStart = _listOfStartPoints[Random.Range(0, _listOfStartPoints.Count)];
            var randomEnd = _listOfEndPoints[Random.Range(0, _listOfEndPoints.Count)];
            var newPath = new PathCombination(randomStart, randomEnd);
            _listOfPathCombinations.Add(newPath);
        }
        GenerateNPCs();

    }

    void GenerateNPCs()
    {

        foreach (var path in _listOfPathCombinations)
        {
            var randomIndex = Random.Range(0, _listOfSpawnlableNPCs.Count);
            var randomNPCData = _listOfSpawnlableNPCs[randomIndex];
            var newNPC = Instantiate(randomNPCData.NPCPrefab, path.StartPoint.position, Quaternion.identity);
            newNPC.transform.parent = path.StartPoint;
            var randomSprite = randomNPCData.NPCPortrait;
            PhoneSystem.instance.SpawnClientRequestUI(randomSprite, path.Distance, path.Cost, randomIndex);
        }
    }
    private void HandleArrowDirection()
    {
        _destinationArrow.forward = (_destination.position - _destinationArrow.position).normalized;
    }

    public void SetDestination(Transform nextDestination)
    {
        _destination = nextDestination;
    }

    public void SetCurrentPathCombinationByIndex(int index)
    {
        _currentPathCombination = _listOfPathCombinations[index];
    }

}
