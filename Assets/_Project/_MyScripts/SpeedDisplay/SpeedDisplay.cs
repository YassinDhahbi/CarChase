using ArcadeVehicleController;
using TMPro;
using UnityEngine;

public class SpeedDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private Vehicle _vehicle;
    private void Update()
    {
        _speedText.text = _vehicle.Velocity.magnitude.ToString("F0") + "KM/H";
    }
}
