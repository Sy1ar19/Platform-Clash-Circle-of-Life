using System;
using UnityEngine;
using UnityEngine.UI;

public class AdvertisementButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private int _coinsMultiplier = 2;

    public event Action<int> WatchedAd;

    private bool _isCoinMultiplierUsed = false;

    private void Start()
    {
        _button.onClick.AddListener(WatchAd);
    }

    private void WatchAd()
    {
        if (_isCoinMultiplierUsed == false)
        {
            if (_playerMoney.LevelMoney > 0)
            {
                int additionalCoins = _playerMoney.LevelMoney;

                _playerMoney.EarnMoney(_playerMoney.LevelMoney);
                _button.interactable = false;
                _isCoinMultiplierUsed = true;
            }
        }
    }
}
