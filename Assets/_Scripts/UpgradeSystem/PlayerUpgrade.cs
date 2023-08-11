/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerUpgrade : IPlayerUpgrade
{
    public string Id
    {
        get { return this.config.id; }
    }

    public int Level { get; private set; } = 1;

    public int MaxLevel
    {
        get { return this.config.maxLevel; }
    }

    public int NextPrice
    {
        get { return this.config.priceTable.GetPrice(this.level +1)}
    }

    public string Title
    {
        get { return this.config.metadata.title; }
    }

    public abstract string CurrentStats { get; }

    public abstract string NextImprovement { get; }

    public Sprite Icon
    {
        get { return this.config.metadata.Icon; }
    }

    public void Setup(int level)
    {
        this.Level = level;
    }

    private readonly HeroUpgradeConfig config;

    protected HeroUpgrade(HeroUpgradeConfig config)
    {
        this.config = config;
    }

    public void IncrementLevel()
    {
        if(this.Level >= this.MaxLevel)
        {
            throw new Exception($"Cannot increment level for upgrade  {this.config.id}");
        }

        var nextLevel = this.Level + 1;
        this UpdateLevel(nextLevel);
        this.Level = nextLevel;
        this.OnLevelUp?.Invoke(nextLevel);
    }

    void IgameInitLevel.InitGame(IGameSystem system)
    {
        this.Initialize(system, this.Level);
    }

    protected abstract void Initialize(IGameSystem, int level);
    protected abstract void UpdateLevel(int level);
}

*/