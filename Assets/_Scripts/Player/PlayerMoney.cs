using System;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    private const string MoneyKey = "PlayerMoney";
    private const string GoldMultiplierKey = "PlayerGoldMultiplier";

    public event Action<int> MoneyChanged;

    [SerializeField] private int _money = 0;
    private int _levelMoney;
    private int _goldMultiplier = 1;

    public int LevelMoney => _levelMoney;
    public int Money => _money;
    public int GoldMultiplier => _goldMultiplier;

    private void Awake()
    {
        _money = SaveLoadSystem.LoadData<int>(MoneyKey, Money);
        _goldMultiplier = SaveLoadSystem.LoadData<int>(GoldMultiplierKey, _goldMultiplier);
        _levelMoney = 0;
    }

    private void Update()
    {
        _goldMultiplier = SaveLoadSystem.LoadData<int>(GoldMultiplierKey, GoldMultiplier);
    }

    public void EarnMoney(int amount)
    {
        _money += amount * _goldMultiplier;
        _levelMoney += amount * _goldMultiplier;
        MoneyChanged?.Invoke(amount * _goldMultiplier);
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
        SaveLoadSystem.SaveData<int>(MoneyKey, _money);
        SaveLoadSystem.SaveData<int>(GoldMultiplierKey, _goldMultiplier);
    }
}
