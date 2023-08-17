using System;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    private const string ExperienceKey = "PlayerExperience";
    private const string MaxExperienceKey = "PlayerMaxExperience";
    private const string LevelKey = "PlayerLevel";

    [SerializeField] private int _maxExperience = 100;

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
/*        _currentExperience = PlayerPrefs.GetInt(ExperienceKey, 0);
        _maxExperience = PlayerPrefs.GetInt(MaxExperienceKey, _maxExperience);
        _level = PlayerPrefs.GetInt(LevelKey, _level);*/
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

        SaveExperience();
        ExperienceChanged?.Invoke(_currentExperience);
    }

    private void SaveExperience()
    {
        SaveLoadSystem.SaveData<int>(ExperienceKey, _currentExperience);
        SaveLoadSystem.SaveData<int>(MaxExperienceKey, _maxExperience);
        SaveLoadSystem.SaveData<int>(LevelKey, _level);
/*        PlayerPrefs.SetInt(ExperienceKey, _currentExperience);
        PlayerPrefs.SetInt(MaxExperienceKey, _maxExperience);
        PlayerPrefs.SetInt(LevelKey, _level);*/
    }
}
