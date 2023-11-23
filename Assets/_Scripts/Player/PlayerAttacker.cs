using Assets._Scripts.Enemy;
using Assets._Scripts.Fight;
using Assets._Scripts.Interfaces;
using Assets._Scripts.Shop;
using System;
using UnityEngine;

namespace Assets._Scripts.Player
{
    [RequireComponent(typeof(PlayerMoney), typeof(PlayerAnimator), typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerExperience))]
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private EnemyDetector _enemyDetector;
        [SerializeField] private WorkShop _workShop;
        [SerializeField] private Weapon _weapon;

        private bool _isAttacking = false;
        private Coroutine _attackCoroutine;
        private PlayerAnimator _playerAnimator;
        private PlayerHealth _playerHealth;
        private PlayerMoney _playerMoney;
        private PlayerExperience _playerExperience;
        private IDamageable _enemy;

        public event Action MaxCriticalChanceReached;

        public IDamageable Enemy => _enemy;

        private void Awake()
        {
            _playerAnimator = GetComponent<PlayerAnimator>();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerMoney = GetComponent<PlayerMoney>();
            _playerExperience = GetComponent<PlayerExperience>();
        }

        private void OnEnable()
        {
            _enemyDetector.EnemyDetected += OnEnemyDetected;
            _enemyDetector.EnemyLost += OnEnemyLost;
        }

        private void OnDisable()
        {
            _enemyDetector.EnemyDetected -= OnEnemyDetected;
            _enemyDetector.EnemyLost -= OnEnemyLost;
        }

        public bool IsPlayerAlive()
        {
            return _playerHealth.IsAlive;
        }

        public void AddReward()
        {
            _playerMoney.EarnMoney(_enemy.MoneyReward);
            _playerExperience.AddExperience(_enemy.ExperienceReward);
        }

        public bool CheckIfEnemyIsBoss(IDamageable enemy)
        {
            if (enemy is BossHealth)
                return true;
            return false;
        }

        private void OnEnemyLost()
        {
            if (_isAttacking)
            {
                StopCoroutine(_attackCoroutine);
                _isAttacking = false;
            }

            _playerAnimator.PlayAttackAnimation(false);
            _enemy = null;
        }

        private void OnEnemyDetected(IDamageable enemy)
        {
            _enemy = enemy;
            _weapon.Shoot(enemy);
        }
    }
}