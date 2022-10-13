using System;
using UnityEngine;

namespace Shooter.SaveSystem
{
    public sealed class PlayerPrefsStorage : IStorage
    {
        public bool Exists(string key) => PlayerPrefs.HasKey(key);

        public void DeleteSave(string key)
        {
            if (Exists(key) == false)
                throw new InvalidOperationException(nameof(DeleteSave));

            PlayerPrefs.DeleteKey(key);
        }

        public T Load<T>(string key)
        {
            if (Exists(key) == false)
                throw new InvalidOperationException(nameof(Load));

            var loadJson = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(loadJson);
        }

        public void Save<T>(string key, T saveObject)
        {
            var saveJson = JsonUtility.ToJson(saveObject);
            PlayerPrefs.SetString(key, saveJson);
            PlayerPrefs.Save();
        }
    }
}