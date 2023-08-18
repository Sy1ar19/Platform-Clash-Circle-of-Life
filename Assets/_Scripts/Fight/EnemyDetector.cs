using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public event System.Action<IDamageable> EnemyDetected;
    public event System.Action EnemyLost;
    private IDamageable _detectedEnemy;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable enemy = other.GetComponent<IDamageable>();

        if (enemy != null && enemy.GetCurrentHealth() > 0)
        {
            if (_detectedEnemy != enemy)
            {
                _detectedEnemy = enemy;
                EnemyDetected?.Invoke(enemy);
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
        }
    }
}
