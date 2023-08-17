using System;
using UnityEngine;

public class PlayerPrefsStorageService : IStorageService
{
    public void Save(string key, object data, Action callback = null)
    {
        if (data is int intValue)
        {
            PlayerPrefs.SetInt(key, intValue);
        }
        else if (data is float floatValue)
        {
            PlayerPrefs.SetFloat(key, floatValue);
        }
        // Добавьте другие типы данных по мере необходимости

        PlayerPrefs.Save();

        callback?.Invoke();
    }

    public void Load<T>(string key, Action<T> callback)
    {
        if (typeof(T) == typeof(int))
        {
            int loadedValue = PlayerPrefs.GetInt(key, default);
            callback?.Invoke((T)(object)loadedValue);
        }
        else if (typeof(T) == typeof(float))
        {
            float loadedValue = PlayerPrefs.GetFloat(key, default);
            callback?.Invoke((T)(object)loadedValue);
        }
        // Добавьте другие типы данных по мере необходимости
    }
}
