using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private const string MaxValue = "MAX";

    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _armorText;
    [SerializeField] private TextMeshProUGUI _criticalChanceText;
    [SerializeField] private TextMeshProUGUI _criticalDamageText;
    [SerializeField] private TextMeshProUGUI _goldMultiplierText;
    [SerializeField] private TextMeshProUGUI _nextDamageText;
    [SerializeField] private TextMeshProUGUI _nextHealthText;
    [SerializeField] private TextMeshProUGUI _nextArmorText;
    [SerializeField] private TextMeshProUGUI _nextCriticalChanceText;
    [SerializeField] private TextMeshProUGUI _nextCriticalDamageText;
    [SerializeField] private TextMeshProUGUI _nextGoldMultiplierText;

    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerHealth _playerArmor;
    [SerializeField] private PlayerAttacker _playerDamage;
    [SerializeField] private PlayerAttacker _playerCriticalChance;
    [SerializeField] private PlayerAttacker _playerCriticalDamage;
    [SerializeField] private PlayerMoney _playerGoldMultiplier;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private WorkShop _workShop;

    [SerializeField] private UpgradeSO _damageUpgrade;
    [SerializeField] private UpgradeSO _healthUpgrade;
    [SerializeField] private UpgradeSO _armorUpgrade;
    [SerializeField] private UpgradeSO _criticalChanceUpgrade;
    [SerializeField] private UpgradeSO _criticalDamageUpgrade;
    [SerializeField] private UpgradeSO _goldMultiplierUpgrade;

    private int maxCriticalChance = 100;

    private void Awake()
    {
        _damageText.text = _weapon.Damage.ToString();
        _healthText.text = _playerHealth.Health.ToString();
        _armorText.text = _playerArmor.Armor.ToString();
        _criticalChanceText.text = _weapon.CriticalChance.ToString();
        _criticalDamageText.text = _weapon.CriticalDamage.ToString();
        _goldMultiplierText.text = _playerGoldMultiplier.GoldMultiplier.ToString();
        _nextDamageText.text = (_weapon.Damage + _damageUpgrade.IncreaseValue).ToString();
        _nextHealthText.text = (_playerHealth.Health + _healthUpgrade.IncreaseValue).ToString();
        _nextArmorText.text = (_playerHealth.Armor + _armorUpgrade.IncreaseValue).ToString();

        int nextCriticalChance = _weapon.CriticalChance + _criticalChanceUpgrade.IncreaseValue;

        if (nextCriticalChance >= maxCriticalChance + 1)
        {
            _nextCriticalChanceText.text = MaxValue;
        }
        else
        {
            _nextCriticalChanceText.text = nextCriticalChance.ToString();
        }

        _nextCriticalDamageText.text = (_weapon.CriticalDamage + _criticalDamageUpgrade.IncreaseValue).ToString();
        _nextGoldMultiplierText.text = (_playerGoldMultiplier.GoldMultiplier + _goldMultiplierUpgrade.IncreaseValue).ToString();
    }

    private void OnEnable()
    {
        _weapon.MaxCriticalChanceReached += OnMaxCriticalChanceReached;
        _workShop.DamageUpgraded += OnDamageUpgraded;
        _workShop.HealthUpgraded += OnHealthUpgraded;
        _workShop.ArmorUpgraded += OnArmorUpgraded;
        _workShop.CriticalChanceUpgraded += OnCriticalChanceUpgraded;
        _workShop.CriticalDamageUpgraded += OnCriticalDamageUpgraded;
        _workShop.GoldMultiplierUpgraded += OnGoldMultiplierUpgraded;
    }

    private void OnDisable()
    {
        _weapon.MaxCriticalChanceReached -= OnMaxCriticalChanceReached;
        _workShop.DamageUpgraded -= OnDamageUpgraded;
        _workShop.HealthUpgraded -= OnHealthUpgraded;
        _workShop.ArmorUpgraded -= OnArmorUpgraded;
        _workShop.CriticalChanceUpgraded -= OnCriticalChanceUpgraded;
        _workShop.CriticalDamageUpgraded -= OnCriticalDamageUpgraded;
        _workShop.GoldMultiplierUpgraded -= OnGoldMultiplierUpgraded;
    }

    private void OnDamageUpgraded()
    {
        _damageText.text = _weapon.Damage.ToString();
        _nextDamageText.text = (_weapon.Damage + _damageUpgrade.IncreaseValue).ToString();
    }

    private void OnHealthUpgraded()
    {
        _healthText.text = _playerHealth.Health.ToString();
        _nextHealthText.text = (_playerHealth.Health + _healthUpgrade.IncreaseValue).ToString();
    }

    private void OnArmorUpgraded()
    {
        _armorText.text = _playerArmor.Armor.ToString();
        _nextArmorText.text = (_playerArmor.Armor + _armorUpgrade.IncreaseValue).ToString();
    }

    private void OnCriticalChanceUpgraded()
    {
        _criticalChanceText.text = _weapon.CriticalChance.ToString();
        int nextCriticalChance = _weapon.CriticalChance + _criticalChanceUpgrade.IncreaseValue;

        if (nextCriticalChance >= maxCriticalChance + 1)
        {
            _nextCriticalChanceText.text = MaxValue;
        }
        else
        {
            _nextCriticalChanceText.text = nextCriticalChance.ToString();
        }
    }

    private void OnCriticalDamageUpgraded()
    {
        _criticalDamageText.text = _weapon.CriticalDamage.ToString();
        _nextCriticalDamageText.text = (_weapon.CriticalDamage + _criticalDamageUpgrade.IncreaseValue).ToString();
    }

    private void OnGoldMultiplierUpgraded(int newGoldMultiplier)
    {
        _goldMultiplierText.text = newGoldMultiplier.ToString();
        _nextGoldMultiplierText.text = (_playerGoldMultiplier.GoldMultiplier + _goldMultiplierUpgrade.IncreaseValue).ToString();
    }

    private void OnMaxCriticalChanceReached()
    {
        _nextCriticalChanceText.text = MaxValue;
    }
}
