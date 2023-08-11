using System;
using UnityEngine;

public interface IPlayerUpgrade 
{
    event Action OnLevelUp;

    string Id { get; }
    int Level { get; }
    int MaxLevel { get; }
    int NextPrice{ get; }
    string Title { get; }

    string CurrentStats { get; }
    string NextImprovement { get; }
    Sprite Icon { get; }

    void Setup(int level);
}
