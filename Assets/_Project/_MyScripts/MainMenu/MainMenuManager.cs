using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjects;
    [SerializeField] private GameObject _vmCam;
    [SerializeField] private GameObject _startGameTxt;

    private void Awake()
    {
        StartGame(LevelData.LogoLoaded);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelData.LogoLoaded = false;
            StartGame(false);
        }
    }

    private void StartGame(bool isStart)
    {
        foreach (GameObject go in _gameObjects)
        {
            go.SetActive(!isStart);
        }
        _vmCam.SetActive(isStart);
        _startGameTxt.SetActive(isStart);
    }
}
