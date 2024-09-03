using ArcadeVehicleController;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvolveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _item;
    [SerializeField] private Image _image;

    private float _animationDuration = .5f;

    public void UpdateUI(string evolveName,float startFillAmount, float targetAmont)
    {
        _item.text = evolveName;
        StartCoroutine(UpdateFillUI(_image, startFillAmount, targetAmont));
    }

    private IEnumerator UpdateFillUI(Image fillImage, float startFillAmount, float targetFillAmount)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _animationDuration)
        {
            fillImage.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, elapsedTime / _animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        fillImage.fillAmount = targetFillAmount;

    }
}
