using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[CreateAssetMenu(fileName = "GoldMultiplierUpgradeData", menuName = "Upgrades/GoldMultiplierUpgradeData")]

public class GoldMultiplyerDataSO : UpgradeDataSO
{
    [SerializeField] private int _goldMultiplier;
    [SerializeField] private int _goldMultiplierIncreaseValue;

    public int GoldMultiplier => _goldMultiplier;

    public override void ApplyUpgrade()
    {
        _goldMultiplier += _goldMultiplierIncreaseValue;
        _cost += 500;
    }
    public int GetNextValue()
    {
        return _goldMultiplier + _goldMultiplierIncreaseValue;
    }
}
