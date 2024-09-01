using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    AudioSource _audioSource;
    public static MoneyManager Instance;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }
}
