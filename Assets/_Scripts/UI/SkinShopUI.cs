using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets._Scripts.Shop;
using Assets._Scripts.Storage;

namespace Assets._Scripts.UI
{
    public class SkinShopUI : MonoBehaviour
    {
        private const string BoughtSkinsKey = "BoughtSkins";
        private const float ButtonClickCooldown = 0.1f;

        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _selectedButton;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _buttonClickSound;

        private float _lastButtonClickTime;

        public void Initialize(Skin[] info, int index)
        {
            UpdateUI(info, index);
        }

        public void UpdateUI(Skin[] info, int index)
        {
            if (info[index].inStock && info[index].isChosen)
            {
                _buyButton.gameObject.SetActive(false);
                _selectButton.gameObject.SetActive(false);
                _selectedButton.gameObject.SetActive(true);
            }
            else if (!info[index].inStock)
            {
                _priceText.text = info[index].cost.ToString();
                _buyButton.gameObject.SetActive(true);
                _selectButton.gameObject.SetActive(false);
                _selectedButton.gameObject.SetActive(false);
            }
            else if (info[index].inStock && !info[index].isChosen)
            {
                _buyButton.gameObject.SetActive(false);
                _selectButton.gameObject.SetActive(true);
                _selectedButton.gameObject.SetActive(false);
            }
        }

        public void HandleActionButton(Skin[] info, int index, bool[] stockCheck)
        {
            if (info[index].inStock && !info[index].isChosen)
            {
                info[index].isChosen = true;
                PlayerPrefsX.SetBoolArray(BoughtSkinsKey, stockCheck);

                _buyButton.gameObject.SetActive(false);
                _selectButton.gameObject.SetActive(true);
            }
        }

        public void HandleSkinSelection(Skin[] info, int selectedIndex)
        {
            PlayButtonClickSound();

            for (int i = 0; i < info.Length; i++)
            {
                info[i].isChosen = false;
            }

            info[selectedIndex].isChosen = true;

            UpdateUI(info, selectedIndex);
        }

        public void PlayButtonClickSound()
        {
            if (Time.time - _lastButtonClickTime >= ButtonClickCooldown)
            {
                _audioSource.PlayOneShot(_buttonClickSound);
                _lastButtonClickTime = Time.time;
            }
        }
    }
}