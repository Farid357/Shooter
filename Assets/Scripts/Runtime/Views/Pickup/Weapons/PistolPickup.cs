using Shooter.Model;
using Shooter.Tools;
using UnityEngine;

namespace Shooter.GameLogic
{
    public sealed class PistolPickup : MonoBehaviour, IWeaponPickup
    {
        [SerializeField] private WeaponPickup _weaponPickup;

        public void Init(IFactory<IBullet> bulletsFactory, IInventory<IWeapon> inventory)
        {
            var weaponFactory = new WeaponFactoryWithShootWaiting(bulletsFactory, _weaponPickup.Data);
            _weaponPickup.Init(inventory, new Pistol(weaponFactory.Create()));
        }
    }
}