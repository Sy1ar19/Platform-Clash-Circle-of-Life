using UnityEngine;

public class Money 
{
    private const string MoneyKey = "PlayerMoney";

    private int _amount;

    public int Amount => _amount;

    public Money()
    {
        _amount = PlayerPrefs.GetInt(MoneyKey, 0);
    }

    public void EarnMoney(int amount)
    {
        _amount += amount;
        SaveMoney();
    }

    public bool TrySpendMoney(int amount)
    {
        if (_amount >= amount)
        {
            _amount -= amount;
            SaveMoney();
            return true;
        }

        return false;
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt(MoneyKey, _amount);
    }
}
