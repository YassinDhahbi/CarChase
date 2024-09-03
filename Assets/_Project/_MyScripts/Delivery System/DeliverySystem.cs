using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    List<Transform> _listOfShuffledStartPoints = new List<Transform>();
    List<Transform> _listOfShuffledEndPoints = new List<Transform>();


    Transform GetStartPointByIndex(int index) => index < _startPointsContainer.childCount ? _listOfShuffledStartPoints[index] : null;
    Transform GetEndPointByIndex(int index) => index < _endPointsContainer.childCount ? _listOfShuffledEndPoints[index] : null;


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
        FillList(_listOfShuffledStartPoints, _startPointsContainer);
        FillList(_listOfShuffledEndPoints, _endPointsContainer);
        _listOfShuffledStartPoints.Shuffle();
        _listOfShuffledEndPoints.Shuffle();


        for (int i = 0; i < _listOfShuffledStartPoints.Count; i++)
        {
            var randomStart = GetStartPointByIndex(i);
            var randomEnd = GetEndPointByIndex(i);
            var newPath = new PathCombination(randomStart, randomEnd);
            _listOfPathCombinations.Add(newPath);
        }
        GenerateNPCs();
    }


    void FillList(List<Transform> _list, Transform source)
    {
        foreach (Transform child in source)
        {
            _list.Add(child);
        }
    }
    void GenerateNPCs()
    {
        Debug.Log("Level Index: " + LevelData.LevelIndex);
        LevelData.LevelIndex = Mathf.Clamp(LevelData.LevelIndex, 0, _listOfSpawnlableNPCs.Count - 1);
        for (int i = 0; i < LevelData.LevelIndex + 1; i++)
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


public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}