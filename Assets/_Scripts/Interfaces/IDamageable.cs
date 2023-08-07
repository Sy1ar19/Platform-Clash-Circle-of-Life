public interface IDamageable
{
    bool WasAttacked { get; }
    float ReceivedDamage { get; }

    public void ApplyDamage(float damage);
}
