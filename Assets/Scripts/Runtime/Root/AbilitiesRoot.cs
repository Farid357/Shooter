using System;
using System.Collections.Generic;
using Shooter.GameLogic;
using Shooter.Model;
using Shooter.SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.Root
{
    public sealed class AbilitiesRoot : SerializedMonoBehaviour, IAbilityRoot
    {
        [Title("Views")] 
        [SerializeField] private IAbilityView _speedBoostAbility;
        [SerializeField] private IAbilityView _bulletsDamageAbility;
        [SerializeField] private IAbilityView _regenerationAbility;
        
        [Title("Other")]
        [SerializeField] private List<IBulletsFactory> _bulletsFactories;
        [SerializeField] private IHealthTransformView _characterHealthTransformView;
        [SerializeField] private ICharacterMovement _characterMovement;
        
        private CharacterIncreaseBulletsDamageAbility _characterIncreaseBulletsDamageAbility;
        private IEnumerable<IAbility> _abilities;
        
        private IEnumerable<IAbility> Compose()
        {
            var storageCharacterIncreaseBulletsSeconds = new StorageWithNameSaveObject<CharacterIncreaseBulletsDamageAbility, float>(new BinaryStorage());
            var characterIncreaseBulletsDamageSeconds  = storageCharacterIncreaseBulletsSeconds.HasSave() ? storageCharacterIncreaseBulletsSeconds.Load() : 3f;
            var storageCharacterSpeedBoostSeconds = new StorageWithNameSaveObject<CharacterSpeedBoostAbility, float>(new BinaryStorage());
            var characterSpeedBoostSeconds  = storageCharacterSpeedBoostSeconds.HasSave() ? storageCharacterIncreaseBulletsSeconds.Load() : 4f;

            yield return new CharacterSpeedBoostAbility(_speedBoostAbility, _characterMovement, characterSpeedBoostSeconds);
            yield return _characterIncreaseBulletsDamageAbility = new CharacterIncreaseBulletsDamageAbility(_bulletsDamageAbility, _bulletsFactories.ToArray(), characterIncreaseBulletsDamageSeconds );;
            yield return new CharacterHealthRegenerationAbility(_regenerationAbility, _characterHealthTransformView.Health);
        }

        public IEnumerable<IAbility> Abilities()
        {
            _abilities ??= Compose();
            return _abilities;
        }

        private void OnDestroy() => _characterIncreaseBulletsDamageAbility.Dispose();
    }
}