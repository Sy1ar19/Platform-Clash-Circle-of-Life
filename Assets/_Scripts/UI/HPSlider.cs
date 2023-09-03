using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private TMP_Text _healthText;

    private void Start()
    {
        if (_healthSlider == null || _playerHealth == null)
        {
            enabled = false;
            return;
        }

        UpdateExperienceSlider(_playerHealth.Health);
    }

    private void Update()
    {
        _healthSlider.value = _playerHealth.CurrentHealth;
    }

    private void OnEnable()
    {
        _playerHealth.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        UpdateExperienceSlider(health);
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= OnHealthChanged;
    }

    private void UpdateExperienceSlider(int health)
    {
        _healthSlider.maxValue = _playerHealth.MaxHealth;
        _healthSlider.value = health;
        _healthText.text = $"{health} / {_playerHealth.MaxHealth}";
    }
}
