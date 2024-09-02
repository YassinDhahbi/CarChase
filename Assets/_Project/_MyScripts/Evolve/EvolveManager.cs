using ArcadeVehicleController;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EvolveManager : MonoBehaviour
{
    [SerializeField] private VehicleSettings _vehicleSettings;

    [SerializeField] private EvolveUI evolveUI;

    const string MAX_SPEED = "MaxSpeed";
    const string ACCELERTE_POWER = "AcceleratePower";
    const string STEER_HANDELER = "SteerHandeler";
    const string FUEL = "fuel";

    private string currentItem= "SteerHandeler";

    [Header("Vehicle Settings")]
    [SerializeField] private float _fuelMaxEvolve = 1000f;
    [SerializeField] private float _maxsSpeedEvolve = 40f;
    [SerializeField] private float _maxAcceleratePower = 400f;
    [SerializeField] private float _maxSteerAngle = 60;
    private void Start()
    {
        float targetAmont = _vehicleSettings.SteerAngle / _maxSteerAngle;
        evolveUI.UpdateUI(currentItem, targetAmont, targetAmont);

    }

    public void Evolve()
    {
        switch (currentItem)
        {
            case MAX_SPEED:
                MaxSpeed();
                break;
            case ACCELERTE_POWER:
                AcceleratePower();
                break;
            case STEER_HANDELER:
                SteerHandeler();
                break;
            case FUEL:
                Fuel();
                    break;
        }
        MoneyManager.Instance.AddMoney(-10);
    }
    private void Fuel()
    {
        if (_vehicleSettings.Fuel < _fuelMaxEvolve)
        {
            float startFillAmount = _vehicleSettings.Fuel / _fuelMaxEvolve;
            _vehicleSettings.SetFuel(50f);
            float targetAmont = _vehicleSettings.Fuel / _fuelMaxEvolve;
            evolveUI.UpdateUI(FUEL, startFillAmount, targetAmont);
        }
    }
    public void MaxSpeed()
    {
        if (_vehicleSettings.MaxSpeed < _maxsSpeedEvolve)
        {
            float startFillAmount = _vehicleSettings.MaxSpeed / _maxsSpeedEvolve;
            _vehicleSettings.SetMaxSpeed(5f);
            float targetAmont = _vehicleSettings.MaxSpeed / _maxsSpeedEvolve;
            evolveUI.UpdateUI("MaxSpeed", startFillAmount, targetAmont);
        }

    }

    public void AcceleratePower()
    {
        if (_vehicleSettings.AcceleratePower < _maxAcceleratePower)
        {
            float startFillAmount = _vehicleSettings.AcceleratePower / _maxAcceleratePower;
            _vehicleSettings.SetAcceleratePower(50f);
            float targetAmont = _vehicleSettings.AcceleratePower / _maxAcceleratePower;
            evolveUI.UpdateUI("AcceleratePower", startFillAmount, targetAmont);
        }
    }

    public void SteerHandeler()
    {
        if (_vehicleSettings.SteerAngle < _maxSteerAngle)
        {
            float startFillAmount = _vehicleSettings.SteerAngle / _maxSteerAngle;
            _vehicleSettings.SetSteerHandeling(10f);
            float targetAmont = _vehicleSettings.SteerAngle / _maxSteerAngle;
            evolveUI.UpdateUI("SteerHandeler", startFillAmount, targetAmont);
        }
    }

    public void SelectEvolce(string item)
    {
        float targetAmont = 0;
        switch (item)
        {
            case MAX_SPEED:
                targetAmont = _vehicleSettings.MaxSpeed / _maxsSpeedEvolve;

                break;
            case ACCELERTE_POWER:
                targetAmont = _vehicleSettings.AcceleratePower / _maxAcceleratePower;
                break;
            case STEER_HANDELER:
                targetAmont = _vehicleSettings.SteerAngle / _maxsSpeedEvolve;
                break;
            case FUEL:
                targetAmont = _vehicleSettings.Fuel / _fuelMaxEvolve;
                break;
        }

        currentItem = item;
        evolveUI.UpdateUI(item, 0, targetAmont);
    }
}
