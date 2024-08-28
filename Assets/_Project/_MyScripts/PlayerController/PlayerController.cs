using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [Header("Movement Parameters")]
    [SerializeField] private float _normalMovementSpeed;
    [SerializeField] private float _sprintMovementSpeed;
    private Rigidbody _rigidbody;
    private float _currentMovementSpeed;
    private Vector3 _inputDirection;

    [Header("Camera Sensitivity Parameters")]
    [SerializeField] private float _mouseSensitivityX;
    [SerializeField] private float _mouseSensitivityY;
    [SerializeField] private float _xRotationClampMax;
    [SerializeField] private float _xRotationClampMin;
    [SerializeField] private Transform _cameraTransform;


    [Header("Shooting Parameters")]
    [SerializeField] private float _shootingRate;
    [SerializeField] private float _currentShootingCounter;
    [SerializeField] private ParticleSystem _shootingEffect;
    [SerializeField] private AudioSource _shootingSound;
    [SerializeField] private float _shootingSoundPitchMin;
    [SerializeField] private float _shootingSoundPitchMax;
    [SerializeField] private float _cameraShakeRate;
    [SerializeField] private float _shotGunImpactRadius;
    [SerializeField] private Transform _shotGunImpactArea;
    [SerializeField] private Image _reloadIndicator;
    [SerializeField] private TextMeshProUGUI _reloadText;



    private float _xRotation = 0f;


    private void Awake()
    {

        Cursor.lockState = CursorLockMode.Locked;
        _currentShootingCounter = 0;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
        HandleSpeed();
        HandleShooting();
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleCameraRotation();
    }

    void HandleMovement()
    {
        var movementDirection = transform.right * _inputDirection.x + _cameraTransform.forward * _inputDirection.z;
        movementDirection.y = 0;
        _rigidbody.linearVelocity = movementDirection * _currentMovementSpeed;
    }
    void HandleSpeed()
    {
        var isShiftPressed = Input.GetKey(KeyCode.LeftShift);
        var magnitude = _rigidbody.linearVelocity.magnitude;
        var isMoving = magnitude > 0.1f;
        var isSprinting = isShiftPressed && isMoving;
        _currentMovementSpeed = isSprinting ? _sprintMovementSpeed : _normalMovementSpeed;

        /// Handle Animations
        _playerAnimator.SetBool("IsSprinting", isSprinting);
        _playerAnimator.SetBool("IsMoving", isMoving);

    }
    void HandleInput()
    {
        _inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
    }

    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivityY;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -_xRotationClampMin, _xRotationClampMax);
        _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleShooting()
    {
        if (_currentShootingCounter >= _shootingRate)
        {
            _reloadText.text = "Shoot";
            _reloadText.color = Color.green;
            if (Input.GetMouseButtonDown(0))
            {
                ShootingBehaviour();
                DetectEnemies();

            }
            return;
        }

        _currentShootingCounter += Time.deltaTime;
        _reloadText.text = "Reloading...";
        _reloadText.color = Color.red;
        _reloadIndicator.fillAmount = _currentShootingCounter / _shootingRate;

    }

    private void ShootingBehaviour()
    {
        _shootingEffect.Play();
        PlayRandomPitchShootingSound();
        _currentShootingCounter = 0;
        _playerAnimator.SetTrigger("IsShooting");

    }

    void PlayRandomPitchShootingSound()
    {
        var randomPitch = Random.Range(_shootingSoundPitchMin, _shootingSoundPitchMax);
        _shootingSound.pitch = randomPitch;
        _shootingSound.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_shotGunImpactArea == null) return;
        Gizmos.DrawWireSphere(_shotGunImpactArea.position, _shotGunImpactRadius);
    }

    void DetectEnemies()
    {

        Collider[] hitColliders = Physics.OverlapSphere(_shotGunImpactArea.position, _shotGunImpactRadius);

        foreach (var hitCollider in hitColliders)
        {
            var enemy = hitCollider.GetComponent<ZombieEnemyAi>();
            if (enemy != null && enemy.enabled)
            {
                enemy.TakeDamage(10f);
            }
        }
    }


}
