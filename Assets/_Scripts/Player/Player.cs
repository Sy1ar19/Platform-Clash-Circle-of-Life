using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody), typeof(PlayerAnimations), typeof(EnemyDetector))]
[RequireComponent(typeof(DisplayDamage))]
public class Player : MonoBehaviour, IMovable, IDamageable, IAttackable
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackDelay = 1.0f;

    private Enemy _enemy;
    private Rigidbody _rigidbody;
    private PlayerAnimations _playerAnimations;
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
        _playerAnimations = GetComponent<PlayerAnimations>();
        CurrentHealth = _health;

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
        StopMove();
        _playerAnimations.PlayDeathAnimation();
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
        _playerAnimations.PlayVictoryAnimation();
    }

    private void OnEnemyLost()
    {
        if (_isAttacking)
        {
            StopCoroutine(_attackCoroutine);
            _isAttacking = false;
        }

        _playerAnimations.PlayAttackAnimation(false);
        _enemy = null;
    }

    private void OnEnemyDetected(Enemy enemy)
    {
        if (!_isAttacking && _enemy == null)
        {
            _enemy = enemy;
            _attackCoroutine = StartCoroutine(AttackWithDelay(enemy, _damage, _attackDelay));
            _playerAnimations.PlayAttackAnimation(true);
        }
    }

    private IEnumerator AttackWithDelay(Enemy enemy, float damage, float delay)
    {
        _isAttacking = true;

        while (_enemy != null && _enemy.CurrentHealth > 0)
        {
            enemy.ApplyDamage(damage + UnityEngine.Random.Range(-5, 5));
            _playerAnimations.PlayAttackAnimation(true);
            yield return new WaitForSeconds(delay);
        }

        _playerAnimations.PlayAttackAnimation(false);
        _isAttacking = false;
        _enemy = null;
    }

    public void Move()
    {
        Vector3 forwardDirection = transform.forward * _movementSpeed;
        //_rigidbody.MovePosition(_rigidbody.position + forwardDirection * _movementSpeed * Time.deltaTime);
        _rigidbody.velocity = forwardDirection * _movementSpeed;
        //_rigidbody.AddForce(forwardDirection);
    }
}
