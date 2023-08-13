using System;
using UnityEngine;

public abstract class UpgradeDataSO : ScriptableObject
{
    [SerializeField] protected int _cost;

    public int Cost => _cost;

    public abstract void ApplyUpgrade();

    public event Action Upgraded;
}
