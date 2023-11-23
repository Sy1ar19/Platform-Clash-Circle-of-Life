using Assets._Scripts.Interfaces;
using Assets._Scripts.Shop;
using System;
using UnityEngine;

namespace Assets._Scripts.Player
{
    [RequireComponent(typeof(DisplayDamage), typeof(PlayerMover), typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _health = 50;
        [SerializeField] private int _armor;
        [SerializeField] private UpgradeSO _healthUpgrade;
        [SerializeField] private WorkShop _workShop;

        private const string HealthKey = "PlayerHealth";
        private const string ArmorKey = "PlayerArmor";

        public event Action Died;
        public event Action<int> HealthChanged;

        private bool _isAlive = true;
        private DisplayDamage _displayDamage;
        private PlayerMover _playerMover;
        private PlayerAnimator _playerAnimator;

        public bool IsAlive => _isAlive;
        public int CurrentHealth { get; private set; }
        public int Health => _health;
        public int MaxHealth { get; private set; }
        public int Armor => _armor;
        public int MoneyReward => throw new NotImplementedException();
        public int ExperienceReward => throw new NotImplementedException();

        private void Awake()
        {
            _health = SaveLoadSystem.LoadData(HealthKey, _health);
            _armor = SaveLoadSystem.LoadData(ArmorKey, Armor);
            CurrentHealth = _health;
            _displayDamage = GetComponent<DisplayDamage>();
            _playerMover = GetComponent<PlayerMover>();
            _playerAnimator = GetComponent<PlayerAnimator>();
            MaxHealth = _health;
        }

        private void OnEnable()
        {
            _workShop.HealthUpgraded += OnHealthUpgraded;
            _workShop.ArmorUpgraded += OnArmorUpgraded;
        }

        private void OnDisable()
        {
            _workShop.HealthUpgraded -= OnHealthUpgraded;
            _workShop.ArmorUpgraded -= OnArmorUpgraded;
        }

        public void ApplyDamage(int damage)
        {
            if (damage < 0)
                return;

            int damageTaken = Mathf.Max(0, damage - _armor);

            _health -= damageTaken;
            _displayDamage.SpawnPopup(damageTaken);

            CurrentHealth -= damageTaken;

            HealthChanged?.Invoke(CurrentHealth);

            if (CurrentHealth <= 0)
                Die();
        }

        public void Die()
        {
            _isAlive = false;
            _playerMover.StopMove();
            _playerAnimator.PlayDeathAnimation();
            Died?.Invoke();
        }

        private void OnArmorUpgraded()
        {
            _armor = SaveLoadSystem.LoadData(ArmorKey, Armor);
        }

        private void OnHealthUpgraded()
        {
            _health = SaveLoadSystem.LoadData(HealthKey, _health);
        }
    }
}