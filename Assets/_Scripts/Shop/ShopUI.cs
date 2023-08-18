using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonClickSound;

    [SerializeField] private TextMeshProUGUI _damagePriceText;
    [SerializeField] private TextMeshProUGUI _healthPriceText;
    [SerializeField] private TextMeshProUGUI _armorPriceText;
    [SerializeField] private TextMeshProUGUI _criticalChancePriceText;
    [SerializeField] private TextMeshProUGUI _criticalDamagePriceText;
    [SerializeField] private TextMeshProUGUI _goldMultiplierPriceText;

    [SerializeField] private Image _healthPriceButtonImage;
    [SerializeField] private Image _damagePriceButtonImage;
    [SerializeField] private Image _armorPriceButtonImage;
    [SerializeField] private Image _criticalChancePriceButtonImage;
    [SerializeField] private Image _criticalDamagePriceButtonImage;
    [SerializeField] private Image _goldMultiplierPriceButtonImage;
    [SerializeField] private Sprite _spriteWithEnoughMoney;
    [SerializeField] private Sprite _spriteWithoutEnoughMoney;

    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private PlayerAttacker _playerAttacker;

    private const float ButtonClickCooldown = 0.1f;
    private const string MaxPrice = "MAX";

    private float _lastButtonClickTime;

    private void OnEnable()
    {
        _playerAttacker.MaxCriticalChanceReached += HandleMaxCriticalChanceReached;
    }

    private void OnDisable()
    {
        _playerAttacker.MaxCriticalChanceReached -= HandleMaxCriticalChanceReached;
    }

    public void UpdateUI(int damagePrice, int healthPrice, int armorPrice, int criticalChancePrice, int criticalDamagePrice, int goldMultiplierPrice)
    {
        _damagePriceText.text = damagePrice.ToString();
        _healthPriceText.text = healthPrice.ToString();
        _armorPriceText.text = armorPrice.ToString();
        _criticalChancePriceText.text = criticalChancePrice.ToString();
        _criticalDamagePriceText.text = criticalDamagePrice.ToString();
        _goldMultiplierPriceText.text = goldMultiplierPrice.ToString();

        UpdateButtonSprite(_damagePriceButtonImage, damagePrice);
        UpdateButtonSprite(_healthPriceButtonImage, healthPrice);
        UpdateButtonSprite(_armorPriceButtonImage, armorPrice);
        UpdateButtonSprite(_criticalChancePriceButtonImage, criticalChancePrice);
        UpdateButtonSprite(_criticalDamagePriceButtonImage, criticalDamagePrice);
        UpdateButtonSprite(_goldMultiplierPriceButtonImage, goldMultiplierPrice);

        if (_playerAttacker.CriticalChance >= _playerAttacker.GetMaxCriticalChance())
        {
            _criticalChancePriceText.text = MaxPrice;
        }
        else
        {
            _criticalChancePriceText.text = criticalChancePrice.ToString();
        }
    }

    private void UpdateButtonSprite(Image buttonImage, int upgradePrice)
    {
        bool canAfford = _playerMoney.Money >= upgradePrice;
        buttonImage.sprite = canAfford ? _spriteWithEnoughMoney : _spriteWithoutEnoughMoney;

        if (canAfford)
        {
            buttonImage.GetComponent<Button>().onClick.AddListener(PlayButtonClickSound);
        }
        else
        {
            buttonImage.GetComponent<Button>().onClick.RemoveListener(PlayButtonClickSound);
        }
    }

    private void PlayButtonClickSound()
    {
        if (Time.time - _lastButtonClickTime >= ButtonClickCooldown)
        {
            _audioSource.PlayOneShot(_buttonClickSound);
            _lastButtonClickTime = Time.time;
        }
    }

    private void HandleMaxCriticalChanceReached()
    {
        _criticalChancePriceButtonImage.GetComponent<Button>().interactable = false;
    }
}
