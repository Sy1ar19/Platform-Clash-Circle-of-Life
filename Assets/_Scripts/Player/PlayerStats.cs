using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
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

    [SerializeField] private DamageUpgradeDataSO _damageUpgradeData;
    [SerializeField] private HealthUpgradeDataSO _healthUpgradeData;
    [SerializeField] private ArmorUpgradeDataSO _armorUpgradeData;
    [SerializeField] private CriticalChanceDataSO _criticalChanceUpgradeData;
    [SerializeField] private CriticalDamageDataSO _criticalDamageUpgradeData;
    [SerializeField] private GoldMultiplyerDataSO _goldMultiplierUpgradeData;

    private void Start()
    {
        // При старте, установите начальные значения статистик из ScriptableObjects
        _damageText.text = _damageUpgradeData.Damage.ToString();
        _healthText.text = _healthUpgradeData.Health.ToString();
        _armorText.text = _armorUpgradeData.Armor.ToString();
        _criticalChanceText.text = _criticalChanceUpgradeData.CriticalChance.ToString();
        _criticalDamageText.text = _criticalDamageUpgradeData.CriticalDamage.ToString();
        _goldMultiplierText.text = _goldMultiplierUpgradeData.GoldMultiplier.ToString();
        _nextDamageText.text = _damageUpgradeData.GetNextValue().ToString();
        _nextHealthText.text = _healthUpgradeData.GetNextValue().ToString();
        _nextArmorText.text = _armorUpgradeData.GetNextValue().ToString();
        _nextCriticalChanceText.text = _criticalChanceUpgradeData.GetNextValue().ToString();
        _nextCriticalDamageText.text = _criticalDamageUpgradeData.GetNextValue().ToString();
        _nextGoldMultiplierText.text = _goldMultiplierUpgradeData.GetNextValue().ToString();
    }

    private void Update()
    {
        _damageText.text = _damageUpgradeData.Damage.ToString();
        _healthText.text = _healthUpgradeData.Health.ToString();
        _armorText.text = _armorUpgradeData.Armor.ToString();
        _criticalChanceText.text = _criticalChanceUpgradeData.CriticalChance.ToString();
        _criticalDamageText.text = _criticalDamageUpgradeData.CriticalDamage.ToString();
        _goldMultiplierText.text = _goldMultiplierUpgradeData.GoldMultiplier.ToString();
        _nextDamageText.text = _damageUpgradeData.GetNextValue().ToString();
        _nextHealthText.text = _healthUpgradeData.GetNextValue().ToString();
        _nextArmorText.text = _armorUpgradeData.GetNextValue().ToString();
        _nextCriticalChanceText.text = _criticalChanceUpgradeData.GetNextValue().ToString();
        _nextCriticalDamageText.text = _criticalDamageUpgradeData.GetNextValue().ToString();
        _nextGoldMultiplierText.text = _goldMultiplierUpgradeData.GetNextValue().ToString();
    }

/*    private void OnEnable()
    {
        _damageUpgradeData.OnDamageChanged += UpdateDamageText;
    }

    private void UpdateDamageText(int newDamage)
    {
        _damageText.text = newDamage.ToString();
    }*/

}
