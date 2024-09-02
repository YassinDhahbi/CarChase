using System.Collections.Generic;
using ArcadeVehicleController;
using UnityEngine;

public class CarEngineAdapter : MonoBehaviour
{
    [SerializeField]
    Vehicle _vehicle;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<float> _listOfMaxPitchBasedOnUpgrades;
    [SerializeField] private int _pitchIndex;
    [SerializeField] private float _currentPitchValue;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _speedThreshold;
    [SerializeField] private float _pitchDeltaMultiplier;

    float MaxPitch => _listOfMaxPitchBasedOnUpgrades[_pitchIndex];




    void ManagePitch()
    {
        _currentSpeed = _vehicle.Velocity.magnitude;
        var additionValue = _currentSpeed > _speedThreshold;
        _currentPitchValue += (additionValue ? Time.deltaTime : -Time.deltaTime) * _pitchDeltaMultiplier;
        _currentPitchValue = Mathf.Clamp(_currentPitchValue, 1, MaxPitch);
        _audioSource.pitch = _currentPitchValue;
    }

    private void Update()
    {
        ManagePitch();
    }

}
