using System;

namespace Assets._Scripts.Enemy
{
    public class BossHealth : EnemyHealth
    {
        public event Action BossDied;

        public override void Die()
        {
            BossDied?.Invoke();
            _isAlive = false;
            _enemyAnimator.PlayDieAnimation();
        }
    }
}