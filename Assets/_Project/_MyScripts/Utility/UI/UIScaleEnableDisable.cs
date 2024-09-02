using DG.Tweening;
using UnityEngine;

public class UIScaleEnableDisable : MonoBehaviour
{
    [SerializeField] private float _targetScale;
    [SerializeField] private float _animationDuration;
    public void ScaleUp()
    {
        transform.DOScale(_targetScale, _animationDuration).SetEase(Ease.OutBack);
    }
    public void ScaleDown()
    {
        transform.DOScale(0, _animationDuration).SetEase(Ease.InBack);
    }
}
