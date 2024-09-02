using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    [SerializeField] private float _maxFuel = 100f;
    [SerializeField] private float _fuel = 100f;
    [SerializeField] private Image _fuelImage;
    void Start()
    {
        _fuel = _maxFuel;
    }

    void Update()
    {

        _fuelImage.fillAmount = _fuel / _maxFuel;
    }

}
