using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private Player _player;
    [SerializeField] private AdvertisementButton _advertisementButton;

    private void OnMoneyChanged(int newMoney)
    {
        UpdateMoneyDisplay(newMoney);
    }

    private void UpdateMoneyDisplay(int moneyAmount)
    {
        int temp = int.Parse(_moneyText.text) + moneyAmount;
        _moneyText.text = temp.ToString();
    }

    private void OnEnable()
    {
       // _player.MoneyIncreased += OnMoneyChanged;
        _advertisementButton.WatchedAd += OnMoneyChanged;
        _moneyText.text = _player.LevelMoney.ToString();
    }

    private void OnDisable()
    {
        //_player.MoneyIncreased -= OnMoneyChanged;
        _advertisementButton.WatchedAd -= OnMoneyChanged;
    }
}
