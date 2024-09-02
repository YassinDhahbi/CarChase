using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjects;
    [SerializeField] private GameObject _vmCam;
    [SerializeField] private GameObject _startGameTxt;

    private void Awake()
    {
        foreach (GameObject go in _gameObjects)
        {
            go.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        foreach (GameObject go in _gameObjects)
        {
            go.SetActive(true);
        }
        _vmCam.SetActive(false);
        _startGameTxt.SetActive(false);
    }
}
