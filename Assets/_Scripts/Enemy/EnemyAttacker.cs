using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator), typeof(EnemyHealth))]
public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private float _attackDelay = 1.0f;
    [SerializeField] protected int _damage;
    [SerializeField] private ParticleSystem _muzzleEffect;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private CapsuleCollider _capsuleCollider;

    [SerializeField] private EnemyDetector _enemyDetector;

    private bool _isAttacking = false;
    protected EnemyAnimator _enemyAnimator;
    private IDamageable _enemy;
    private IDamageable _health;
    private Coroutine _attackCoroutine;

    public IDamageable Target => _enemy;

    private void OnEnable()
    {
        _enemyDetector.EnemyDetected += OnEnemyDetected;
        _enemyDetector.EnemyLost += OnEnemyLost;
    }

    private void Awake()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _health = GetComponent<EnemyHealth>();
    }

    private void OnEnemyLost()
    {
        if (_isAttacking)
        {
            StopCoroutine(_attackCoroutine);
            _isAttacking = false;
        }

        _enemyAnimator.PlayAttackAnimation(false);
        _enemy = null;
    }

    private void OnEnemyDetected(IDamageable enemy)
    {
        if (_isAttacking == false && _enemy == null)
        {
            _enemy = enemy;
            _attackCoroutine = StartCoroutine(AttackWithDelay(enemy, _damage, _attackDelay));
            _enemyAnimator.PlayAttackAnimation(true);
        }
    }

    private IEnumerator AttackWithDelay(IDamageable target, int damage, float delay)
    {
        while (_health.GetCurrentHealth() > 0 && target.GetCurrentHealth() > 0)
        {
            _isAttacking = true;
            _enemyAnimator.PlayAttackAnimation(_isAttacking);

            target.ApplyDamage(damage + UnityEngine.Random.Range(-1, 1));
            EffectUtils.PerformEffect(_muzzleEffect, _audioSource, _audioClip);

            yield return new WaitForSeconds(delay);

            _isAttacking = false;
            _enemyAnimator.PlayAttackAnimation(_isAttacking);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable player) && _health.GetCurrentHealth() > 0)
        {
            player.ApplyDamage(player.GetCurrentHealth());
        }
    }

    private void OnDisable()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }

        _enemyDetector.EnemyDetected -= OnEnemyDetected;
        _enemyDetector.EnemyLost -= OnEnemyLost;
    }
}
