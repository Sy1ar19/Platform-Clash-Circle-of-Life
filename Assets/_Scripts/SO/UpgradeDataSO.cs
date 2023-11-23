using UnityEngine;

namespace Assets._Scripts.SO
{
    public abstract class UpgradeDataSO : ScriptableObject
    {
        [SerializeField] protected int _cost;
    }
}