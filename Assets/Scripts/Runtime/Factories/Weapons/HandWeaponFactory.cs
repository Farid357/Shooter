using Shooter.Model;
using Shooter.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class HandWeaponFactory : SerializedMonoBehaviour, IFactory<IShootingWeapon>
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private AudioSource _sliceAudio;
        [SerializeField] private HandWeapon _prefab;
        [SerializeField] private CharacterMovement _parent;

        public IShootingWeapon Create()
        {
            var handWeapon = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity, _parent.transform);
            handWeapon.Init(_sliceAudio);
            return handWeapon;
        }
    }
}