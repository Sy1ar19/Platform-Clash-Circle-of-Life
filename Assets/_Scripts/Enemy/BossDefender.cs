using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CapsuleCollider))]
public class BossDefender : Enemy
{
    [SerializeField] private float _raycastDistance = 60f;
    [SerializeField] private Color _rayColor = Color.red;
    [SerializeField] private float _attackDelay = 1.0f;
    [SerializeField] private Transform _shootPoint;

    [SerializeField] private ParticleSystem _muzzleEffect;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private CapsuleCollider _capsuleCollider;

    private Player _target;
    private bool _isAttacking = false;
    private Coroutine _attackCoroutine;
    private float delay;

    private void Update()
    {
        //сделать таймер на автоатаку

        if (_canShoot && _isAlive)
        {
            CheckForPlayerDetection();
        }
    }

    public override void Die()
    {
        if (_target != null)
        {
            Debug.Log("Defender die");
            _target.AddMoney(_moneyReward);
            _target.AddExperience(_experienceReward);
            _animator.PlayDieAnimation();
            _capsuleCollider.enabled = false;
            _isAlive = false;
            _canShoot = false;
            enabled = false;
        }
    }

    private void CheckForPlayerDetection()
    {
        RaycastHit hit;

        if (Physics.Raycast(_shootPoint.position, transform.forward, out hit, _raycastDistance))
        {
            Debug.DrawRay(_shootPoint.position, transform.forward * _raycastDistance, _rayColor);

            _target = hit.collider.GetComponent<Player>();

            if (_target != null)
            {
                if (_target.CurrentHealth <= 0)
                {
                    _target = null;
                }
            }
            else
            {
                _target = null;
            }
        }
        else
        {
            Debug.DrawRay(_shootPoint.position, transform.forward * _raycastDistance, Color.green);
            _target = null;
        }

        if (_target != null && _isAttacking == false && _canShoot == true)
        {
            _attackCoroutine = StartCoroutine(AttackWithDelay(_target, _damage, _attackDelay));
            _animator.PlayAttackAnimation(true);
        }
    }

    private IEnumerator AttackWithDelay(Player target, float damage, float delay)
    {
        if(_isAlive)
        {
            _isAttacking = true;

            yield return new WaitForSeconds(delay);

            target.ApplyDamage(damage + UnityEngine.Random.Range(-1, 1));
            EffectUtils.PerformEffect(_muzzleEffect, _audioSource, _audioClip);

            _animator.PlayAttackAnimation(false);

            _isAttacking = false;
        }
    }

    private void OnDisable()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.ApplyDamage(player.CurrentHealth);
        }
    }

    private void Shoot()
    {

    }

}
