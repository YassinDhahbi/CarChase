using System.Collections.Generic;
using ArcadeVehicleController;
using UnityEngine;

public class DeliverySystem : MonoBehaviour
{
    public static DeliverySystem Instance;
    [Header("Path Configuration")]
    [SerializeField] private PathCombination _currentPathCombination;
    [SerializeField]
    private List<PathCombination> _listOfPathCombinations = new List<PathCombination>();
    [SerializeField]
    private Transform _startPointsContainer;
    [SerializeField]
    private Transform _endPointsContainer;
    public PathCombination PathCombination => _currentPathCombination;
    [Header("Destination Arrow Configuration")]
    [SerializeField] private Transform _destinationArrow;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _destination;
    [Header("NPC Settings")]
    [SerializeField] private List<SpawnlableNPC> _listOfSpawnlableNPCs = new List<SpawnlableNPC>();
    [Header("Delivery Parameters")]
    [SerializeField] private AudioClip _cashSound;
    [SerializeField] private AudioClip _clientPickUpSound;



    Transform GetStartPointByIndex(int index) => index < _startPointsContainer.childCount ? _startPointsContainer.GetChild(index) : null;
    Transform GetEndPointByIndex(int index) => index < _endPointsContainer.childCount ? _endPointsContainer.GetChild(index) : null;


    private void Awake()
    {
        Instance = this;
        SetArrowState(false);
    }
    void Start()
    {
        GeneratePathsList();
    }

    private void Update()
    {
        if (_destination != null)
            HandleArrowDirection();
    }


    void GeneratePathsList()
    {
        for (int i = 0; i < _startPointsContainer.childCount; i++)
        {
            var randomStart = GetStartPointByIndex(i);
            var randomEnd = GetEndPointByIndex(i);
            var newPath = new PathCombination(randomStart, randomEnd);
            _listOfPathCombinations.Add(newPath);
        }
        GenerateNPCs();
    }

    void GenerateNPCs()
    {
        for (int i = 0; i < _listOfSpawnlableNPCs.Count; i++)
        {
            var npcData = _listOfSpawnlableNPCs[i];
            var path = _listOfPathCombinations[i];
            var newNPC = Instantiate(npcData.NPCPrefab, path.StartPoint.position, Quaternion.identity);
            newNPC.transform.parent = path.StartPoint;
            var randomSprite = npcData.NPCPortrait;
            PhoneSystem.Instance.SpawnClientRequestUI(randomSprite, path.Distance, path.Cost, i);
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

        for (int i = 0; i < _listOfPathCombinations.Count; i++)
        {
            var path = _listOfPathCombinations[i];
            path.StartPoint.gameObject.SetActive(false);
            path.EndPoint.gameObject.SetActive(false);
        }
        _currentPathCombination = _listOfPathCombinations[index];
        _currentPathCombination.ActivatePath();
        _destination = _currentPathCombination.StartPoint;
        Sprite characterPortrait = _listOfSpawnlableNPCs[index].NPCPortrait;
        DestinationPanelManager.Instance.SetClientPortrait(characterPortrait);

    }

    public void ManageDestinations(Transform reachedPoint)
    {
        if (PathCombination.StartPoint == reachedPoint)
        {
            _destination = PathCombination.EndPoint;
            PathCombination.EndPoint.gameObject.SetActive(true);
            DestinationPanelManager.Instance.ActivateDestinationPortrait();
            AudioManager.Instance.PlaySound(_clientPickUpSound);

        }
        else
        {
            var indexOfCurrentPathCombination = _listOfPathCombinations.IndexOf(PathCombination);
            PhoneSystem.Instance.RemoveClientRequestUI(indexOfCurrentPathCombination);
            MoneyManager.Instance.AddMoney(_currentPathCombination.Cost);
            _destination = null;
            _currentPathCombination = null;
            PhoneSystem.Instance.ActivatePhone();
            DestinationPanelManager.Instance.ToggleDestinationPanel();
            AudioManager.Instance.PlaySound(_cashSound);
        }

    }
    public void SetArrowState(bool state) => _destinationArrow.gameObject.SetActive(state);

    public Transform Destination => _destination;

}
