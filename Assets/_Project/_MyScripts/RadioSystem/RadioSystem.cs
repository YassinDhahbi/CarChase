using System.Collections;
using System.Collections.Generic;

using DG.Tweening;
using TMPro;
using UnityEngine;


public class RadioSystem : Singelton<RadioSystem>
{
    [SerializeField] private List<RadioChannel> _listOfRadiochannels;
    [SerializeField] private int _currentChanelIndex = 0;
    AudioSource _audioSource;
    [Header("Text Dispaly")]
    [SerializeField] private TextMeshProUGUI _channelName;
    [SerializeField] private float _textUpdateRate = 0.1f;
    [SerializeField] private GameObject _textContainer;
    [SerializeField] private Vector3 _textContainerScale;
    private void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
        AnimateContainer();
        _textContainerScale = _textContainer.transform.localScale;
    }
    void Update()
    {
        ManageScrollBehaviour();
    }

    private void ManageScrollBehaviour()
    {
        if (Input.mouseScrollDelta.y == 0) return;
        if (Input.mouseScrollDelta.y > 0)
        {
            _currentChanelIndex = (_currentChanelIndex + 1) % _listOfRadiochannels.Count;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            _currentChanelIndex = (_currentChanelIndex - 1 + _listOfRadiochannels.Count) % _listOfRadiochannels.Count; ;
        }
        _audioSource.clip = _listOfRadiochannels[_currentChanelIndex].Clip;
        _audioSource.Play();
        AnimateContainer();
    }

    void AnimateContainer()
    {

        _textContainer.SetActive(true);
        _channelName.text = _listOfRadiochannels[_currentChanelIndex].Name;
        StartCoroutine(DeactivateTextContainer());

    }
    IEnumerator DeactivateTextContainer()
    {
        yield return new WaitForSeconds(2f);
        _textContainer.SetActive(false);
    }


    public void SetRadioVolume(float volume)
    {
        _audioSource.volume = volume;
    }
}

[System.Serializable]
public class RadioChannel
{
    [SerializeField] private string _name;
    [SerializeField] AudioClip _clip;

    public string Name => _name;
    public AudioClip Clip => _clip;
}
