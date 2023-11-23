namespace Assets._Scripts.Interfaces
{
    public interface IAttackable
    {
        void Attack(IDamageable enemy, int damage);
    }
}