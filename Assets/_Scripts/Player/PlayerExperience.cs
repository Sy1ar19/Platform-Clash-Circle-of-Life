using System;
using UnityEngine;

namespace Assets._Scripts.Player
{
    public class PlayerExperience : MonoBehaviour
    {
        private const string ExperienceKey = "PlayerExperience";
        private const string MaxExperienceKey = "PlayerMaxExperience";
        private const string LevelKey = "PlayerLevel";

        [SerializeField] private int _maxExperience = 40;
        [SerializeField] private LoadSaveDataSystem _loadSaveDataSystem;
        [SerializeField] private int _currentExperience = 0;

        private readonly int _multiplier = 2;
        private int _level = 1;

        public event Action<int> ExperienceChanged;

        public int CurrentExperience => _currentExperience;
        public int MaxExperience => _maxExperience;
        public int Level => _level;

        private void Start()
        {
            _currentExperience = SaveLoadSystem.LoadData(ExperienceKey, CurrentExperience);
            _maxExperience = SaveLoadSystem.LoadData(MaxExperienceKey, MaxExperience);
            _level = SaveLoadSystem.LoadData(LevelKey, Level);
        }

        public void AddExperience(int experience)
        {
            _currentExperience += experience;

            while (_currentExperience >= _maxExperience)
            {
                _currentExperience -= _maxExperience;
                _level++;
                _maxExperience *= _multiplier;
            }

            _loadSaveDataSystem.SaveExperience(_currentExperience, _maxExperience, _level);
            ExperienceChanged?.Invoke(_currentExperience);
        }
    }
}