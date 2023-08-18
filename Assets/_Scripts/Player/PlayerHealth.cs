using System;
using UnityEngine;

[RequireComponent(typeof(DisplayDamage), typeof(PlayerMover), typeof(PlayerAnimator))]
public class PlayerHealth : MonoBehaviour , IDamageable
{
    [SerializeField] private int _health = 50;
    [SerializeField ] private int _armor;
    [SerializeField] private Upgrade _healthUpgrade;

    private const string HealthKey = "PlayerHealth";
    private const string ArmorKey = "PlayerArmor";

    public event Action Died;

    private bool _isAlive = true;
    private DisplayDamage _displayDamage;
    private PlayerMover _playerMover;
    private PlayerAnimator _playerAnimator;

    public bool IsAlive => _isAlive;
    public int CurrentHealth { get; private set; }
    public int Health => _health;
    public int Armor => _armor;
    public int MoneyReward => throw new NotImplementedException();
    public int ExperienceReward => throw new NotImplementedException();

    private void Awake()
    {
        _health = SaveLoadSystem.LoadData<int>(HealthKey, _health) ;
        _armor = SaveLoadSystem.LoadData<int>(ArmorKey, Armor);
        CurrentHealth = _health;
        _displayDamage = GetComponent<DisplayDamage>();
        _playerMover = GetComponent<PlayerMover>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        _health = SaveLoadSystem.LoadData<int>(HealthKey, _health);
        _armor = SaveLoadSystem.LoadData<int>(ArmorKey, Armor);
    }

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            return;

        _health -= damage - _armor;
        _displayDamage.SpawnPopup(damage);

        CurrentHealth -= damage;

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
