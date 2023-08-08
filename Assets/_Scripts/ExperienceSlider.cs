using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceSlider : MonoBehaviour
{
    [SerializeField] private Slider _experienceSlider;
    [SerializeField] private TMP_Text _experienceText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private Player _player;

    private void Start()
    {

        UpdateExperienceSlider(_player.CurrentExperience);
    }

    private void OnExperienceChanged(int newExperience)
    {
        UpdateExperienceSlider(newExperience);
    }

    private void UpdateExperienceSlider(float experience)
    {
        _experienceSlider.value = experience / _player.MaxExperience;
        _experienceText.text = $"{experience} / {_player.MaxExperience}";
        _levelText.text = $"{_player.Level}";

    }

    private void OnEnable()
    {
        _player.ExperienceChanged += OnExperienceChanged;
    }

    private void OnDisable()
    {
        _player.ExperienceChanged -= OnExperienceChanged;
    }
}
