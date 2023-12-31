using UnityEngine;
using TMPro;
using Assets._Scripts.Player;

namespace Assets._Scripts
{
    public class DamageTrigger : MonoBehaviour
    {
        private const string dashMark = "-";

        [SerializeField] private int _minDamage;
        [SerializeField] private int _maxDamage;
        [SerializeField] private TextMeshPro _text;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _damageSound;

        private int _damage;

        private void Start()
        {
            _damage = Random.Range(_minDamage, _maxDamage);
            _text.text = dashMark + _damage.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.ApplyDamage(_damage);

                if (_audioSource != null && _damageSound != null)
                {
                    _audioSource.PlayOneShot(_damageSound);
                }
            }
        }
    }
}