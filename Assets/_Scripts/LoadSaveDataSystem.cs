using Assets._Scripts.Shop;
using Assets._Scripts.Storage;
using UnityEngine;

namespace Assets._Scripts
{
    public class LoadSaveDataSystem : MonoBehaviour
    {
        private const string ChosenSkinKey = "ChosenSkin";
        private const string BoughtSkinsKey = "BoughtSkins";
        private const string MoneyKey = "PlayerMoney";
        private const string GoldMultiplierKey = "PlayerGoldMultiplier";
        private const string ExperienceKey = "PlayerExperience";
        private const string MaxExperienceKey = "PlayerMaxExperience";
        private const string LevelKey = "PlayerLevel";
        private const string SelectedSkinKey = "SelectedSkin";

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
            SaveLoadSystem.SaveData(MoneyKey, money);
            SaveLoadSystem.SaveData(GoldMultiplierKey, goldMultiplier);
        }

        public void SaveExperience(int currentExperience, int maxExperience, int level)
        {
            SaveLoadSystem.SaveData(ExperienceKey, currentExperience);
            SaveLoadSystem.SaveData(MaxExperienceKey, maxExperience);
            SaveLoadSystem.SaveData(LevelKey, level);
        }

        public void SaveSelectedSkin(int index)
        {
            PlayerPrefs.SetInt(SelectedSkinKey, index);
        }
    }
}