using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private Skin[] _info;
    [SerializeField] private bool[] _stockCheck;

    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Transform _skins;
    [SerializeField] private int _index;

    [SerializeField] private SkinChangerUI _skinSwitcherUI;

    [SerializeField] private PlayerMoney _playerMoney;

    private const string ChosenSkinKey = "ChosenSkin";
    private const string BoughtSkinsKey = "BoughtSkins";

    private void Awake()
    {
        LoadSkinData();

        for (int i = 0; i < _info.Length; i++)
        {
            _skins.GetChild(i).gameObject.SetActive(false);
        }

        _info[_index].isChosen = true;
        _skins.GetChild(_index).gameObject.SetActive(true);

        _skinSwitcherUI.UpdateUI(_info, _index);
    }

    public void Save()
    {
        bool[] modifiedStockCheck = new bool[_info.Length];

        for (int i = 0; i < _info.Length; i++)
        {
            modifiedStockCheck[i] = _info[i].inStock;
        }

        PlayerPrefsX.SetBoolArray(BoughtSkinsKey, modifiedStockCheck);
        PlayerPrefs.SetInt(ChosenSkinKey, _index);
    }

    public void ScrollRight()
    {
        _skins.GetChild(_index).gameObject.SetActive(false);
        _index = (_index + 1) % _skins.childCount;
        _skins.GetChild(_index).gameObject.SetActive(true);
        _skinSwitcherUI.UpdateUI(_info, _index);
    }

    public void ScrollLeft()
    {
        _skins.GetChild(_index).gameObject.SetActive(false);
        _index = (_index - 1 + _skins.childCount) % _skins.childCount;
        _skins.GetChild(_index).gameObject.SetActive(true);
        _skinSwitcherUI.UpdateUI(_info, _index);
    }

    public void ActionButtonAction()
    {
        if (!_info[_index].inStock)
        {
            int skinCost = _info[_index].cost;

            if (_playerMoney.Money >= skinCost)
            {
                BuySkin(_playerMoney, skinCost);
                UpdateSkinStatus();
            }
        }
        else if (_info[_index].inStock && !_info[_index].isChosen)
        {
            SetSkinChosen(); 
            Save();
        }
    }

    private void UpdateSkinStatus()
    {
        _info[_index].inStock = true;
        _stockCheck[_index] = true;
        PlayerPrefsX.SetBoolArray(BoughtSkinsKey, _stockCheck);
        _skinSwitcherUI.UpdateUI(_info, _index);
    }

    private void SetSkinChosen()
    {
        for (int i = 0; i < _info.Length; i++)
        {
            _info[i].isChosen = false;
        }

        _info[_index].isChosen = true;
        _skinSwitcherUI.HandleSkinSelection(_info, _index, Save);
        _skinSwitcherUI.UpdateUI(_info, _index);
    }

    private void BuySkin(PlayerMoney playerMoney, int skinCost)
    {
        if (playerMoney.Money >= skinCost)
        {
            playerMoney.SpendMoney(skinCost);
            _info[_index].inStock = true;
            _stockCheck[_index] = true;
            PlayerPrefsX.SetBoolArray(BoughtSkinsKey, _stockCheck);
        }
    }

    private void LoadSkinData()
    {
        _index = PlayerPrefs.GetInt(ChosenSkinKey, 0);

        if (PlayerPrefs.HasKey(BoughtSkinsKey))
            _stockCheck = PlayerPrefsX.GetBoolArray(BoughtSkinsKey);
        else
            InitializeDefaultStockCheck();

        for (int i = 0; i < Mathf.Min(_info.Length, _stockCheck.Length); i++)
        {
            _info[i].inStock = _stockCheck[i];
        }

        _info[_index].isChosen = true;
    }

    private void InitializeDefaultStockCheck()
    {
        _stockCheck = new bool[_info.Length];
        _stockCheck[0] = true; 
    } 
}


[System.Serializable]
public class Skin
{
    public int cost;
    public bool inStock;
    public bool isChosen;
}