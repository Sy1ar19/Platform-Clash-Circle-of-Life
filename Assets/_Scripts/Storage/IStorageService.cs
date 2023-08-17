using System;

public interface IStorageService 
{
    void Save(string key, object data, Action callback = null);
    void Load<T>(string key, Action<T> callback);
}
