using Shooter.Model;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic
{
    public abstract class WeaponWithRollbackAndShootWaitingPickup : MonoBehaviour
    {
        [SerializeField] private WeaponPickup _weaponPickup;
        
        public void Init(IFactory<IBullet> bulletsFactory, IInventory<IWeapon> inventory)
        {
            var weaponFactory = new WeaponFactoryWithShootWaitingAndRollback(bulletsFactory, _weaponPickup.Data);
            _weaponPickup.Init(inventory, weaponFactory.Create());
        }
    }
}