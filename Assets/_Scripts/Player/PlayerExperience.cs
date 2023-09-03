using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    private const string ExperienceKey = "PlayerExperience";
    private const string MaxExperienceKey = "PlayerMaxExperience";
    private const string LevelKey = "PlayerLevel";

    [SerializeField] private int _maxExperience = 100;
    [SerializeField] private LoadSaveDataSystem _loadSaveDataSystem;

    public event Action<int> ExperienceChanged;

    private int _totalCount;
    private int _currentExperience = 0;
    private int _level = 1;

    public int Experience => _totalCount;
    public int CurrentExperience => _currentExperience;
    public int MaxExperience => _maxExperience;
    public int Level => _level;

    private void Start()
    {
        _currentExperience = SaveLoadSystem.LoadData<int>(ExperienceKey, CurrentExperience);
        _maxExperience = SaveLoadSystem.LoadData<int>(MaxExperienceKey, MaxExperience);
        _level = SaveLoadSystem.LoadData<int>(LevelKey, Level);
    }

    public void AddExperience(int experience)
    {
        _currentExperience += experience;

        while (_currentExperience >= _maxExperience)
        {
            _currentExperience -= _maxExperience;
            _level++;
            _maxExperience *= 2;
        }

        _loadSaveDataSystem.SaveExperience(_currentExperience, _maxExperience, _level);
        ExperienceChanged?.Invoke(_currentExperience);
    }
}
