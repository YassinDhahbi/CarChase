using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PhoneSystem : MonoBehaviour
{
    public static PhoneSystem instance;

    [Header("Phone Button Animation Parameters")]
    [SerializeField] private Transform _phone;
    [SerializeField] private Button _phoneToggleButton;
    [SerializeField] private float _animationDuration;
    [Header("Phone Request Spawn Parameters")]
    [SerializeField] private ClientRequestUI _clientRequestUIPrefab;
    [SerializeField] private Transform _requestSpawnParent;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        _phoneToggleButton.onClick.AddListener(MovePanel);
    }
    private void OnDisable()
    {

        _phoneToggleButton.onClick.RemoveListener(MovePanel);
    }

    void MovePanel()
    {
        var targetY = -_phone.position.y;
        _phone.transform.DOMoveY(targetY, _animationDuration).SetEase(Ease.InOutBack);
    }
    public void SpawnClientRequestUI(Sprite image, int distance, int cost, int pathIndex)
    {
        var requestUI = Instantiate(_clientRequestUIPrefab, _requestSpawnParent);
        var distanceInMeters = distance.ToString() + " M";
        var costInCents = cost.ToString() + " $";
        requestUI.SetClientDetails(image, distanceInMeters, costInCents);
        // Add the on click behaviour to select the button and send its data to the delivery system
    }
}
