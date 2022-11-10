namespace Shooter.Model.Inventory
{
    public sealed class DummyThrowingWeapon : IThrowingWeapon
    {
        public void Shoot()
        {
            
        }

        public bool CanShoot => false;

        public IInventoryItemGameObjectView ItemView { get; }
    }
}