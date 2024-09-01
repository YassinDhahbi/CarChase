using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenClosePhoneBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Button Settings")]
    [SerializeField] private Button _btn;
    [SerializeField] private TextMeshProUGUI _btnText;
    [SerializeField] private bool _isOpen = false;
    [SerializeField] private string _textOpen = "Open Phone";
    [SerializeField] private Color _openColor;
    [SerializeField] private string _textClose = "Close Phone";
    [SerializeField] private Color _closeColor;

    [Header("Warning Settings")]
    [SerializeField] private GameObject _warning;
    private void Awake()
    {
        _btn = GetComponent<Button>();
        _btnText = _btn.GetComponentInChildren<TextMeshProUGUI>();
        ManageColors();
    }

    private void OnEnable()
    {
        _btn.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _isOpen = !_isOpen;
        ManageColors();
    }

    private void ManageColors()
    {
        _btnText.text = _isOpen ? _textClose : _textOpen;
        _btn.image.color = _isOpen ? _closeColor : _openColor;
    }

    private void OnDisable()
    {
        _btn.onClick.RemoveListener(OnClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_btn.interactable == false)
        {
            _warning.SetActive(true);

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_btn.interactable == false)
        {
            _warning.SetActive(false);

        }
    }
}