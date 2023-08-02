using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(DisplayDamage))]
public class Player : MonoBehaviour, IMovable, IDamageable, IAttackable
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _raycastDistance = 60f;
    [SerializeField] private Color _rayColor = Color.red;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackDelay = 1.0f;
    [SerializeField] private Transform _shootPoint;

    private Enemy _enemy;
    private Rigidbody _rigidbody;
    private bool _canMove = true;
    private Animator _animator;
    private bool _isAttacking = false;
    private Coroutine _attackCoroutine;
    protected DisplayDamage _displayDamage;

    public float CurrentHealth { get; private set; }
    public bool IsAlive { get; private set; } = true;
    public float ReceivedDamage { get; private set; }
    public bool WasAttacked {get; private set; }

    private void Awake()
    {
        _displayDamage = GetComponent<DisplayDamage>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
        _animator = GetComponent<Animator>();
        CurrentHealth = _health;
    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(_shootPoint.position, transform.forward, out hit, _raycastDistance))
        {
            Debug.DrawRay(_shootPoint.position, transform.forward * _raycastDistance, _rayColor);

            _enemy = hit.collider.GetComponent<Enemy>();

            if (_enemy != null)
            {
                Debug.Log("Enemy detected!");
                if (_enemy.CurrentHealth <= 0)
                {
                    _enemy = null;
                }
            }
            else
            {
                _enemy = null; 
            }
        }
        else
        {
            Debug.DrawRay(_shootPoint.position, transform.forward * _raycastDistance, Color.green);
            _enemy = null;
        }

        if (_enemy != null && !_isAttacking)
        {
            _attackCoroutine = StartCoroutine(AttackWithDelay(_enemy, _damage, _attackDelay));
            _animator.SetBool("IsAttacking", true);
        }
    }

    private IEnumerator AttackWithDelay(Enemy enemy, float damage, float delay)
    {
        _isAttacking = true;

        yield return new WaitForSeconds(delay); 

        enemy.ApplyDamage(damage + UnityEngine.Random.Range(-5,5));

        _animator.SetBool("IsAttacking", false);

        _isAttacking = false;
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

    public void Die()
    {
        IsAlive = false;
        StopMove();
        _animator.SetBool("Die", true);
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

    internal void PlayVictoryAnimation()
    {
        _animator.SetBool("Win", true);
    }
}
