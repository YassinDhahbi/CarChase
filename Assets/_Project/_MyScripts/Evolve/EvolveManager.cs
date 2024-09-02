using ArcadeVehicleController;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class EvolveManager : MonoBehaviour
{
    [SerializeField] private VehicleSettings _vehicleSettings;

    [SerializeField] private EvolveUI evolveUI;

    const string MAX_SPEED = "MaxSpeed";
    const string ACCELERTE_POWER = "AcceleratePower";
    const string STEER_HANDELER = "SteerHandeler";

    private string currentItem= "SteerHandeler";
    private void Start()
    {
        float targetAmont = _vehicleSettings.SteerAngle / 60;
        evolveUI.UpdateUI("SteerHandeler", targetAmont, targetAmont);

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
        }
    }

    [ContextMenu("Evolve")]
    public void MaxSpeed()
    {
        float startFillAmount = _vehicleSettings.MaxSpeed / 40;
        _vehicleSettings.SetMaxSpeed(5f);
        float targetAmont = _vehicleSettings.MaxSpeed / 40;
        evolveUI.UpdateUI("MaxSpeed", startFillAmount, targetAmont);

    }

    public void AcceleratePower()
    {
        if (_vehicleSettings.AcceleratePower < 400)
        {
            float startFillAmount = _vehicleSettings.AcceleratePower / 400;
            _vehicleSettings.SetAcceleratePower(50f);
            float targetAmont = _vehicleSettings.AcceleratePower / 400;
            evolveUI.UpdateUI("AcceleratePower", startFillAmount, targetAmont);
        }
    }

    public void SteerHandeler()
    {
        float startFillAmount = _vehicleSettings.SteerAngle / 60;
        _vehicleSettings.SetSteerHandeling(10f);
        float targetAmont = _vehicleSettings.SteerAngle / 60;
        evolveUI.UpdateUI("SteerHandeler", startFillAmount, targetAmont);
    }

    public void SelectEvolce(string item)
    {
        float targetAmont = 0;
        switch (item)
        {
            case MAX_SPEED:
                targetAmont = _vehicleSettings.MaxSpeed / 40;

                break;
            case ACCELERTE_POWER:
                targetAmont = _vehicleSettings.AcceleratePower / 400;
                break;
            case STEER_HANDELER:
                targetAmont = _vehicleSettings.SteerAngle / 60;
                break;
        }

        currentItem = item;
        evolveUI.UpdateUI(item, 0, targetAmont);
    }
}
