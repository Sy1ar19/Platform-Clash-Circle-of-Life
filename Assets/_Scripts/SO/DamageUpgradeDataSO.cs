using UnityEngine;

[CreateAssetMenu(fileName = "DamageUpgradeData", menuName = "Upgrades/DamageUpgradeData")]
public class DamageUpgradeDataSO : UpgradeDataSO
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private int _damageIncreaseValue;

    private const string DamageKey = "PlayerDamage";

    public event System.Action<int> OnDamageChanged;

    public int Damage => _damage;

    private void Awake()
    {
        SaveLoadSystem.SaveData<int>(DamageKey, _damage);
    }

    public override void ApplyUpgrade()
    {
        _damage += _damageIncreaseValue;
        _cost += 50;
        _damageIncreaseValue += 10;
        OnDamageChanged?.Invoke(_damage);
        SaveLoadSystem.SaveData<int>(DamageKey, _damage);
    }

    public int GetNextValue()
    {
        return _damage + _damageIncreaseValue;
    }
}
