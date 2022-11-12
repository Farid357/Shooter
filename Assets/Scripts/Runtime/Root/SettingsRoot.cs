using System.Collections;
using Shooter.SaveSystem;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class SettingsRoot : MonoBehaviour
    {
        private readonly StorageWithNameSaveObject<SettingsRoot, bool> _cursorStateStorage = new(new BinaryStorage());
        private readonly StorageWithNameSaveObject<SettingsRoot, int> _fpsStorage = new(new BinaryStorage());
        private readonly StorageWithNameSaveObject<QualitySettings, int> _qualityLevelStorage = new(new BinaryStorage());
        private readonly StorageWithNameSaveObject<SettingsRoot, ShadowResolution> _shadowResolutionStorage = new(new BinaryStorage());

        private IEnumerator Start()
        {
            while (true)
            {
                var lockState = _cursorStateStorage.HasSave() && _cursorStateStorage.Load();
                Cursor.visible = lockState;
                Application.targetFrameRate = _fpsStorage.HasSave() ? _fpsStorage.Load() : 60;
                QualitySettings.SetQualityLevel(_qualityLevelStorage.HasSave() ? _qualityLevelStorage.Load() : 2);
                QualitySettings.shadowResolution = _shadowResolutionStorage.HasSave() ? _shadowResolutionStorage.Load() : ShadowResolution.Medium;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}