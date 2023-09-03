using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkShopUI : MonoBehaviour
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
    [SerializeField] private Button _armorAvailableButton;
    [SerializeField] private Button _armorUnavailableButton;
    [SerializeField] private Button _criticalChanceAvailableButton;
    [SerializeField] private Button _criticalChanceUnavailableButton;
    [SerializeField] private Button _criticalDamageAvailableButton;
    [SerializeField] private Button _criticalDamageUnavailableButton;
    [SerializeField] private Button _goldMultiplierAvailableButton;
    [SerializeField] private Button _goldMultiplierUnavailableButton;

    [SerializeField] private PlayerMoney _playerMoney;
    [SerializeField] private PlayerAttacker _playerAttacker;
    [SerializeField] private PlayerExperience _playerExperience;
    [SerializeField] private Weapon _weapon;

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

        if (_playerExperience.Level >= 5)
        {
            _armorAvailableButton.gameObject.SetActive(true);
            _armorUnavailableButton.gameObject.SetActive(false);
        }
        else
        {
            _armorAvailableButton.gameObject.SetActive(false);
            _armorUnavailableButton.gameObject.SetActive(true);
        }

        if (_playerExperience.Level >= 10)
        {
            _criticalChanceAvailableButton.gameObject.SetActive(true);
            _criticalChanceUnavailableButton.gameObject.SetActive(false);
            _criticalDamageAvailableButton.gameObject.SetActive(true);
            _criticalDamageUnavailableButton.gameObject.SetActive(false);
        }
        else
        {
            _criticalChanceAvailableButton.gameObject.SetActive(false);
            _criticalChanceUnavailableButton.gameObject.SetActive(true);
            _criticalDamageAvailableButton.gameObject.SetActive(false);
            _criticalDamageUnavailableButton.gameObject.SetActive(true);
        }

        if (_playerExperience.Level >= 15)
        {
            _goldMultiplierAvailableButton.gameObject.SetActive(true);
            _goldMultiplierUnavailableButton.gameObject.SetActive(false);
        }
        else
        {
            _goldMultiplierAvailableButton.gameObject.SetActive(false);
            _goldMultiplierUnavailableButton.gameObject.SetActive(true);
        }

        if (_weapon.CriticalChance >= _weapon.GetMaxCriticalChance())
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
            //_audioSource.PlayOneShot(_buttonClickSound);
            //_audioSource.PlayOneShot();
            _audioSource.Play();
            _lastButtonClickTime = Time.time;
        }
    }

    private void HandleMaxCriticalChanceReached()
    {
        _criticalChancePriceButtonImage.GetComponent<Button>().interactable = false;
    }
}
