using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkinChangerUI : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _coinsText;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonClickSound;

    private const string BoughtSkinsKey = "BoughtSkins";
    private const string SelectedText = "Selected";
    private const string SelectText = "Select";
    private const float ButtonClickCooldown = 0.1f;

    private float _lastButtonClickTime;

    public void Initialize(Skin[] info, int index)
    {
        UpdateUI(info, index);
    }

    public void UpdateUI(Skin[] info, int index)
    {
        if (info[index].inStock && info[index].isChosen)
        {
            _priceText.text = SelectedText;
            _buyButton.interactable = false;
        }
        else if (!info[index].inStock)
        {
            _priceText.text = info[index].cost.ToString();
            _buyButton.interactable = true;
        }
        else if (info[index].inStock && !info[index].isChosen)
        {
            _priceText.text = SelectText;
            _buyButton.interactable = true;
        }
    }

    public void HandleActionButton(Skin[] info, int index, bool[] stockCheck)
    {
        PlayButtonClickSound();

        if (info[index].inStock && !info[index].isChosen)
        {
            info[index].isChosen = true;
            _priceText.text = SelectedText;
            PlayerPrefsX.SetBoolArray(BoughtSkinsKey, stockCheck);
        }
    }

    public void HandleSkinSelection(Skin[] info, int selectedIndex, Action onSave)
    {
        PlayButtonClickSound();

        for (int i = 0; i < info.Length; i++)
        {
            info[i].isChosen = false;
        }

        info[selectedIndex].isChosen = true;

        _buyButton.interactable = false;

        onSave?.Invoke();
    }

    private void PlayButtonClickSound()
    {
        if (Time.time - _lastButtonClickTime >= ButtonClickCooldown)
        {
            _audioSource.PlayOneShot(_buttonClickSound);
            _lastButtonClickTime = Time.time;
        }
    }
}
