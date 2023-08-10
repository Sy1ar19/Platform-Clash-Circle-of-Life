using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DisplayDamage), typeof(EnemyAnimator))]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] protected int _health;

    public event Action Died;

    protected DisplayDamage _displayDamage;
    protected EnemyAnimator _enemyAnimator;
    protected bool _isAlive = true;

    public bool IsAlive => _isAlive;
    public int CurrentHealth { get; private set; }
    public float ReceivedDamage { get; private set; }

    private void Awake()
    {
        CurrentHealth = _health;
        _displayDamage = GetComponent<DisplayDamage>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
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

        //Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }
}
