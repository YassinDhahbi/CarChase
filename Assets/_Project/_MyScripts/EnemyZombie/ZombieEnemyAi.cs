using System;
using UnityEngine;
using UnityEngine.AI;

public class ZombieEnemyAi : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _target;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _chasingRange;
    [SerializeField] private float _hp = 100;
    [SerializeField] private AudioSource _zombieSFXPlayer;
    [SerializeField] private AudioClip _zombieImpactSFX;
    [SerializeField] private AudioClip _zombieHurtSFX;
    [SerializeField] private AudioClip _zombieDeathSFX;
    [SerializeField] private ParticleSystem _bloodEffect;
    [SerializeField] private ParticleSystem _deathBloodEffect;



    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        ChasingBehaviour();
    }

    void ChasingBehaviour()
    {
        if (Vector3.Distance(transform.position, _target.position) <= _chasingRange)
        {
            _animator.SetFloat("speed", 1);
            _navMeshAgent.SetDestination(_target.position);
        }
        else
        {
            _animator.SetFloat("speed", 0);
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chasingRange);
    }

    internal void TakeDamage(float _amount)
    {
        if (_hp > 0)
        {
            _hp -= _amount;
            _animator.SetTrigger("damage");
            _zombieSFXPlayer.PlayOneShot(_zombieImpactSFX);
            _zombieSFXPlayer.PlayOneShot(_zombieHurtSFX);
            _bloodEffect.Play();
        }
        else
        {
            _animator.SetTrigger("death");
            _navMeshAgent.isStopped = true;
            _zombieSFXPlayer.Stop();
            GetComponent<Collider>().enabled = false;
            _zombieSFXPlayer.PlayOneShot(_zombieDeathSFX);
            _deathBloodEffect.Play();

        }
    }
}


