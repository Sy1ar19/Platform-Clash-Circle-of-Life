public interface IDamageable
{

    public bool IsAlive { get; }
    public int Reward { get; }
    public void ApplyDamage(int damage);

    public int GetCurrentHealth();
}
