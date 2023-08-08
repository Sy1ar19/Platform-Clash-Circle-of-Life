using System;
using UnityEngine;

[RequireComponent(typeof(DisplayDamage), typeof(Animator))]
public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _health;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _reward;

    public readonly int TakeDamage = Animator.StringToHash(nameof(TakeDamage));

    protected DisplayDamage _displayDamage;
    protected Animator _animator;

    public event Action Died;

    public float CurrentHealth { get; private set; }
    public bool WasAttacked { get; private set; }
    public float ReceivedDamage { get; private set; }

    private void Awake()
    {
        CurrentHealth = _health;
        _displayDamage = GetComponent<DisplayDamage>();
        _animator = GetComponent<Animator>();
    }

    public void ApplyDamage(float damage)
    {
        ReceivedDamage = damage;
        _displayDamage.SpawnPopup(damage);
        _animator.SetBool(TakeDamage, true);

        if (damage < 0)
            return;

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        Debug.Log("Die");
        Died?.Invoke();
        Destroy(gameObject);
    }
}
