using System;
using UnityEngine;

[RequireComponent(typeof(WorkShopUI))]
public class WorkShop : MonoBehaviour
{
    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerAttacker _playerAttacker;
    [SerializeField] private Weapon _weapon;

    [SerializeField] private UpgradeSO _damageUpgrade;
    [SerializeField] private UpgradeSO _healthUpgrade;
    [SerializeField] private UpgradeSO _armorUpgrade;
    [SerializeField] private UpgradeSO _criticalChanceUpgrade;
    [SerializeField] private UpgradeSO _criticalDamageUpgrade;
    [SerializeField] private UpgradeSO _goldMultiplierUpgrade;

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

    private WorkShopUI _shopUI;

    public event Action HealthUpgraded;
    public event Action DamageUpgraded;
    public event Action ArmorUpgraded;
    public event Action CriticalChanceUpgraded;
    public event Action CriticalDamageUpgraded;
    public event Action<int> GoldMultiplierUpgraded;

    private void Awake()
    {
        _shopUI = GetComponent<WorkShopUI>();

        _healthPrice = SaveLoadSystem.LoadData(HealthPriceKey, _healthUpgrade.Price);
        _damagePrice = SaveLoadSystem.LoadData(DamagePriceKey, _damageUpgrade.Price);
        _armorPrice = SaveLoadSystem.LoadData(ArmorPriceKey, _armorUpgrade.Price);
        _criticalChancePrice = SaveLoadSystem.LoadData(CriticalChancePriceKey, _criticalChanceUpgrade.Price);
        _criticalDamagePrice = SaveLoadSystem.LoadData(CriticalDamagePriceKey, _criticalDamageUpgrade.Price);
        _goldMultiplierPrice = SaveLoadSystem.LoadData(GoldMultiplierPriceKey, _goldMultiplierUpgrade.Price);

        UpdateUI();
    }

    private void UpdateUI()
    {
        _shopUI.UpdateUI(_damagePrice, _healthPrice, _armorPrice, _criticalChancePrice, _criticalDamagePrice, _goldMultiplierPrice);
    }

    private void UpdatePlayerParameter(int newValue, ref int price, string parameterKey, string priceKey, UpgradeSO upgrade)
    {
        SaveLoadSystem.SaveData<int>(parameterKey, newValue);
        _playerMoney.SpendMoney(price);
        price += upgrade.Price;
        SaveLoadSystem.SaveData(priceKey, price);
        UpdateUI();
    }

    public void BuyUpgrade(UpgradeSO selectedUpgrade)
    {
        if (_playerMoney.Money >= selectedUpgrade.Price)
        {
            Debug.Log(selectedUpgrade.Title);

            if (selectedUpgrade == _healthUpgrade && _playerMoney.Money >= _healthPrice)
            {
                int newHealth = _playerHealth.Health + selectedUpgrade.IncreaseValue;
                UpdatePlayerParameter(newHealth, ref _healthPrice, HealthKey, HealthPriceKey, selectedUpgrade);
                HealthUpgraded?.Invoke();
            }
            else if (selectedUpgrade == _damageUpgrade && _playerMoney.Money >= _damagePrice)
            {
                int newDamage = _weapon.Damage + selectedUpgrade.IncreaseValue;
                UpdatePlayerParameter(newDamage, ref _damagePrice, DamageKey, DamagePriceKey, selectedUpgrade);
                DamageUpgraded?.Invoke();
            }
            else if (selectedUpgrade == _armorUpgrade && _playerMoney.Money >= _armorPrice)
            {
                int newArmor = _playerHealth.Armor + selectedUpgrade.IncreaseValue;
                UpdatePlayerParameter(newArmor, ref _armorPrice, ArmorKey, ArmorPriceKey, selectedUpgrade);
                ArmorUpgraded?.Invoke();
            }
            else if (selectedUpgrade == _criticalChanceUpgrade && _playerMoney.Money >= _criticalChancePrice && _weapon.CriticalChance 
                < _weapon.GetMaxCriticalChance())
            {
                int newCriticalChance = _weapon.CriticalChance + selectedUpgrade.IncreaseValue;
                UpdatePlayerParameter(newCriticalChance, ref _criticalChancePrice, CriticalChanceKey, CriticalChancePriceKey, selectedUpgrade);
                CriticalChanceUpgraded?.Invoke();
            }
            else if (selectedUpgrade == _criticalDamageUpgrade && _playerMoney.Money >= _criticalDamagePrice)
            {
                int newCriticalDamage = _weapon.CriticalDamage + selectedUpgrade.IncreaseValue;
                UpdatePlayerParameter(newCriticalDamage, ref _criticalDamagePrice, CriticalDamageKey, CriticalDamagePriceKey, selectedUpgrade);
                CriticalDamageUpgraded?.Invoke();
            }
            else if (selectedUpgrade == _goldMultiplierUpgrade && _playerMoney.Money >= _goldMultiplierPrice)
            {
                int newGoldMultiplier = _playerMoney.GoldMultiplier + selectedUpgrade.IncreaseValue;
                UpdatePlayerParameter(newGoldMultiplier, ref _goldMultiplierPrice, GoldMultiplierKey, GoldMultiplierPriceKey, selectedUpgrade);
                GoldMultiplierUpgraded?.Invoke(newGoldMultiplier);
            }
        }
    }

    /*public void BuyUpgrade(UpgradeSO selectedUpgrade)
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
    }*/


}