using UnityEngine;

public class TopBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private GameObject _topBar;

    private void OnEnable()
    {
        _playerHealth.Died += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        _topBar.SetActive(false);
    }

    private void OnDisable()
    {
        _playerHealth.Died -= OnPlayerDied;
    }
}
