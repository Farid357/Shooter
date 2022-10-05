using Shooter.Model;
using Shooter.Model.Inventory;

namespace Shooter.GameLogic.Inventory
{
    public interface IBulletsPickup
    {
        public void Init(IReadOnlyInventory<(IWeapon, IWeaponInput)> inventory);
    }
}