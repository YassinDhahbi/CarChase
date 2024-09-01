using UnityEngine;
using UnityEngine.EventSystems;

public class UIClickSFX : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private AudioClip clickSFX;

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySound(clickSFX);
    }
}
