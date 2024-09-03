using System;
using System.Collections;
using ArcadeVehicleController;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Button _seetingsBtn;
    [SerializeField] private Button _backBtn;
    [SerializeField] private GameObject _settingsParameters;
    [SerializeField] private GameObject _pauseTitle;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _musicSlider;

    [SerializeField] private SceneLoader _sceneLoader;

    [Header("Reset Player")]
    Vector3 _resetPosition;

    private void Start()
    {
        _resetPosition = PlayerController.Instance.Vehicle.transform.position;
    }
    private void OnEnable()
    {
        _seetingsBtn.onClick.AddListener(OpenSettings);
        _backBtn.onClick.AddListener(CloseSettings);
        _sfxSlider.onValueChanged.AddListener(ManageSFXVolume);
        _musicSlider.onValueChanged.AddListener(ManageMusicVolume);
    }
    private void OnDisable()
    {
        _seetingsBtn.onClick.RemoveListener(OpenSettings);
        _backBtn.onClick.RemoveListener(CloseSettings);
        _sfxSlider.onValueChanged.RemoveListener(ManageSFXVolume);
        _musicSlider.onValueChanged.RemoveListener(ManageMusicVolume);
    }
    private void CloseSettings()
    {
        _settingsParameters.SetActive(false);
        _seetingsBtn.gameObject.SetActive(true);
        _pauseTitle.SetActive(true);
    }

    private void OpenSettings()
    {
        _settingsParameters.SetActive(true);
        _seetingsBtn.gameObject.SetActive(false);
        _pauseTitle.SetActive(false);

    }

    private void ManageSFXVolume(float value) => AudioManager.Instance.SetSFXVolume(value);
    private void ManageMusicVolume(float value) => AudioManager.Instance.SetMusicVolume(value);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var currentState = _pauseMenu.activeInHierarchy;
            _pauseMenu.SetActive(!currentState);
        }
    }

    public void Quit() => Application.Quit();

    public void LoadMainMneu()
    {
        LevelData.LogoLoaded = true;
    }

    public void ResetPlayer() => StartCoroutine(DelayedReset());

    IEnumerator DelayedReset()
    {
        PlayerController.Instance.Vehicle.enabled = false;
        PlayerController.Instance.Vehicle.transform.position = _resetPosition;
        PlayerController.Instance.Vehicle.transform.rotation = Quaternion.identity;
        var rb = PlayerController.Instance.Vehicle.GetComponent<Rigidbody>();
        rb.Sleep();
        yield return new WaitForSeconds(0.5f);
        PlayerController.Instance.Vehicle.enabled = true;
        rb.WakeUp();

    }
}
