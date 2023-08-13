using UnityEngine;

[CreateAssetMenu(fileName = "CriticalChanceUpgradeData", menuName = "Upgrades/CriticalChanceUpgradeData")]
public class CriticalChanceDataSO : UpgradeDataSO
{
    [SerializeField] private int _criticalChance;
    [SerializeField] private int _criticalChanceIncreaseValue;

    public int CriticalChance => _criticalChance;

    public override void ApplyUpgrade()
    {
        _criticalChance += _criticalChanceIncreaseValue;
        _cost += 30;
    }

    public int GetNextValue()
    {
        return _criticalChance + _criticalChanceIncreaseValue;
    }
}
