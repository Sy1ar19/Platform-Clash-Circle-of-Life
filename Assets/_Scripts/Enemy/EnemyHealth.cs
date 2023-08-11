using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DisplayDamage), typeof(EnemyAnimator) ,typeof(EnemyAttacker))]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] protected int _health;
    [SerializeField] protected int _reward;

    public event Action Died;

    protected DisplayDamage _displayDamage;
    protected EnemyAnimator _enemyAnimator;
    protected EnemyAttacker _enemyAttacker;
    protected bool _isAlive = true;
    public int Reward => _reward;

    public bool IsAlive => _isAlive;
    public int CurrentHealth { get; private set; }
    public float ReceivedDamage { get; private set; }

    private void Awake()
    {
        CurrentHealth = _health;
        _displayDamage = GetComponent<DisplayDamage>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _enemyAttacker = GetComponent<EnemyAttacker>();
    }

    public void ApplyDamage(int damage)
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
        _isAlive = false;
        _enemyAnimator.PlayDieAnimation();
    }

    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }
}
