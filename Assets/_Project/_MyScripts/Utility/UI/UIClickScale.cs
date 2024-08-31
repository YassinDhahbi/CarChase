using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIClickScale : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float _targetScale = 1.1f;
    [SerializeField] private float _initialScale;
    [SerializeField] private float _animationDuration;
    [SerializeField] private bool _isAutomatic = true;
    private void Awake()
    {
        _initialScale = transform.localScale.x;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isAutomatic)
            ScaleUp();
    }
    public void ScaleUp()
    {
        transform.DOScale(_initialScale * _targetScale, _animationDuration).SetEase(Ease.OutBack);
    }
    public void ScaleDown()
    {
        transform.DOScale(_initialScale, _animationDuration).SetEase(Ease.OutBack);
    }
}


