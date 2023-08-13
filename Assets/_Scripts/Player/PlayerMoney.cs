using System;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    private const string MoneyKey = "PlayerMoney";

    public event Action<int> MoneyChanged;

    private int _money;
    private int _levelMoney;

    public int LevelMoney => _levelMoney;
    public int Money => _money;

    private void Start()
    {
        _money = PlayerPrefs.GetInt(MoneyKey, 0);
        _levelMoney = 0;
    }

    public void EarnMoney(int amount)
    {
        _money += amount;
        _levelMoney += amount;
        MoneyChanged?.Invoke(amount);
        SaveMoney();
    }

    public bool SpendMoney(int amount)
    {
        if (_money >= amount)
        {
            _money -= amount;
            MoneyChanged?.Invoke(-amount);
            SaveMoney();
            return true;
        }
        return false;
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetInt(MoneyKey, _money);
    }
}
