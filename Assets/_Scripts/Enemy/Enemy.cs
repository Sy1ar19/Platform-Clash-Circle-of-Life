using System;
using UnityEngine;

[RequireComponent(typeof(DisplayDamage), typeof(Animator), typeof(EnemyAnimator))]
public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _health;
    [SerializeField] protected float _damage;
    [SerializeField] protected int _moneyReward;
    [SerializeField] protected int _experienceReward;
    protected EnemyAnimator _animator;

    public readonly int TakeDamage = Animator.StringToHash(nameof(TakeDamage));
    public readonly int Win = Animator.StringToHash(nameof(Win));

    protected DisplayDamage _displayDamage;
    protected bool _canShoot = true;
    protected bool _isAlive = true;

    public event Action Died;

    public float CurrentHealth { get; private set; }
    public bool WasAttacked { get; private set; }
    public float ReceivedDamage { get; private set; }

    private void Awake()
    {
        CurrentHealth = _health;
        _displayDamage = GetComponent<DisplayDamage>();
        _animator = GetComponent<EnemyAnimator>();
    }

    public void ApplyDamage(float damage)
    {
        ReceivedDamage = damage;
        _displayDamage.SpawnPopup(damage);

        if (damage < 0)
            return;

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        Died?.Invoke();
        Destroy(gameObject);
    }
}
