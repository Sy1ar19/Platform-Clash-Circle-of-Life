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

    [SerializeField] private Upgrade _damageUpgrade;
    [SerializeField] private Upgrade _healthUpgrade;
    [SerializeField] private Upgrade _armorUpgrade;
    [SerializeField] private Upgrade _criticalChanceUpgrade;
    [SerializeField] private Upgrade _criticalDamageUpgrade;
    [SerializeField] private Upgrade _goldMultiplierUpgrade;

    private int maxCriticalChance = 100;

    private void Update()
    {
        _damageText.text = _playerDamage.Damage.ToString();
        _healthText.text = _playerHealth.Health.ToString();
        _armorText.text = _playerArmor.Armor.ToString();
        _criticalChanceText.text = _playerCriticalChance.CriticalChance.ToString();
        _criticalDamageText.text = _playerCriticalDamage.CriticalDamage.ToString();
        _goldMultiplierText.text = _playerGoldMultiplier.GoldMultiplier.ToString();
        _nextDamageText.text = (_playerDamage.Damage + _damageUpgrade.IncreaseValue).ToString();
        _nextHealthText.text = (_playerHealth.Health + _healthUpgrade.IncreaseValue).ToString();
        _nextArmorText.text = (_playerHealth.Armor + _armorUpgrade.IncreaseValue).ToString();

        int nextCriticalChance = _playerDamage.CriticalChance + _criticalChanceUpgrade.IncreaseValue;

        if (nextCriticalChance >= maxCriticalChance + 1)
        {
            _nextCriticalChanceText.text = MaxValue;
        }
        else
        {
            _nextCriticalChanceText.text = nextCriticalChance.ToString();
        }

        _nextCriticalDamageText.text = (_playerDamage.CriticalDamage + _criticalDamageUpgrade.IncreaseValue).ToString();
        _nextGoldMultiplierText.text = (_playerGoldMultiplier.GoldMultiplier + _goldMultiplierUpgrade.IncreaseValue).ToString();
    }
}
