using UnityEngine;

public class DieDisplay : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private GameObject _dieDisplay;

    private void OnEnable()
    {
        _playerHealth.Died += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        _dieDisplay.SetActive(true);
    }

    private void OnDisable()
    {
        _playerHealth.Died -= OnPlayerDied;
    }
}
