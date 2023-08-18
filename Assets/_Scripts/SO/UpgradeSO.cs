using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrades/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    [SerializeField] private string _title;
    [SerializeField] private int _price;
    [SerializeField] private int _increaseValue;

    public int Price => _price;
    public int IncreaseValue => _increaseValue;
    public string Title => _title;
}
