using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(DisplayDamage))]
public class Player : MonoBehaviour, IMovable, IDamageable, IAttackable
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackDelay = 1.0f;

    private Enemy _enemy;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private bool _canMove = true;
    private bool _isAttacking = false;
    public readonly int IsAttacking = Animator.StringToHash(nameof(IsAttacking));
    public readonly int IsDying = Animator.StringToHash(nameof(IsDying));
    public readonly int Win = Animator.StringToHash(nameof(Win));
    private Coroutine _attackCoroutine;
    protected DisplayDamage _displayDamage;
    private EnemyDetector _enemyDetector;

    public float CurrentHealth { get; private set; }
    public bool IsAlive { get; private set; } = true;
    public float ReceivedDamage { get; private set; }
    public bool WasAttacked { get; private set; }

    private void Awake()
    {
        _displayDamage = GetComponent<DisplayDamage>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _animator = GetComponent<Animator>();
        CurrentHealth = _health;

        _enemyDetector = GetComponent<EnemyDetector>();
        _enemyDetector.EnemyDetected += OnEnemyDetected;
        _enemyDetector.EnemyLost += OnEnemyLost;
    }

    public void Die()
    {
        IsAlive = false;
        StopMove();
        _animator.SetBool(IsDying, true);
        Debug.Log("Die");
    }

    public void StopMove()
    {
        _canMove = false;
    }

    public void Attack(Enemy enemy, float damage)
    {
        enemy.ApplyDamage(damage);
    }

    public void ApplyDamage(float damage)
    {
        if (damage < 0)
            return;

        _displayDamage.SpawnPopup(damage);

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
            Die();
    }

    public void PlayVictoryAnimation()
    {
        _animator.SetBool(Win, true);
    }

    private void OnEnemyLost()
    {
        if (_isAttacking)
        {
            StopCoroutine(_attackCoroutine);
            _isAttacking = false;
        }

        _animator.SetBool(IsAttacking, false);
        _enemy = null;
    }

    private void OnEnemyDetected(Enemy enemy)
    {
        if (!_isAttacking && _enemy == null)
        {
            _enemy = enemy;
            _attackCoroutine = StartCoroutine(AttackWithDelay(enemy, _damage, _attackDelay));
            _animator.SetBool(IsAttacking, true);
        }
    }

    private IEnumerator AttackWithDelay(Enemy enemy, float damage, float delay)
    {
        _isAttacking = true;

        while (_enemy != null && _enemy.CurrentHealth > 0)
        {
            enemy.ApplyDamage(damage + UnityEngine.Random.Range(-5, 5));
            _animator.SetBool(IsAttacking, true);
            yield return new WaitForSeconds(delay);
        }

        _animator.SetBool(IsAttacking, false);
        _isAttacking = false;
        _enemy = null;
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

    public void Move()
    {
        Vector3 forwardDirection = transform.forward * _movementSpeed;
        //_rigidbody.MovePosition(_rigidbody.position + forwardDirection * _movementSpeed * Time.deltaTime);
        _rigidbody.velocity = forwardDirection * _movementSpeed;
        //_rigidbody.AddForce(forwardDirection);
    }
}
