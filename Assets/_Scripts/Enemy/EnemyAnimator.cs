using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerHealth _player;

    private string _attackParamName = "IsAttacking";
    private string _winParamName = "Win";
    private string _dieParamName = "IsDying";

    private void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }
    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        PlayWinAnimation();
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
    }

    public void PlayAttackAnimation(bool isAttacking)
    {
        _animator.SetBool(_attackParamName, isAttacking);
    }

    public void PlayWinAnimation()
    {
        _animator.SetBool(_winParamName, true);
    }

    public void PlayDieAnimation()
    {
        _animator.SetBool(_dieParamName, true);
    }
}
