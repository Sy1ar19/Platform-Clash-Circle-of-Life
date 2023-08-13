using UnityEngine;

[CreateAssetMenu(fileName = "CriticalDamageUpgradeData", menuName = "Upgrades/CriticalDamageUpgradeData")]

public class CriticalDamageDataSO : UpgradeDataSO
{
    [SerializeField] private int _criticalDamage;
    [SerializeField] private int _criticalDamageIncreaseValue;

    public int CriticalDamage => _criticalDamage;

    public override void ApplyUpgrade()
    {
        _criticalDamage += _criticalDamageIncreaseValue;
        _cost += 30;
    }

    public int GetNextValue()
    {
        return _criticalDamage + _criticalDamageIncreaseValue;
    }
}
