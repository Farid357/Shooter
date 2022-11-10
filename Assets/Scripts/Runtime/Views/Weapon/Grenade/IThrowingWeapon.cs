using Shooter.Model.Inventory;

namespace Shooter.Model
{
    public interface IThrowingWeapon : IDroppingWeapon
    {
        IInventoryItemGameObjectView ItemView { get; }

    }
}