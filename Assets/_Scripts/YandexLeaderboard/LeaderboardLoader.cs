using Agava.YandexGames;
using Assets._Scripts.Leaderboard;
using Lean.Localization;
using UnityEngine;

public class LeaderboardLoader : MonoBehaviour
{
    private const int MaxRecordsToShow = 10;
    private const string ENAnonymous = "Anonymous";
    private const string RUAnonymous = "Àíîíèì";
    private const string TRAnonymous = "Anonim";
    private const string Russian = "Russian";
    private const string English = "English";
    private const string Turkish = "Turkish";

    [SerializeField] private Record[] _records;
    [SerializeField] private PlayerRecord _playerRecord;
    [SerializeField] private LeanLocalization _localization;

    private readonly string _name = "money";

    public string Name => _name;

    private void Start()
    {
        DisableAllRecords();
#if UNITY_WEBGL && !UNITY_EDITOR
        LoadYandexLeaderboard();
#endif
    }

    private void DisableAllRecords()
    {
        _playerRecord.gameObject.SetActive(false);

        foreach (var record in _records)
        {
            record.gameObject.SetActive(false);
        }
    }

    private void LoadYandexLeaderboard()
    {
        Leaderboard.GetEntries(_name, (result) =>
        {
            int recordsToShow =
                result.entries.Length <= MaxRecordsToShow ? result.entries.Length : MaxRecordsToShow;

            for (int i = 0; i < recordsToShow; i++)
            {
                string name = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(name))
                {
                    switch (_localization.CurrentLanguage)
                    {
                        case Russian:
                            name = RUAnonymous;
                            break;
                        case English:
                            name = ENAnonymous;
                            break;
                        case Turkish:
                            name = TRAnonymous;
                            break;
                    }
                }

                _records[i].SetName(name);
                _records[i].SetScore(result.entries[i].formattedScore);
                _records[i].gameObject.SetActive(true);
            }
        });

        LoadPlayerScore();
    }

    private void LoadPlayerScore()
    {
        if (YandexGamesSdk.IsInitialized)
        {
            Leaderboard.GetPlayerEntry(_name, OnSuccessCallback);
        }
    }

    private void OnSuccessCallback(LeaderboardEntryResponse result)
    {
        if (result != null)
        {
            _playerRecord.gameObject.SetActive(true);

            if (result.player.publicName == null)
            {
                switch (_localization.CurrentLanguage)
                {
                    case Russian:
                        _playerRecord.SetName(RUAnonymous);
                        break;
                    case English:
                        _playerRecord.SetName(ENAnonymous);
                        break;
                    case Turkish:
                        _playerRecord.SetName(TRAnonymous);
                        break;
                }
            }
            else
            {
                _playerRecord.SetName(result.player.publicName);
            }

            _playerRecord.SetScore(result.score.ToString());
            _playerRecord.SetRank(result.rank);
        }
        else
        {
            _playerRecord.gameObject.SetActive(false);
        }
    }
}