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

    public void SetClientDetails(Sprite image, string distance, string cost)
    {
        _clientImage.sprite = image;
        _distanceTMP.text = distance;
        _costTMP.text = cost;
    }


}
