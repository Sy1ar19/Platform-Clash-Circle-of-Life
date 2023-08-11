public interface IPlayerUpgradesManager 
{
    bool CanLevelUp(IPlayerUpgrade upgrade);
    void LevelUp(IPlayerUpgrade upgrade);
    IPlayerUpgrade GetUpgrade(string id);
    IPlayerUpgrade[] GetAllUpgrades();
}
