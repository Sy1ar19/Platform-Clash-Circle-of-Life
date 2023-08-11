using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMoney), typeof(PlayerAnimator), typeof(PlayerHealth))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay = 1.0f;
    [SerializeField] private ParticleSystem _muzzleEffect;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private EnemyDetector _enemyDetector;

    private bool _isAttacking = false;
    private Coroutine _attackCoroutine;
    private PlayerAnimator _playerAnimator;
    private PlayerHealth _playerHealth;
    private PlayerMoney _playerMoney;
    private IDamageable _enemy;

    private void Awake()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerMoney = GetComponent<PlayerMoney>();
    }

    private void OnEnable()
    {
        _enemyDetector.EnemyDetected += OnEnemyDetected;
        _enemyDetector.EnemyLost += OnEnemyLost;
    }

    private void OnDisable()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
    }

    private void OnEnemyLost()
    {
        if (_isAttacking)
        {
            StopCoroutine(_attackCoroutine);
            _isAttacking = false;
        }

        _playerAnimator.PlayAttackAnimation(false);
        _enemy = null;
    }

    private void OnEnemyDetected(IDamageable enemy)
    {
        if (!_isAttacking && _enemy == null)
        {
            _enemy = enemy;
            _attackCoroutine = StartCoroutine(AttackWithDelay(enemy, _damage, _attackDelay));
        }
    }

    public void Attack(Enemy enemy)
    {
        // Вызов метода атаки у врага
    }

    private IEnumerator AttackWithDelay(IDamageable enemy, int damage, float delay)
    {
        _isAttacking = true;

        while (_enemy != null && _enemy.GetCurrentHealth() > 0 && _playerHealth.IsAlive)
        {
            enemy.ApplyDamage(damage + UnityEngine.Random.Range(-5, 5));
            EffectUtils.PerformEffect(_muzzleEffect, _audioSource, _audioClip);

            if (CheckIfEnemyIsBoss(_enemy))
            {
                _playerAnimator.PlayAttackAnimation(true);
            }

            if (enemy.IsAlive == false)
            {
                _playerMoney.EarnMoney(enemy.Reward);
            }

            yield return new WaitForSeconds(delay);
        }


        _playerAnimator.PlayAttackAnimation(false);
        _isAttacking = false;
        _enemy = null;
    }

    private bool CheckIfEnemyIsBoss(IDamageable enemy)
    {
        if (enemy is Boss)
            return true;
        return false;
    }
}
