using System;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    private const string MoneyKey = "PlayerMoney";
    private const string GoldMultiplierKey = "PlayerGoldMultiplier";

    public event Action<int> MoneyChanged;

    [SerializeField] private WorkShop _workShop;

    [SerializeField] private int _money = 0;
    [SerializeField] private LoadSaveDataSystem loadSaveDataSystem;
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
    }

    public void EarnMoney(int amount)
    {
        _money += amount * _goldMultiplier;
        _levelMoney += amount * _goldMultiplier;
        MoneyChanged?.Invoke(amount * _goldMultiplier);
        loadSaveDataSystem.SaveMoney(_money, _goldMultiplier);
    }

    public bool SpendMoney(int amount)
    {
        if (_money >= amount)
        {
            _money -= amount;
            MoneyChanged?.Invoke(-amount);
            loadSaveDataSystem.SaveMoney(_money, _goldMultiplier);
            return true;
        }
        return false;
    }
}
