using System;
using UnityEngine;
using UnityEngine.UI;

public class RadioSliderUpdater : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float arg0)
    {
        RadioSystem.Instance.SetRadioVolume(arg0);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
