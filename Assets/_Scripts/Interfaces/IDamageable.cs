using System;

public interface IDamageable
{
    public bool IsAlive { get; }
    public int MoneyReward { get; }
    public int ExperienceReward { get; }
    public void ApplyDamage(int damage);

    public int MaxHealth { get; }

    public int CurrentHealth { get; }

    public event Action<int> HealthChanged;

    public int GetCurrentHealth();
}
