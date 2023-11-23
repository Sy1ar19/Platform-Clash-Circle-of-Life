using Assets._Scripts.Shop;
using UnityEngine;

namespace Assets._Scripts.Player
{
    public class PlayerSkin : MonoBehaviour
    {
        private const string ChosenSkinKey = "ChosenSkin";

        [SerializeField] private Skin[] _skins;
        [SerializeField] private Transform _playerVisual;

        private int _chosenSkinIndex;

        private void OnEnable()
        {
            _chosenSkinIndex = PlayerPrefs.GetInt(ChosenSkinKey, 0);

            for (int i = 0; i < _playerVisual.childCount; i++)
            {
                _playerVisual.GetChild(i).gameObject.SetActive(i == _chosenSkinIndex);
            }
        }
    }
}