using Shooter.Model;
using Shooter.Tools;

namespace Shooter.GameLogic
{
    public interface IWeaponPickup
    {
        public void Init(IFactory<IBullet> bulletsFactory, IInventory<IWeapon> inventory);
    }
}