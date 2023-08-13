using UnityEngine;

[CreateAssetMenu(fileName = "HealthUpgradeData", menuName = "Upgrades/HealthUpgradeData")]
public class HealthUpgradeDataSO : UpgradeDataSO
{
    [SerializeField] private int _health;
    [SerializeField] private int _healthIncreaseValue;

    public int Health => _health;

    public override void ApplyUpgrade()
    {
        _health += _healthIncreaseValue;
        _cost += 30;
        _healthIncreaseValue += 10;
    }

    public int GetNextValue()
    {
        return _health + _healthIncreaseValue;
    }
}
