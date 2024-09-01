using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverSFX : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private AudioClip hoverSFXClip;


    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(hoverSFXClip);
    }

}
