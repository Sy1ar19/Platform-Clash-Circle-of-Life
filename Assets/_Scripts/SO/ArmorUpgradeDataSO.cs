using UnityEngine;

[CreateAssetMenu(fileName = "ArmorUpgradeData", menuName = "Upgrades/ArmorUpgradeData")]
public class ArmorUpgradeDataSO : UpgradeDataSO
{
    [SerializeField] private int _armor;
    [SerializeField] private int _armorIncreaseValue;

    public int Armor => _armor;

    public override void ApplyUpgrade()
    {
        _armor += _armorIncreaseValue;
        _cost += 30;
    }

    public int GetNextValue()
    {
        return _armor + _armorIncreaseValue;
    }
}
