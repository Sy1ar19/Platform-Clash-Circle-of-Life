/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerUpgradeCatalog", menuName = "PlayerUpgrades/New PlayerUpgradeCatalog")]
public class PlayerUpgradeCatalog : ScriptableObject
{
    [SerializeField] private PlayerUpgradeCongfig[] _configs;

    public PlayerUpgradeConfig[] GetAllUpgrades()
    {
        return _configs;
    }

    public HeroUpgradeConfig FindUpgrade(string id)
    {
        var lentgth = _configs.Length;

        for (int i = 0; i < lentgth; i++)
        {
            var config = _configs[i];

            if (config.id == id)
            {
                return config;
            }
        }
    }
}
*/