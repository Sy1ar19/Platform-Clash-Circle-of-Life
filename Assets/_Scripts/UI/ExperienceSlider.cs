using Assets._Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI
{
    public class ExperienceSlider : MonoBehaviour
    {
        [SerializeField] private Slider _experienceSlider;
        [SerializeField] private TMP_Text _experienceText;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private PlayerExperience _playerExperience;
        [SerializeField] private Transform _experiencePosition;
        [SerializeField] private Transform _experienceMobilePosition;

        private void Start()
        {
            UpdateExperienceSlider(_playerExperience.CurrentExperience);
            CheckScreenOrientation();
        }

        private void OnExperienceChanged(int newExperience)
        {
            UpdateExperienceSlider(newExperience);
        }

        private void CheckScreenOrientation()
        {
            if (Screen.width > Screen.height)
            {
                transform.position = _experiencePosition.position;
            }
            else
            {
                transform.position = _experienceMobilePosition.position;
            }
        }

        private void UpdateExperienceSlider(float experience)
        {
            _experienceSlider.value = experience / _playerExperience.MaxExperience;
            _experienceText.text = $"{experience} / {_playerExperience.MaxExperience}";
            _levelText.text = $"{_playerExperience.Level}";
        }

        private void OnEnable()
        {
            _playerExperience.ExperienceChanged += OnExperienceChanged;
        }

        private void OnDisable()
        {
            _playerExperience.ExperienceChanged -= OnExperienceChanged;
        }
    }
}