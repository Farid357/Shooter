using Shooter.Model;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shooter.GameLogic
{
    public sealed class HandWeaponFactory : SerializedMonoBehaviour, IFactory<IShootingWeapon>
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private AudioSource _sliceAudio;
        [SerializeField] private HandWeapon _prefab;
        [FormerlySerializedAs("_parent")] [SerializeField] private CharacterMovement _character;

        public IShootingWeapon Create()
        {
            var handWeapon = Instantiate(_prefab, _spawnPoint.position, _prefab.transform.rotation, transform);
            handWeapon.Init(_sliceAudio, _character);
            return handWeapon;
        }
    }
}