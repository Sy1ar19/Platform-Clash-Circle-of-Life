using UnityEngine;

[RequireComponent(typeof(ShopUI))]
public class Shop : MonoBehaviour
{
    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerAttacker _playerAttacker;

    [SerializeField] private Upgrade _damageUpgrade;
    [SerializeField] private Upgrade _healthUpgrade;
    [SerializeField] private Upgrade _armorUpgrade;
    [SerializeField] private Upgrade _criticalChanceUpgrade;
    [SerializeField] private Upgrade _criticalDamageUpgrade;
    [SerializeField] private Upgrade _goldMultiplierUpgrade;

    private const string HealthKey = "PlayerHealth";
    private const string HealthPriceKey = "HealthPrice";
    private const string ArmorKey = "PlayerArmor";
    private const string ArmorPriceKey = "ArmorPrice";
    private const string DamageKey = "PlayerDamage";
    private const string DamagePriceKey = "DamagePrice";
    private const string CriticalChanceKey = "PlayerCriticalChance";
    private const string CriticalChancePriceKey = "CriticalChancePrice";
    private const string CriticalDamageKey = "PlayerCriticalDamage";
    private const string CriticalDamagePriceKey = "CriticalDamagePrice";
    private const string GoldMultiplierKey = "PlayerGoldMultiplier";
    private const string GoldMultiplierPriceKey = "GoldMultiplierKey";

    private int _healthPrice;
    private int _damagePrice;
    private int _armorPrice;
    private int _criticalChancePrice;
    private int _criticalDamagePrice;
    private int _goldMultiplierPrice;

    private ShopUI _shopUI;

    private void Awake()
    {
        _shopUI = GetComponent<ShopUI>();

        _healthPrice = SaveLoadSystem.LoadData(HealthPriceKey, _healthUpgrade.Price);
        _damagePrice = SaveLoadSystem.LoadData(DamagePriceKey, _damageUpgrade.Price);
        _armorPrice = SaveLoadSystem.LoadData(ArmorPriceKey, _armorUpgrade.Price);
        _criticalChancePrice = SaveLoadSystem.LoadData(CriticalChancePriceKey, _criticalChanceUpgrade.Price);
        _criticalDamagePrice = SaveLoadSystem.LoadData(CriticalDamagePriceKey, _criticalDamageUpgrade.Price);
        _goldMultiplierPrice = SaveLoadSystem.LoadData(GoldMultiplierPriceKey, _goldMultiplierUpgrade.Price);

        UpdateUI();
    }

    public void BuyUpgrade(Upgrade selectedUpgrade)
    {
        if (_playerMoney.Money >= selectedUpgrade.Price)
        {
            Debug.Log(selectedUpgrade.Title);
            if (selectedUpgrade == _healthUpgrade && _playerMoney.Money >= _healthPrice)
            {
                int newHealth = _playerHealth.Health + selectedUpgrade.IncreaseValue;
                SaveLoadSystem.SaveData<int>(HealthKey, newHealth);
                _playerMoney.SpendMoney(_healthPrice);
                _healthPrice += _healthUpgrade.Price;
                SaveLoadSystem.SaveData(HealthPriceKey, _healthPrice);

                UpdateUI();
            }
            else if (selectedUpgrade == _damageUpgrade && _playerMoney.Money >= _damagePrice)
            {
                int newDamage = _playerAttacker.Damage + selectedUpgrade.IncreaseValue;
                SaveLoadSystem.SaveData<int>(DamageKey, newDamage);
                _playerMoney.SpendMoney(_damagePrice);
                _damagePrice += _damageUpgrade.Price;
                SaveLoadSystem.SaveData(DamagePriceKey, _damagePrice);

                UpdateUI();
            }
            else if (selectedUpgrade == _armorUpgrade && _playerMoney.Money >= _armorPrice)
            {
                int newArmor = _playerHealth.Armor + selectedUpgrade.IncreaseValue;
                SaveLoadSystem.SaveData<int>(ArmorKey, newArmor);
                _playerMoney.SpendMoney(_armorPrice);
                _armorPrice += _armorUpgrade.Price;
                SaveLoadSystem.SaveData(ArmorPriceKey, _armorPrice);

                UpdateUI();
            }
            else if (selectedUpgrade == _criticalChanceUpgrade 
                && _playerMoney.Money >= _criticalChancePrice 
                && _playerAttacker.CriticalChance <  _playerAttacker.GetMaxCriticalChance())
            {
                int newCriticalChance = _playerAttacker.CriticalChance + selectedUpgrade.IncreaseValue;
                SaveLoadSystem.SaveData<int>(CriticalChanceKey, newCriticalChance);
                _playerMoney.SpendMoney(_criticalChancePrice);
                _criticalChancePrice += _criticalChanceUpgrade.Price;
                SaveLoadSystem.SaveData(CriticalChancePriceKey, _criticalChancePrice);
                UpdateUI();
            }
            else if (selectedUpgrade == _criticalDamageUpgrade && _playerMoney.Money >= _criticalDamagePrice)
            {
                int newCriticalDamage = _playerAttacker.CriticalDamage + selectedUpgrade.IncreaseValue;
                SaveLoadSystem.SaveData<int>(CriticalDamageKey, newCriticalDamage);
                _playerMoney.SpendMoney(_criticalDamagePrice);
                _criticalDamagePrice += _criticalDamageUpgrade.Price;
                SaveLoadSystem.SaveData(CriticalDamagePriceKey, _criticalDamagePrice);

                UpdateUI();
            }
            else if (selectedUpgrade == _goldMultiplierUpgrade && _playerMoney.Money >= _goldMultiplierPrice)
            {
                int newGoldMultiplier = _playerMoney.GoldMultiplier + selectedUpgrade.IncreaseValue;
                Debug.Log(newGoldMultiplier);
                SaveLoadSystem.SaveData<int>(GoldMultiplierKey, newGoldMultiplier);
                Debug.Log(SaveLoadSystem.LoadData<int>(GoldMultiplierKey, newGoldMultiplier));
                _playerMoney.SpendMoney(_goldMultiplierPrice);
                _goldMultiplierPrice += _goldMultiplierUpgrade.Price;
                SaveLoadSystem.SaveData(GoldMultiplierPriceKey, _goldMultiplierPrice);

                UpdateUI();
            }

            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        _shopUI.UpdateUI(_damagePrice, _healthPrice, _armorPrice, _criticalChancePrice, _criticalDamagePrice, _goldMultiplierPrice);
    }
}
