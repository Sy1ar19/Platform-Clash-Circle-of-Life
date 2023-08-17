using UnityEngine;

[CreateAssetMenu(fileName = "ArmorUpgradeData", menuName = "Upgrades/ArmorUpgradeData")]
public class ArmorUpgradeDataSO : UpgradeDataSO
{
    [SerializeField] private int _armor;
    [SerializeField] private int _armorIncreaseValue;

    private const string ArmorKey = "PlayerArmor";

    public int Armor => _armor;

    private void Awake()
    {
        SaveLoadSystem.SaveData<int>(ArmorKey, _armor);
    }

    public override void ApplyUpgrade()
    {
        _armor += _armorIncreaseValue;
        _cost += 30;
        SaveLoadSystem.SaveData<int>(ArmorKey, _armor);
    }

    public int GetNextValue()
    {
        return _armor + _armorIncreaseValue;
    }
}
