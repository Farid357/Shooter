using Shooter.Root;
using Shooter.SaveSystem;
using UnityEngine;

namespace Shooter.Model.Settings
{
    public sealed class ShadowResolutionSelector
    {
        private readonly StorageWithNameSaveObject<SettingsRoot, ShadowResolution> _storage = new(new BinaryStorage());

        public void Select(ShadowResolution shadowQuality)
        {
            QualitySettings.shadowResolution = shadowQuality;
            _storage.Save(shadowQuality);
        }
    }
}