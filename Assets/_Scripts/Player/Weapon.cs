using Assets._Scripts.Interfaces;
using Assets._Scripts.Shop;
using Assets._Scripts.UI;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Scripts.Player
{
    public class Weapon : MonoBehaviour
    {
        private const string DamageKey = "PlayerDamage";
        private const string CriticalChanceKey = "PlayerCriticalChance";
        private const string CriticalDamageKey = "PlayerCriticalDamage";
        private const int MinCriticalChance = 1;
        private const int MaxCriticalChance = 101;

        [SerializeField] private int _damage = 10;
        [SerializeField][Range(1, 100)] private int _criticalChance;
        [SerializeField] private int _criticalDamage;
        [SerializeField] private float _attackDelay = 1.0f;
        [SerializeField] private ParticleSystem _muzzleEffect;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private PlayerAttacker _playerAttacker;
        [SerializeField] private WorkShop _workShop;

        public event Action MaxCriticalChanceReached;
        public event Action<bool, int> CriticalHit;

        private Coroutine _attackCoroutine;
        private bool _isAttacking = false;
        private bool _isCriticalhit = false;

        public bool IsAttacking => _isAttacking;
        public bool IsCriticalHit => _isCriticalhit;
        public int Damage => _damage;
        public int CriticalChance => _criticalChance;
        public int CriticalDamage => _criticalDamage;

        private void Awake()
        {
            _damage = SaveLoadSystem.LoadData(DamageKey, _damage);
            _criticalChance = SaveLoadSystem.LoadData(CriticalChanceKey, CriticalChance);
            _criticalDamage = SaveLoadSystem.LoadData(CriticalDamageKey, CriticalDamage);
        }

        private void OnEnable()
        {
            _workShop.DamageUpgraded += OnDamageUpdated;
            _workShop.CriticalChanceUpgraded += OnCriticalChanceUpdated;
            _workShop.CriticalDamageUpgraded += OnCriticalDamageUpdated;
        }

        private void OnDisable()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }

            _workShop.DamageUpgraded -= OnDamageUpdated;
            _workShop.CriticalChanceUpgraded -= OnCriticalChanceUpdated;
            _workShop.CriticalDamageUpgraded -= OnCriticalDamageUpdated;
        }

        public void Shoot(IDamageable enemy)
        {
            if (_isAttacking == false && enemy != null)
            {
                _attackCoroutine = StartCoroutine(AttackWithDelay(enemy, _damage, _attackDelay));
            }
        }

        public int GetMaxCriticalChance()
        {
            return MaxCriticalChance - 1;
        }

        private IEnumerator AttackWithDelay(IDamageable enemy, int damage, float delay)
        {
            _isAttacking = true;

            while (_playerAttacker.Enemy != null && enemy.CurrentHealth > 0 && _playerAttacker.IsPlayerAlive())
            {

                int damageToDeal = damage;
                if (CheckForCriticalHit(_criticalChance))
                {
                    damageToDeal += _criticalDamage;
                    _isCriticalhit = true;

                    CriticalHit?.Invoke(true, damageToDeal);
                }

                enemy.ApplyDamage(damageToDeal);

                EffectUtils.PerformEffect(_muzzleEffect, _audioSource, _audioClip);

                _isCriticalhit = false;

                if (_playerAttacker.CheckIfEnemyIsBoss(enemy))
                {
                    _playerAnimator.PlayAttackAnimation(true);
                }

                if (enemy.IsAlive == false)
                {
                    _playerAttacker.AddReward();
                }

                yield return new WaitForSeconds(delay);
            }

            _playerAnimator.PlayAttackAnimation(false);
            _isAttacking = false;
        }

        private void OnDamageUpdated()
        {
            _damage = SaveLoadSystem.LoadData(DamageKey, Damage);
        }

        private void OnCriticalChanceUpdated()
        {
            _criticalChance = SaveLoadSystem.LoadData(CriticalChanceKey, CriticalChance);
            CheckMaxCriticalChance();
        }

        private void OnCriticalDamageUpdated()
        {
            _criticalDamage = SaveLoadSystem.LoadData(CriticalDamageKey, CriticalDamage);
        }

        private void CheckMaxCriticalChance()
        {
            if (_criticalChance >= GetMaxCriticalChance())
            {
                MaxCriticalChanceReached?.Invoke();
            }
        }

        private bool CheckForCriticalHit(int criticalChance)
        {
            int randomValue = UnityEngine.Random.Range(MinCriticalChance, MaxCriticalChance);
            return randomValue <= criticalChance;
        }
    }
}