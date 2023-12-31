using Assets._Scripts.Player;
using TMPro;
using UnityEngine;

namespace Assets._Scripts.UI
{
    public class RewardDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private PlayerMoney _playerMoney;
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
            _playerMoney.MoneyChanged += OnMoneyChanged;

            _moneyText.text = _playerMoney.LevelMoney.ToString();
        }

        private void OnDisable()
        {
            _playerMoney.MoneyChanged -= OnMoneyChanged;
        }
    }
}