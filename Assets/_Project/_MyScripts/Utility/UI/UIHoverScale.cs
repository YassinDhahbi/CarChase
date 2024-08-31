using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;


public class UIHoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private float _targetScale = 1.1f;
    [SerializeField] private float _initialScale;
    [SerializeField] private float _animationDuration;
    private void Awake()
    {
        _initialScale = transform.localScale.x;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverExit();
    }

    public void HoverEnter()
    {
        transform.DOScale(_initialScale * _targetScale, _animationDuration).SetEase(Ease.OutBack);
    }
    public void HoverExit()
    {
        transform.DOScale(_initialScale, _animationDuration).SetEase(Ease.OutBack);
    }
}
