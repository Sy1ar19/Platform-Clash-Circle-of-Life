using System;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    private const string MoneyKey = "PlayerMoney";
    private const string GoldMultiplierKey = "PlayerGoldMultiplier";
    private const string TotalMoneyKey = "PlayerTotalMoney";

    public event Action<int> MoneyChanged;

    [SerializeField] private WorkShop _workShop;

    [SerializeField] private int _money = 0;
    [SerializeField] private LoadSaveDataSystem _loadSaveDataSystem;
    private int _levelMoney;
    [SerializeField][Range(1, 2)] private int _goldMultiplier = 1;

    private int _totalMoney = 0;

    public int LevelMoney => _levelMoney;
    public int Money => _money;
    public int GoldMultiplier => _goldMultiplier;

    public int TotalMoney => _totalMoney;

    public int GetGoldMultiplier()
    {
        return 2;
    }

    private void Awake()
    {
        _money = SaveLoadSystem.LoadData<int>(MoneyKey, Money);
        _goldMultiplier = SaveLoadSystem.LoadData<int>(GoldMultiplierKey, _goldMultiplier);
        _totalMoney = SaveLoadSystem.LoadData(TotalMoneyKey, _totalMoney);
        _levelMoney = 0;
    }

    private void OnEnable()
    {
        _workShop.GoldMultiplierUpgraded += OnGoldMultiplierUpgraded;
    }

    private void OnDisable()
    {
        _workShop.GoldMultiplierUpgraded -= OnGoldMultiplierUpgraded;
    }

    private void OnGoldMultiplierUpgraded(int newGoldMultiplier)
    {
        _goldMultiplier = newGoldMultiplier;
        SaveLoadSystem.SaveData<int>(GoldMultiplierKey, _goldMultiplier);
    }

    public void EarnMoney(int amount)
    {
        _money += amount * _goldMultiplier;
        _totalMoney += amount;
        _levelMoney += amount * _goldMultiplier;
        MoneyChanged?.Invoke(amount * _goldMultiplier);
        _loadSaveDataSystem.SaveMoney(_money, _goldMultiplier);
        SaveLoadSystem.SaveData(TotalMoneyKey, _totalMoney);
    }

    public bool SpendMoney(int amount)
    {
        if (_money >= amount)
        {
            _money -= amount;
            MoneyChanged?.Invoke(-amount);
            _loadSaveDataSystem.SaveMoney(_money, _goldMultiplier);
            return true;
        }
        return false;
    }
}
