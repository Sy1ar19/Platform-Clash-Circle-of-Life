using System;
using UnityEngine;

[RequireComponent(typeof(DisplayDamage), typeof(EnemyAnimator) ,typeof(EnemyAttacker))]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] protected int _health;
    [SerializeField] protected int _moneyReward;
    [SerializeField] protected int _experienceReward;

    public event Action Died;

    protected DisplayDamage _displayDamage;
    protected EnemyAnimator _enemyAnimator;
    protected EnemyAttacker _enemyAttacker;
    protected bool _isAlive = true;
    public int MoneyReward => _moneyReward;
    public int ExperienceReward => _experienceReward;

    public event Action<int> HealthChanged;

    public bool IsAlive => _isAlive;
    public int CurrentHealth { get; private set; }
    public float ReceivedDamage { get; private set; }
    public int MaxHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = _health;
        MaxHealth = _health;
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

        HealthChanged?.Invoke(CurrentHealth);

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
