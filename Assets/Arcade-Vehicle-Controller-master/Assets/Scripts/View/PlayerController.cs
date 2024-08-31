using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcadeVehicleController
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vehicle m_Vehicle;
        public Vehicle Vehicle => m_Vehicle;
        public static PlayerController Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (m_Vehicle == null) return;
            m_Vehicle.SetSteerInput(Input.GetAxis("Horizontal"));
            m_Vehicle.SetAccelerateInput(Input.GetAxis("Vertical"));
        }
    }
}