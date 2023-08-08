using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvertisementButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Player _player;
    [SerializeField] private int _coinsMultiplier = 2; // Удвоение монет

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
            //int additionalCoins = _player.LevelMoney * _coinsMultiplier/2;
            int additionalCoins = _player.LevelMoney;
            WatchedAd?.Invoke(additionalCoins);
            _player.AddMoney(additionalCoins);
            _button.interactable = false;
            _isCoinMultiplierUsed = true;
        }
        

        // Обновление UI с монетами
        //_player.MoneyChanged?.Invoke(_player.Money);
    }
}
