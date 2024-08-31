using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientRequestUI : MonoBehaviour
{

    [Header("Client Details")]
    [SerializeField] private Image _clientImage;
    [SerializeField] private Button _clientButton;
    [Header("Course Details")]
    [SerializeField] private TextMeshProUGUI _distanceTMP;
    [SerializeField] private TextMeshProUGUI _costTMP;
    [Header("Background Button Paramters")]
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;
    [Header("Cost TMP Paramters")]
    [SerializeField] private Color _costTMPSelectedColor;
    [SerializeField] private Color _costTMPDeselectedColor;
    [Header("Indexation")]
    [SerializeField] private int _index;
    private void Awake()
    {
        CheckClientRequestSelection(null);
    }
    public void SetClientDetails(Sprite image, string distance, string cost, int index)
    {
        _clientImage.sprite = image;
        _distanceTMP.text = distance;
        _costTMP.text = cost;
        _index = index;
        _clientButton.onClick.AddListener(() => DeliverySystem.Instance.SetCurrentPathCombinationByIndex(_index));
        _clientButton.onClick.AddListener(() => PhoneSystem.Instance.ManageSelectedClientRequest(this));

    }


    private void OnDisable()
    {
        _clientButton.onClick.RemoveAllListeners();
    }

    public void CheckClientRequestSelection(ClientRequestUI request)
    {
        var isSameRequest = this == request;
        _clientButton.image.color = isSameRequest ? _activeColor : _inactiveColor;
        _costTMP.color = isSameRequest ? _costTMPSelectedColor : _costTMPDeselectedColor;
    }
    public Image GetBackgroundImage() => _clientButton.image;
}
