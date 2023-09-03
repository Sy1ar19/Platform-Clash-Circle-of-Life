using UnityEngine;

public class LoadSaveDataSystem : MonoBehaviour
{
    private const string ChosenSkinKey = "ChosenSkin";
    private const string BoughtSkinsKey = "BoughtSkins";
    private const string MoneyKey = "PlayerMoney";
    private const string GoldMultiplierKey = "PlayerGoldMultiplier";
    private const string ExperienceKey = "PlayerExperience";
    private const string MaxExperienceKey = "PlayerMaxExperience";
    private const string LevelKey = "PlayerLevel";

    public void LoadSkinData(int index, Skin[] info, bool[] stockCheck)
    {
        index = PlayerPrefs.GetInt(ChosenSkinKey, 0);

        if (PlayerPrefs.HasKey(BoughtSkinsKey))
            stockCheck = PlayerPrefsX.GetBoolArray(BoughtSkinsKey);
        else
            InitializeDefaultStockCheck(stockCheck, info);

        for (int i = 0; i < Mathf.Min(info.Length, stockCheck.Length); i++)
        {
            info[i].inStock = stockCheck[i];
        }

        info[index].isChosen = true;
    }

    private void InitializeDefaultStockCheck(bool[] stockCheck, Skin[] info)
    {
        stockCheck = new bool[info.Length];
        stockCheck[0] = true;
    }

    public void SaveSkin(Skin[] info, int index)
    {
        bool[] modifiedStockCheck = new bool[info.Length];

        for (int i = 0; i < info.Length; i++)
        {
            modifiedStockCheck[i] = info[i].inStock;
        }

        PlayerPrefsX.SetBoolArray(BoughtSkinsKey, modifiedStockCheck);
        PlayerPrefs.SetInt(ChosenSkinKey, index);
    }

    public void SaveMoney(int money, int goldMultiplier)
    {
        SaveLoadSystem.SaveData<int>(MoneyKey, money);
        SaveLoadSystem.SaveData<int>(GoldMultiplierKey, goldMultiplier);
    }

    public void SaveExperience(int currentExperience, int maxExperience, int level)
    {
        SaveLoadSystem.SaveData<int>(ExperienceKey, currentExperience);
        SaveLoadSystem.SaveData<int>(MaxExperienceKey, maxExperience);
        SaveLoadSystem.SaveData<int>(LevelKey, level);
    }
}
