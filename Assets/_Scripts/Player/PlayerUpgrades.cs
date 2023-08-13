using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] private int initialDamage;
    [SerializeField] private int initialHealth;
    [SerializeField] private int initialArmor;

    private int currentDamage;
    private int currentHealth;
    private int currentArmor;

    private void Awake()
    {
        currentDamage = initialDamage;
        currentHealth = initialHealth;
        currentArmor = initialArmor;
    }

    public bool CanApplyUpgrade(int cost)
    {
        // Проверка, что игрок может применить апгрейд (достаточно средств и т.д.).
        return true; // Ваша логика проверки
    }

    public void Upgrade(int damageIncrease, int healthIncrease, int armorIncrease, int cost)
    {
        if (CanApplyUpgrade(cost))
        {
            currentDamage += damageIncrease;
            currentHealth += healthIncrease;
            currentArmor += armorIncrease;
            // Вычитание стоимости из игровой валюты или другие действия
        }
    }
}
