using UnityEngine;

public class StageProgress : MonoBehaviour
{
    private const string CurrentLevelKey = "CurrentLevel";
    private const string CompletedLevelPrefix = "CompletedLevel_";
    private const string CompletedLevelsKey = "CompletedLevels";
    private const string IsLevelCompleated = "IsLevelCompleted";

    [SerializeField] private BossHealth bossHealth;
    [SerializeField] private StageProgressUI stageProgressUI;
    [SerializeField] private int _level;

    private int _completedLevels = 0;
    private int _levelCompleted = 0;

    private void Start()
    {
        _levelCompleted = SaveLoadSystem.LoadData(IsLevelCompleated, _levelCompleted);
        _completedLevels = SaveLoadSystem.LoadData(CompletedLevelsKey, _completedLevels);
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
        if (SaveLoadSystem.LoadData(CompletedLevelPrefix + _level, 0) == 0)
        {
            Debug.Log(SaveLoadSystem.LoadData(CompletedLevelPrefix + _level, 0));

            _completedLevels++;
            SaveLoadSystem.SaveData(CompletedLevelPrefix + _level, _completedLevels);
            SaveLoadSystem.SaveData(CompletedLevelsKey, _completedLevels);
        }
    }
}
