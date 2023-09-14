using System;
using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;

public class AdvertisementButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private int _coinsMultiplier = 2;

    public event Action<int> WatchedAd;

    private bool _isAdvertismentWatched = false;

    private void Start()
    {
        _button.onClick.AddListener(WatchAd);
    }

    public void WatchAd()
    {
        if (_isAdvertismentWatched == false)
        {
            if (_playerMoney.LevelMoney > 0)
            {
                int additionalCoins = _playerMoney.LevelMoney;

                if(_playerMoney.GoldMultiplier > 1)
                {
                    _playerMoney.EarnMoney(_playerMoney.LevelMoney/2);
                }
                else
                {
                    _playerMoney.EarnMoney(_playerMoney.LevelMoney);
                }

                _button.interactable = false;
                _isAdvertismentWatched = true;
            }
        }
    }
}
