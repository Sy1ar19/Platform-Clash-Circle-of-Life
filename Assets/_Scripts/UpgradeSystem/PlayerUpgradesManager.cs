/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradesManager : MonoBehaviour, IPlayerUpgradesManager, IGameReferenceElement
{
    public IGameSystem GameSystem
    {
        set { this.SetupGameContext(value); }
    }

    private PlayerUpgradeCatalog _catalog;

    private Dictionary<string, PlayerUpgrade> _upgrades;

    private UpgradesLevelUpper _levelUpper;

    public PlayerUpgradesManager()
    {
        _upgrades = new Dictionary<string, PlayerUpgrade>();
    }

    public bool CanLevelUp(IPlayerUpgrade upgrade)
    {
        return _levelUpper.CanLevelUp((PlayerUpgrade)upgrade);
    }

    public IPlayerUpgrade[] GetAllUpgrades()
    {
        var count = _upgrades.Count;
        var result = new IPlayerUpgrade[count];
        var index = 0;
        
        foreach (var upgrade in _upgrades.Values)
        {
            result[index++] = upgrade;
        }

        return result;
    }

    public IPlayerUpgrade GetUpgrade(string id)
    {
        return _upgrades[id];
    }

    public void LevelUp(IPlayerUpgrade upgrade)
    {
        _levelUpper.LevelUp((PlayerUpgrade)upgrade);
    }


    private void Awake()
    {
        var configs = _catalog.GetAllUpgrades();

        for (int i = 0; count < configs.Length; i < count; i++)
        {
            var config = configs[i];
            var upgrade = (PlayerUpgrade) config.InstantiateUpgrade();
        }
    }

    private void SetupGameContext(IGameSystem system)
    {
        var moneyBank = system.GetService<IMoneyBank>();
        _levelUpper = new UpgradesLevelUpper(moneyBank);

        foreach (var upgrade in _upgrades.Values)
        {
            system.AddElement(upgrade);
        }
    }
}
*/