using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    public event Action BossDied;

    public override void Die()
    {
        BossDied?.Invoke();
        _isAlive = false;
        //_target.AddMoney(_moneyReward);
        //_target.AddExperience(_experienceReward);
        _enemyAnimator.PlayDieAnimation();
    }
}
