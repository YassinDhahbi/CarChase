using System.Collections;
using UnityEngine;

public class NPCStateSwitcher : MonoBehaviour
{
    Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    Transform _lookTarget;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _animator.SetBool("Wave", true);

            if (_lookTarget == null)
            {
                _lookTarget = other.transform;
            }

        }
    }
    private void Update()
    {
        if (_lookTarget != null)
        {
            transform.LookAt(_lookTarget);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _animator.SetBool("Wave", false);
            _audioSource.Stop();
            _lookTarget = null;
        }
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }
}
