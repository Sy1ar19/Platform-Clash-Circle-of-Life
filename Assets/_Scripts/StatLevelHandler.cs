using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatLevelHandler : MonoBehaviour
{
    public int Healthlevel { get; private set; }
    public int DamageLevel { get; private set; }

    [SerializeField] List<Upgrade> _upgrades;

    private void Awake()
    {
        foreach (Upgrade upgrade in _upgrades)
        {
            if (upgrade.Title == "Health")
                Healthlevel = SaveLoadSystem.LoadData(upgrade.Title, 0);
            if (upgrade.Title == "Damage")
                DamageLevel = SaveLoadSystem.LoadData(upgrade.Title, 0);
        }
    }
}
