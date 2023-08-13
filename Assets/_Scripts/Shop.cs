using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private PlayerMoney _player;
    [SerializeField] private DamageUpgradeDataSO _damageUpgrade; 
    [SerializeField] private HealthUpgradeDataSO _healthUpgrade;
    [SerializeField] private ArmorUpgradeDataSO _armorUpgrade;
    [SerializeField] private CriticalChanceDataSO _criticalChanceUpgrade;
    [SerializeField] private CriticalDamageDataSO _criticalDamageUpgrade;
    [SerializeField] private GoldMultiplyerDataSO _goldMultiplierUpgrade;

    [SerializeField] private TextMeshProUGUI _damagePrice;
    [SerializeField] private TextMeshProUGUI _healthPrice;
    [SerializeField] private TextMeshProUGUI _armorPrice;
    [SerializeField] private TextMeshProUGUI _criticalChancePrice;
    [SerializeField] private TextMeshProUGUI _criticalDamagePrice;
    [SerializeField] private TextMeshProUGUI _goldMultiplierPrice;

    public void BuyUpgrade(UpgradeDataSO selectedUpgrade)
    {
        if (_player.Money >= selectedUpgrade.Cost)
        {
            selectedUpgrade.ApplyUpgrade();
            _player.SpendMoney(selectedUpgrade.Cost);
        }
    }

    private void Update()
    {
        _damagePrice.text = _damageUpgrade.Cost.ToString();
        _healthPrice.text = _healthUpgrade.Cost.ToString();
        _armorPrice.text = _armorUpgrade.Cost.ToString();
        _criticalChancePrice.text = _criticalChanceUpgrade.Cost.ToString();
        _criticalDamagePrice.text = _criticalDamageUpgrade.Cost.ToString();
        _goldMultiplierPrice.text = _goldMultiplierUpgrade.Cost.ToString();
    }
}
