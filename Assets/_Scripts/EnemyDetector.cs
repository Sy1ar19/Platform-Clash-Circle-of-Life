using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public event System.Action<Enemy> EnemyDetected;
    public event System.Action EnemyLost;

    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _raycastDistance = 60f;
    [SerializeField] private Color _rayColor = Color.red;

    private Enemy _detectedEnemy;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(_shootPoint.position, transform.forward, out hit, _raycastDistance))
        {
            Debug.DrawRay(_shootPoint.position, transform.forward * _raycastDistance, _rayColor);

            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy != null && enemy.CurrentHealth > 0)
            {
                if (_detectedEnemy != enemy)
                {
                    _detectedEnemy = enemy;
                    EnemyDetected?.Invoke(enemy);
                }
            }
            else
            {
                if (_detectedEnemy != null)
                {
                    _detectedEnemy = null;
                    EnemyLost?.Invoke();
                }
            }
        }
        else
        {
            Debug.DrawRay(_shootPoint.position, transform.forward * _raycastDistance, Color.green);
            if (_detectedEnemy != null)
            {
                _detectedEnemy = null;
                EnemyLost?.Invoke();
            }
        }
    }
}
