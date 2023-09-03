using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSlider : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    //[SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private TMP_Text _healthText;

    private IDamageable _enemy;

    private void Start()
    {
        _healthSlider.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //_enemyDetector.EnemyDetected += OnEnemyDetected;
        //_enemyDetector.EnemyLost += OnEnemyLost;
    }

    private void OnDisable()
    {
        //_enemyDetector.EnemyDetected -= OnEnemyDetected;
        //_enemyDetector.EnemyLost -= OnEnemyLost;

        if (_enemy != null)
        {
            _enemy.HealthChanged -= OnHealthChanged;
            _enemy = null;
        }
    }

    public void ActivateSlider(IDamageable enemy)
    {
        _enemy = enemy;
        _healthSlider.gameObject.SetActive(true);
        _healthText.text = $"{enemy.CurrentHealth} / {enemy.MaxHealth}";
        _healthSlider.maxValue = enemy.MaxHealth;
        enemy.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        UpdateHealth(health);
    }

    public void UpdateHealth(int currentHealth)
    {
        _healthSlider.value = currentHealth;
        _healthText.text = $"{_enemy.CurrentHealth} / {_enemy.MaxHealth}";
    }

    public void DeactivateSlider()
    {
        _healthSlider.gameObject.SetActive(false);
    }

/*    private void OnEnemyLost()
    {
        DeactivateSlider();
    }

    private void OnEnemyDetected(IDamageable enemy)
    {
        ActivateSlider(enemy);
        UpdateHealth(enemy.CurrentHealth); 
    }*/
}
