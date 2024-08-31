using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcadeVehicleController
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vehicle m_Vehicle;



        private void Update()
        {

            if (m_Vehicle == null) return;
            m_Vehicle.SetSteerInput(Input.GetAxis("Horizontal"));
            m_Vehicle.SetAccelerateInput(Input.GetAxis("Vertical"));

        }
    }
}