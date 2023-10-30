using Agava.YandexGames;
using UnityEngine;

public class LeaderboardUpdate : MonoBehaviour
{
    [SerializeField] private PlayerMoney _money;

    private readonly string _leaderboardName = "money";


    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
             SetPlayerScore();
#endif
    }

    public void SetPlayerScore()
    {
        if (YandexGamesSdk.IsInitialized)
        {
            Leaderboard.GetPlayerEntry(_leaderboardName, OnSuccessCallback);
        }
    }

    private void OnSuccessCallback(LeaderboardEntryResponse result)
    {
        if (result != null || result.score < _money.TotalMoney)
        {
            Leaderboard.SetScore(_leaderboardName, _money.TotalMoney);
        }
    }
}