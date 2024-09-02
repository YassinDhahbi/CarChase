using ArcadeVehicleController;
using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    [SerializeField] private VehicleSettings _vehicleSettingsSO;
    [SerializeField] private float _fuel = 100f;
    [SerializeField] private Image _fuelImage;
    [SerializeField] private Vehicle _vehicle;
    void Start()
    {
        _fuel = _vehicleSettingsSO.Fuel;
    }

    void Update()
    {
        if (_vehicle == null) return;
        if (_vehicle.Velocity.magnitude > 1f)
        {
            _fuel -= Time.deltaTime;
            _fuelImage.fillAmount = _fuel / _vehicleSettingsSO.Fuel;
        }

    }

}
