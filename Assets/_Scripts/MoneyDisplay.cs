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

    private void OnMoneyChanged(float newMoney)
    {
        UpdateMoneyDisplay(newMoney);
    }

    private void UpdateMoneyDisplay(float moneyAmount)
    {
        _moneyText.text = moneyAmount.ToString();
    }

    private void OnEnable()
    {
        _player.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= OnMoneyChanged;
    }
}
