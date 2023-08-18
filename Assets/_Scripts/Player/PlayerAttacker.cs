using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMoney), typeof(PlayerAnimator), typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerExperience))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField][Range(1, 100)] private int _criticalChance;
    [SerializeField] private int _criticalDamage;
    [SerializeField] private float _attackDelay = 1.0f;
    [SerializeField] private ParticleSystem _muzzleEffect;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private EnemyDetector _enemyDetector;

    private const string DamageKey = "PlayerDamage";
    private const string CriticalChanceKey = "PlayerCriticalChance";
    private const string CriticalDamageKey = "PlayerCriticalDamage";
    private const int MinCriticalChance = 1;
    private const int MaxCriticalChance = 101;

    public event Action MaxCriticalChanceReached;

    private bool _isAttacking = false;
    private Coroutine _attackCoroutine;
    private PlayerAnimator _playerAnimator;
    private PlayerHealth _playerHealth;
    private PlayerMoney _playerMoney;
    private PlayerExperience _playerExperience;
    private IDamageable _enemy;

    public int Damage => _damage;
    public int CriticalChance => _criticalChance;
    public int CriticalDamage => _criticalDamage;

    private void Awake()
    {
        _damage = SaveLoadSystem.LoadData<int>(DamageKey, _damage);
        _criticalChance = SaveLoadSystem.LoadData<int>(CriticalChanceKey, CriticalChance);
        _criticalDamage = SaveLoadSystem.LoadData<int>(CriticalDamageKey, CriticalDamage);
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerMoney = GetComponent<PlayerMoney>();
        _playerExperience = GetComponent<PlayerExperience>();
    }

    private void Update()
    {
        _damage = SaveLoadSystem.LoadData<int>(DamageKey, Damage);
        _criticalChance = SaveLoadSystem.LoadData<int>(CriticalChanceKey, CriticalChance);
        _criticalDamage = SaveLoadSystem.LoadData<int>(CriticalDamageKey, CriticalDamage);

        CheckMaxCriticalChance();
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

    public void Attack(IDamageable enemy)
    {
        if (!_isAttacking && _enemy == null)
        {
            _enemy = enemy;
            _attackCoroutine = StartCoroutine(AttackWithDelay(enemy, _damage, _attackDelay));
        }
    }

    public int GetMaxCriticalChance()
    {
        return MaxCriticalChance - 1;
    }

    private void CheckMaxCriticalChance()
    {
        if (_criticalChance >= GetMaxCriticalChance())
        {
            MaxCriticalChanceReached?.Invoke();
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
        Attack(enemy);
    }

    private IEnumerator AttackWithDelay(IDamageable enemy, int damage, float delay)
    {
        _isAttacking = true;

        while (_enemy != null && _enemy.GetCurrentHealth() > 0 && _playerHealth.IsAlive)
        {

            int damageToDeal = damage;

            if (CheckForCriticalHit(_criticalChance))
            {
                damageToDeal += _criticalDamage;
            }

            enemy.ApplyDamage(damageToDeal);
            EffectUtils.PerformEffect(_muzzleEffect, _audioSource, _audioClip);

            if (CheckIfEnemyIsBoss(_enemy))
            {
                _playerAnimator.PlayAttackAnimation(true);
            }

            if (enemy.IsAlive == false)
            {
                _playerMoney.EarnMoney(enemy.MoneyReward);
                _playerExperience.AddExperience(enemy.ExperienceReward);
            }

            yield return new WaitForSeconds(delay);
        }

        _playerAnimator.PlayAttackAnimation(false);
        _isAttacking = false;
        _enemy = null;
    }

    private bool CheckIfEnemyIsBoss(IDamageable enemy)
    {
        if (enemy is BossHealth)
            return true;
        return false;
    }

    private bool CheckForCriticalHit(int criticalChance)
    {
        int randomValue = UnityEngine.Random.Range(MinCriticalChance, MaxCriticalChance);
        return randomValue <= criticalChance;
    }
}
