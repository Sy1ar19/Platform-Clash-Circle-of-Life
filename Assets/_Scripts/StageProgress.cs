using UnityEngine;

public class StageProgress : MonoBehaviour
{
    private const string CurrentLevelKey = "CurrentLevel";
    private const string CompletedLevelPrefix = "CompletedLevel_";
    private const string CompletedLevelsKey = "CompletedLevels";
    private const string IsLevelCompleated = "IsLevelCompleted";

    [SerializeField] private BossHealth bossHealth;
    [SerializeField] private StageProgressUI stageProgressUI;

    private int _completedLevels = 0;
    private int _currentLevel = 0;
    private int _levelCompleted = 0;

    private void Start()
    {
        _currentLevel = SaveLoadSystem.LoadData(CurrentLevelKey, 0);
        _levelCompleted = SaveLoadSystem.LoadData(IsLevelCompleated, _levelCompleted);
    }

    private void OnEnable()
    {
        bossHealth.BossDied += OnBossDied;
    }

    private void OnDisable()
    {
        bossHealth.BossDied -= OnBossDied;
    }

    private void OnBossDied()
    {
        if (_levelCompleted == 0)
        {
            _completedLevels++;
            SaveLoadSystem.SaveData(CompletedLevelPrefix + _currentLevel, 1);
            SaveLoadSystem.SaveData(CurrentLevelKey, _currentLevel);
            SaveLoadSystem.SaveData(CompletedLevelsKey, _completedLevels);

            _levelCompleted = 1;
            SaveLoadSystem.SaveData(IsLevelCompleated, _levelCompleted);
        }
    }
}
