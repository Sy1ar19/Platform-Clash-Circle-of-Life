using UnityEngine;
using TMPro;

public class WeakingTrigger : MonoBehaviour
{
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _damageSound;

    private int _damage;

    private void Start()
    {
        _damage = Random.Range(_minDamage, _maxDamage);
        _text.text = "-" + _damage.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.ApplyDamage(_damage);

            if (_audioSource != null && _damageSound != null)
            {
                _audioSource.PlayOneShot(_damageSound);
            }
        }
    }
}
