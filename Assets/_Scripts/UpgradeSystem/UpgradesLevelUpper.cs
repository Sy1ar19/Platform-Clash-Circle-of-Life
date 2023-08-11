/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UpgradesLevelUpper
{
    private readonly IMoneyBank _moneyBank;

    public UpgradesLevelUpper(IMoneyBank moneyBank)
    {
        _moneyBank = moneyBank;
    }

    public bool CanLevelUp(PlayerUpgrade upgrade)
    {
        if (upgrade.IsMaxLevel())
        {
            return false;
        }

        var price = upgrade.NextPrice;
        return _moneyBank.CanSpendMoney(price);
    }

    public void LevelUp(PlayerUpgrade upgrade)
    {
        if(this.CanLevelUp(upgrade) == false)
        {
            throw new System.Exception($"Cannot level up {upgrade.Id}");
        }

        var price = upgrade.NextPrice;
        _moneyBank.SpendMoney(price);

        upgrade.IncrementLevel();
    }
}
*/