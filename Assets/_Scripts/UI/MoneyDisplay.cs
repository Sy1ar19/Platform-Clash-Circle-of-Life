using UnityEngine;
using TMPro;
using Assets._Scripts.Player;

namespace Assets._Scripts.UI
{
    public class MoneyDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private PlayerMoney _playerMoney;

        private void Start()
        {
            UpdateMoneyDisplay(_playerMoney.Money);
        }

        private void OnMoneyChanged(int newMoney)
        {
            int currentMoney = int.Parse(_moneyText.text);
            UpdateMoneyDisplay(currentMoney + newMoney);
        }

        private void UpdateMoneyDisplay(int moneyAmount)
        {
            _moneyText.text = moneyAmount.ToString();
        }

        private void OnEnable()
        {
            _playerMoney.MoneyChanged += OnMoneyChanged;
            UpdateMoneyDisplay(_playerMoney.Money);
        }

        private void OnDisable()
        {
            _playerMoney.MoneyChanged -= OnMoneyChanged;
        }
    }
}