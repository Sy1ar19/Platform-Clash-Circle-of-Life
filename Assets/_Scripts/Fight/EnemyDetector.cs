using Assets._Scripts.Enemy;
using Assets._Scripts.Interfaces;
using Assets._Scripts.UI;
using UnityEngine;

namespace Assets._Scripts.Fight
{
    public class EnemyDetector : MonoBehaviour
    {
        [SerializeField] private EnemyHealthSlider _enemyHealthSlider;

        public event System.Action<IDamageable> EnemyDetected;
        public event System.Action EnemyLost;

        private IDamageable _detectedEnemy;

        private void OnTriggerEnter(Collider other)
        {
            IDamageable enemy = other.GetComponent<IDamageable>();

            if (enemy != null && enemy.CurrentHealth > 0)
            {
                if (_detectedEnemy != enemy)
                {
                    _detectedEnemy = enemy;
                    EnemyDetected?.Invoke(enemy);

                    if (other.GetComponent<EnemyHealth>() && other.GetComponent<BossHealth>())
                        _enemyHealthSlider.ActivateSlider(enemy);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IDamageable enemy = other.GetComponent<IDamageable>();

            if (enemy != null && enemy == _detectedEnemy)
            {
                _detectedEnemy = null;
                EnemyLost?.Invoke();
                if (other.GetComponent<EnemyHealth>() && other.GetComponent<BossHealth>())
                    _enemyHealthSlider.DeactivateSlider();
            }
        }
    }
}