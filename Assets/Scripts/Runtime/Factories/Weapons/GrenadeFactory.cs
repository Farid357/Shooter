using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class GrenadeFactory : MonoBehaviour, IFactory<IGrenade>
    {
        [SerializeField] private GrenadeView _prefab;
        [SerializeField] private CharacterMovement _parent;
        [SerializeField] private Transform _spawnPoint;
        
        public IGrenade Create()
        {
            var grenade = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity, _parent.transform);
            grenade.gameObject.SetActive(false);
            return grenade;
        }
    }
}