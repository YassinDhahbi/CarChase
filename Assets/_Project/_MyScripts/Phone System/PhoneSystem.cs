using System;
using System.Collections;
using System.Collections.Generic;
using ArcadeVehicleController;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PhoneSystem : MonoBehaviour
{
    public static PhoneSystem Instance;

    [Header("Phone Button Animation Parameters")]
    [SerializeField] private Transform _phone;
    [SerializeField] private Button _phoneToggleButton;
    public Button PhoneToggleButton => _phoneToggleButton;
    [SerializeField] private float _animationDuration;
    [Header("Phone Request Spawn Parameters")]
    [SerializeField] private ClientRequestUI _clientRequestUIPrefab;
    [SerializeField] private Transform _requestSpawnParent;
    [SerializeField] private List<ClientRequestUI> _listOfClientsRequests;

    [Header("Stop Sign Parameters")]
    [SerializeField] private Transform _stopSign;
    [SerializeField] private float _stopSignAnimationDuration;
    [Header("Win")]
    [SerializeField] private GameObject _winPanel;



    private void Awake()
    {
        Instance = this;
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
        var isPlayerAbleToMove = PlayerController.Instance.enabled;
        PlayerController.Instance.enabled = !isPlayerAbleToMove;
        PlayerController.Instance.Vehicle.enabled = !isPlayerAbleToMove;
        var targetY = -_phone.position.y;
        _phone.transform.DOMoveY(targetY, _animationDuration).SetEase(Ease.InOutBack).OnComplete(() => UpdatePhoneStaus());
        _stopSign.gameObject.SetActive(isPlayerAbleToMove);


    }

    private void UpdatePhoneStaus()
    {
        var isDestinationValide = DeliverySystem.Instance.Destination != null;
        if (isDestinationValide == false)
        {
            ActivatePhone();
        }
        else
        {
            DeactivatePhone();
            DestinationPanelManager.Instance.ToggleDestinationPanel();

        }
    }

    public void SpawnClientRequestUI(Sprite image, int distance, int cost, int pathIndex)
    {
        var requestUI = Instantiate(_clientRequestUIPrefab, _requestSpawnParent);
        var distanceInMeters = distance.ToString() + " M";
        var costInCents = cost.ToString() + " $";
        requestUI.SetClientDetails(image, distanceInMeters, costInCents, pathIndex);
        _listOfClientsRequests.Add(requestUI);
    }

    public void ManageSelectedClientRequest(ClientRequestUI selectedRequest)
    {
        foreach (var item in _listOfClientsRequests)
        {
            if (item != null)
                item.CheckClientRequestSelection(selectedRequest);
        }
    }




    public void ActivatePhone()
    {
        _phoneToggleButton.interactable = true;

        DeliverySystem.Instance.SetArrowState(false);
    }
    public void DeactivatePhone()
    {
        _phoneToggleButton.interactable = false;
        DeliverySystem.Instance.SetArrowState(true);

    }

    public void RemoveClientRequestUI(int index)
    {
        Destroy(_listOfClientsRequests[index].gameObject);
        StartCoroutine(DelayedWin());
    }

    [ContextMenu("Test Win")]
    private void TestWinCondition()
    {
        var childCount = _requestSpawnParent.childCount;
        Debug.Log("ChildCOunt = " + childCount);
        if (childCount == 0)
        {
            _winPanel.SetActive(true);
            LevelData.LevelIndex++;
        }


        Debug.Log("Level Index = " + LevelData.LevelIndex);

    }

    IEnumerator DelayedWin()
    {
        yield return new WaitForSeconds(1f);
        TestWinCondition();
    }

}

public static class LevelData
{
    public static int LevelIndex;
    public static bool LogoLoaded = true;
}
