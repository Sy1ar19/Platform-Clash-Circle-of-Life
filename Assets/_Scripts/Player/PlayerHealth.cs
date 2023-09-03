using System;
using UnityEngine;

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
    private int _maxHealth;

    public bool IsAlive => _isAlive;
    public int CurrentHealth { get; private set; }
    public int Health => _health;
    public int MaxHealth { get; private set; }
    public int Armor => _armor;
    public int MoneyReward => throw new NotImplementedException();
    public int ExperienceReward => throw new NotImplementedException();

    private void Awake()
    {
        _health = SaveLoadSystem.LoadData<int>(HealthKey, _health);
        _armor = SaveLoadSystem.LoadData<int>(ArmorKey, Armor);
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

    private void OnArmorUpgraded()
    {
        _armor = SaveLoadSystem.LoadData<int>(ArmorKey, Armor);
    }

    private void OnHealthUpgraded()
    {
        _health = SaveLoadSystem.LoadData<int>(HealthKey, _health);
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

    private void Die()
    {
        _isAlive = false;
        _playerMover.StopMove();
        _playerAnimator.PlayDeathAnimation();
        Died?.Invoke();
    }

    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }
}
