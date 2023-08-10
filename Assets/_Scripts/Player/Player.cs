using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody), typeof(PlayerAnimator), typeof(EnemyDetector))]
[RequireComponent(typeof(DisplayDamage))]
public class Player : MonoBehaviour, IMovable, IDamageable, IAttackable
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay = 1.0f;
    [SerializeField] private int _maxExperience = 100;

    [SerializeField] private ParticleSystem _muzzleEffect;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    public event Action<int> MoneyIncreased;
    public event Action<int> ExperienceChanged;
    public event Action Died;

    private int _currentExperience = 0;
    private int _level = 1;
    private int _levelMoney = 0;
    private IDamageable _enemy;
    private Rigidbody _rigidbody;
    private PlayerAnimator _playerAnimator;
    private bool _canMove = true;
    private bool _isAttacking = false;
    private bool _isDied = false;
    public readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
    public readonly int IsDying = Animator.StringToHash(nameof(IsDying));
    public readonly int Win = Animator.StringToHash(nameof(Win));
    private Coroutine _attackCoroutine;
    protected DisplayDamage _displayDamage;
    private EnemyDetector _enemyDetector;

    public float CurrentExperience => _currentExperience;
    public float MaxExperience => _maxExperience;
    public float CurrentHealth { get; private set; }
    public bool IsAlive { get; private set; } = true;
    public float ReceivedDamage { get; private set; }
    public bool WasAttacked { get; private set; }
    public int Money { get; private set; }
    public int Level => _level;
    public int LevelMoney => _levelMoney;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        LoadPlayerProgress();
        _displayDamage = GetComponent<DisplayDamage>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _playerAnimator = GetComponent<PlayerAnimator>();
        CurrentHealth = _health;
        _levelMoney = 0;

        _enemyDetector = GetComponent<EnemyDetector>();
        _enemyDetector.EnemyDetected += OnEnemyDetected;
        _enemyDetector.EnemyLost += OnEnemyLost;
    }
    private void FixedUpdate()
    {
        if (_canMove)
        {
            Move();
        }
    }

    private void OnDisable()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
    }

    public void Die()
    {
        IsAlive = false;
        _isAttacking = false;
        StopMove();
        _playerAnimator.PlayDeathAnimation();
        Died?.Invoke();
    }

    public void StopMove()
    {
        _canMove = false;
    }

    public void Attack(IDamageable enemy, int damage)
    {
        enemy.ApplyDamage(damage);
    }

    public void ApplyDamage(float damage)
    {
        if (damage < 0)
            return;

        Debug.Log(damage);
        _displayDamage.SpawnPopup(damage);

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
            Die();
    }

    public void Move()
    {
        Vector3 forwardDirection = transform.forward * _movementSpeed;
        //_rigidbody.MovePosition(_rigidbody.position + forwardDirection * _movementSpeed * Time.deltaTime);
        _rigidbody.velocity = forwardDirection * _movementSpeed;
        //_rigidbody.AddForce(forwardDirection);
    }

    public void AddMoney(int money)
    {
        Money += money;
        _levelMoney += money;
        MoneyIncreased?.Invoke(Money);
        SavePlayerProgress();
    }

    public void AddExperience(int experience)
    {
        _currentExperience += experience;

        while (_currentExperience >= _maxExperience)
        {
            _currentExperience -= _maxExperience;
            _level++;
            _maxExperience *= 2;
        }

        ExperienceChanged?.Invoke(_currentExperience);

        SavePlayerProgress();
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

    private IEnumerator AttackWithDelay(IDamageable enemy, int damage, float delay)
    {
        _isAttacking = true;

        while (_enemy != null && _enemy.GetCurrentHealth() > 0 && IsAlive)
        {
            Debug.Log("Player shoot");
            enemy.ApplyDamage(damage + UnityEngine.Random.Range(-5, 5));
            EffectUtils.PerformEffect(_muzzleEffect, _audioSource, _audioClip);

            if (CheckIfEnemyIsBoss(_enemy))
            {
                _playerAnimator.PlayAttackAnimation(true);
            }

            yield return new WaitForSeconds(delay);
        }

        _playerAnimator.PlayAttackAnimation(false);
        _isAttacking = false;
        _enemy = null;
    }

    private void LoadPlayerProgress()
    {
        _level = PlayerPrefs.GetInt("Level", 1);
        _currentExperience = PlayerPrefs.GetInt("CurrentExperience", 0);
        _maxExperience = PlayerPrefs.GetInt("MaxExperience", _maxExperience);
        Money = PlayerPrefs.GetInt("Money", Money);
    }

    private bool CheckIfEnemyIsBoss(IDamageable enemy)
    {
        if (enemy is Boss)
            return true;
        return false;
    }

    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("Level", _level);
        PlayerPrefs.SetInt("CurrentExperience", _currentExperience);
        PlayerPrefs.SetInt("MaxExperience", _maxExperience);
        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.Save();
    }

    public void ApplyDamage(int damage)
    {
        throw new NotImplementedException();
    }

    public int GetCurrentHealth()
    {
        throw new NotImplementedException();
    }
}
