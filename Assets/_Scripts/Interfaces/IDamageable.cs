public interface IDamageable
{

    public bool IsAlive { get; }
    public int MoneyReward { get; }
    public int ExperienceReward { get; }
    public void ApplyDamage(int damage);

    public int GetCurrentHealth();
}
