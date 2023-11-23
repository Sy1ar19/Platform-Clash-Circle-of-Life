using Assets._Scripts.Enemy;
using UnityEngine;

namespace Assets._Scripts
{
    public class StageProgress : MonoBehaviour
    {
        private const string CompletedLevelPrefix = "CompletedLevel_";
        private const string CompletedLevelsKey = "CompletedLevels";
        private const string IsLevelCompleated = "IsLevelCompleted";

        [SerializeField] private BossHealth bossHealth;
        [SerializeField] private StageProgressUI stageProgressUI;
        [SerializeField] private int _level;

        private int _totalCompletedLevels = 0;
        private int _currentLevelCompleted = 0;

        private void Start()
        {
            _currentLevelCompleted = SaveLoadSystem.LoadData(IsLevelCompleated, _currentLevelCompleted);
            _totalCompletedLevels = SaveLoadSystem.LoadData(CompletedLevelsKey, _totalCompletedLevels);
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

                _totalCompletedLevels++;
                SaveLoadSystem.SaveData(CompletedLevelPrefix + _level, _totalCompletedLevels);
                SaveLoadSystem.SaveData(CompletedLevelsKey, _totalCompletedLevels);
            }
        }
    }
}