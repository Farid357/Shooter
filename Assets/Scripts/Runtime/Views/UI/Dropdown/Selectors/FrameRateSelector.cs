using Shooter.Root;
using Shooter.SaveSystem;
using UnityEngine;

namespace Shooter.Model.Settings
{
    public sealed class FrameRateSelector
    {
        private readonly StorageWithNameSaveObject<SettingsRoot, int> _storage = new(new BinaryStorage());

        public void Select(int frameRate)
        {
            Application.targetFrameRate = frameRate;
            _storage.Save(frameRate);
        }
    }
}