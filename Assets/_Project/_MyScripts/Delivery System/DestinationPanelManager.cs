using ArcadeVehicleController;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DestinationPanelManager : MonoBehaviour
{
    [Header("Portraits")]
    [SerializeField] private Image _clientPortrait;
    [SerializeField] private GameObject _clientPortraitContainer;
    [SerializeField] private GameObject _destinationPortraitContainer;
    [Header("Destination Parameters")]
    [SerializeField] private TextMeshProUGUI _destinationLabelTMP;
    [SerializeField] private TextMeshProUGUI _distanceValueTMP;
    [SerializeField] private Slider _distanceSlider;
    [Header("Labels")]
    [SerializeField] private string _clientLabel;
    [SerializeField] private string _destinationLabel;
    [Header("Panel")]
    [SerializeField] private Transform _mainDestinationPanel;
    [SerializeField] private float _startXPosition;
    [SerializeField] private float _endXPosition;
    private string _baseDestinationLabel;
    public static DestinationPanelManager Instance { get; private set; }
    private float _maxDistance = 1;
    private void Awake()
    {
        _baseDestinationLabel = _destinationLabelTMP.text;
        _clientPortraitContainer.SetActive(false);
        _destinationPortraitContainer.SetActive(false);
        Instance = this;
    }


    [ContextMenu("ShowPanel")]
    public void ToggleDestinationPanel()
    {
        var currentX = _mainDestinationPanel.position.x;
        var targetX = currentX == _startXPosition ? _endXPosition : _startXPosition;
        _mainDestinationPanel.DOMoveX(targetX, 0.5f).SetEase(Ease.InOutBack);

    }

    public void SetClientPortrait(Sprite portrait)
    {
        _clientPortrait.sprite = portrait;
        _clientPortraitContainer.SetActive(true);
        _destinationPortraitContainer.SetActive(false);
        _destinationLabelTMP.text = _baseDestinationLabel + " " + _clientLabel + ":";
        SetMaxDistance();

    }

    public void ActivateDestinationPortrait()
    {
        _destinationPortraitContainer.SetActive(true);
        _clientPortraitContainer.SetActive(false);
        _destinationLabelTMP.text = _baseDestinationLabel + " " + _destinationLabel + ":";
        SetMaxDistance();
    }

    void CalculateDistance()
    {
        var destination = DeliverySystem.Instance.Destination;
        if (destination == null) return;
        var playerPosition = PlayerController.Instance.Vehicle.transform.position;
        var distance = Vector3.Distance(playerPosition, destination.position);
        _distanceSlider.value = 1 - (distance / _maxDistance);
        _distanceValueTMP.text = distance.ToString("F1") + " M";
    }
    private void Update()
    {
        CalculateDistance();
    }
    public void SetMaxDistance()
    {
        var destination = DeliverySystem.Instance.Destination;
        if (destination == null) return;
        var playerPosition = PlayerController.Instance.Vehicle.transform.position;
        var distance = Vector3.Distance(playerPosition, destination.position);
        _maxDistance = distance;

    }
}
