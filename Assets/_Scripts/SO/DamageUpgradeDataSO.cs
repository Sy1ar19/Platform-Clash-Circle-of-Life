using UnityEngine;

[CreateAssetMenu(fileName = "DamageUpgradeData", menuName = "Upgrades/DamageUpgradeData")]
public class DamageUpgradeDataSO : UpgradeDataSO
{
    [SerializeField] private int _damage;
    [SerializeField] private int _damageIncreaseValue;

    public event System.Action<int> OnDamageChanged;

    public int Damage => _damage;

    public override void ApplyUpgrade()
    {
        _damage += _damageIncreaseValue;
        _cost += 50;
        _damageIncreaseValue += 10;
        OnDamageChanged?.Invoke(_damage);
    }

    public int GetNextValue()
    {
        return _damage + _damageIncreaseValue;
    }
}
