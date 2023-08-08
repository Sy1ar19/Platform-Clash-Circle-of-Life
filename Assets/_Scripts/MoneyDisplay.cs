using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private Player _player;

    private void Start()
    {
        UpdateMoneyDisplay(_player.Money);
    }

    private void OnMoneyChanged(int newMoney)
    {
        UpdateMoneyDisplay(newMoney);
    }

    private void UpdateMoneyDisplay(float moneyAmount)
    {
        _moneyText.text = moneyAmount.ToString();
    }

    private void OnEnable()
    {
        _player.MoneyIncreased += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _player.MoneyIncreased -= OnMoneyChanged;
    }
}
