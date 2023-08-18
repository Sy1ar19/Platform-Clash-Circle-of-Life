using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private Skin[] _skins;
    [SerializeField] private Transform _playerVisual;

    private const string ChosenSkinKey = "ChosenSkin";

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
