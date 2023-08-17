using UnityEngine;

[CreateAssetMenu(fileName = "CriticalChanceUpgradeData", menuName = "Upgrades/CriticalChanceUpgradeData")]
public class CriticalChanceDataSO : UpgradeDataSO
{
    [SerializeField] private int _criticalChance = 0;
    [SerializeField] private int _criticalChanceIncreaseValue;

    private const string CriticalChanceKey = "PlayerCriticalChance";

    public int CriticalChance => _criticalChance;

    private void Awake()
    {
        SaveLoadSystem.SaveData<int>(CriticalChanceKey, _criticalChance);
    }

    public override void ApplyUpgrade()
    {
        _criticalChance += _criticalChanceIncreaseValue;
        _cost += 30;
        SaveLoadSystem.SaveData<int>(CriticalChanceKey, _criticalChance);
    }

    public int GetNextValue()
    {
        return _criticalChance + _criticalChanceIncreaseValue;
    }
}
