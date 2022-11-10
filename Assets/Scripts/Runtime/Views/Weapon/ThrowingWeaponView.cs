using Shooter.Model;
using Shooter.Model.Inventory;
using Sirenix.OdinInspector;

namespace Shooter.GameLogic
{
    public abstract class ThrowingWeaponView : SerializedMonoBehaviour, IThrowingWeapon
    {
        public abstract void Shoot();
        
        public abstract bool CanShoot { get; }
        
        public abstract IInventoryItemGameObjectView ItemView { get; }
    }
}