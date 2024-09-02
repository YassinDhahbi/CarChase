using UnityEngine;

public class AudioVolumeLinker : MonoBehaviour
{
    AudioSource _myAudioSource;
    AudioSource _targetAudioSource;

    private void Start()
    {
        _targetAudioSource = AudioManager.Instance.GetSFXPlayer();
        _myAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

        _myAudioSource.volume = _targetAudioSource.volume;
    }

}
