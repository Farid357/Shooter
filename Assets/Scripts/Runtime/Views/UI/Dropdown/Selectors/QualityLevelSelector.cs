using Shooter.SaveSystem;
using UnityEngine;

namespace Shooter.Model.Settings
{
    public sealed class QualityLevelSelector
    {
        private readonly StorageWithNameSaveObject<QualitySettings, int> _storage = new(new BinaryStorage());

        public void Select(int level)
        {
            QualitySettings.SetQualityLevel(level);
            _storage.Save(level);
        }
    }
}