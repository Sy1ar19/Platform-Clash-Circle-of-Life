using Assets._Scripts.Shop;
using System;
using UnityEngine;

namespace Assets._Scripts.Player
{
    public class PlayerMoney : MonoBehaviour
    {
        private const string MoneyKey = "PlayerMoney";
        private const string GoldMultiplierKey = "PlayerGoldMultiplier";
        private const string TotalMoneyKey = "PlayerTotalMoney";
        private const int MaxGoldMultiplier = 2;

        [SerializeField] private WorkShop _workShop;
        [SerializeField] private int _money = 0;
        [SerializeField] private LoadSaveDataSystem _loadSaveDataSystem;
        [SerializeField][Range(1, 2)] private int _goldMultiplier = 1;

        public event Action<int> MoneyChanged;

        private int _levelMoney;
        private int _totalMoney = 0;

        public int LevelMoney => _levelMoney;
        public int Money => _money;
        public int GoldMultiplier => _goldMultiplier;
        public int TotalMoney => _totalMoney;
        public int MaxGoldMultiplierValue => MaxGoldMultiplier;

        private void Awake()
        {
            _money = SaveLoadSystem.LoadData(MoneyKey, Money);
            _goldMultiplier = SaveLoadSystem.LoadData(GoldMultiplierKey, _goldMultiplier);
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
            SaveLoadSystem.SaveData(GoldMultiplierKey, _goldMultiplier);
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

        public bool TrySpendMoney(int amount)
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
}