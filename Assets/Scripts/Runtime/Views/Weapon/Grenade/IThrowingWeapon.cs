using Shooter.Model.Inventory;

namespace Shooter.Model
{
    public interface IThrowingWeapon : IShootingWeapon
    {
        IInventoryItemGameObjectView ItemView { get; }

    }
}