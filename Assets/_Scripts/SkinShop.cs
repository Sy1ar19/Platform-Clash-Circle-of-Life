using UnityEngine;

public class SkinShop : MonoBehaviour
{
    [SerializeField] private Skin[] _info;
    [SerializeField] private bool[] _stockCheck;

    [SerializeField] private Transform _skins;
    [SerializeField] private int _index;

    [SerializeField] private SkinShopUI _skinSwitcherUI;
    private LoadSaveDataSystem loadSaveDataSystem = new LoadSaveDataSystem();

    [SerializeField] private PlayerMoney _playerMoney;

    private const string BoughtSkinsKey = "BoughtSkins";


    private void Awake()
    {
        loadSaveDataSystem.LoadSkinData(_index, _info, _stockCheck);

        for (int i = 0; i < _info.Length; i++)
        {
            _skins.GetChild(i).gameObject.SetActive(false);
        }

        _info[_index].isChosen = true;
        _info[_index].inStock = true;
        _skins.GetChild(_index).gameObject.SetActive(true);

        _skinSwitcherUI.UpdateUI(_info, _index);
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

    public void ButtonAction()
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
            loadSaveDataSystem.SaveSkin(_info, _index);
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
        _skinSwitcherUI.HandleSkinSelection(_info, _index);
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
}


[System.Serializable]
public class Skin
{
    public int cost;
    public bool inStock;
    public bool isChosen;
}