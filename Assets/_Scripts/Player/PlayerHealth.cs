using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DisplayDamage), typeof(PlayerMover), typeof(PlayerAnimator))]
public class PlayerHealth : MonoBehaviour , IDamageable
{
    [SerializeField] private int _health;
    private bool _isAlive = true;
    private DisplayDamage _displayDamage;
    private PlayerMover _playerMover;
    private PlayerAnimator _playerAnimator;

    public event Action Died;

    public bool IsAlive => _isAlive;
    public int CurrentHealth { get; private set; }

    public int MoneyReward => throw new NotImplementedException();

    public int ExperienceReward => throw new NotImplementedException();

    private void Awake()
    {
        //_health = PlayerPrefs.GetInt("PlayerHelth");
        CurrentHealth = _health;
        _displayDamage = GetComponent<DisplayDamage>();
        _playerMover = GetComponent<PlayerMover>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            return;

        _health -= damage;
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
