using UnityEngine;

[CreateAssetMenu(fileName = "CriticalDamageUpgradeData", menuName = "Upgrades/CriticalDamageUpgradeData")]

public class CriticalDamageDataSO : UpgradeDataSO
{
    [SerializeField] private int _criticalDamage = 0;
    [SerializeField] private int _criticalDamageIncreaseValue;

    private const string CriticalDamageKey = "PlayerCriticalDamage";

    public int CriticalDamage => _criticalDamage;

    private void Awake()
    {
        SaveLoadSystem.SaveData<int>(CriticalDamageKey, _criticalDamage);
    }

    public override void ApplyUpgrade()
    {
        _criticalDamage += _criticalDamageIncreaseValue;
        _cost += 30;
        _criticalDamageIncreaseValue += 5;
        SaveLoadSystem.SaveData<int>(CriticalDamageKey, _criticalDamage);
    }

    public int GetNextValue()
    {
        return _criticalDamage + _criticalDamageIncreaseValue;
    }
}
