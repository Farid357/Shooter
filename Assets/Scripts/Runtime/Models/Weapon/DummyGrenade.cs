namespace Shooter.Model.Inventory
{
    public sealed class DummyGrenade : IGrenade
    {
        public void Shoot()
        {
            
        }

        public bool CanShoot => false;

        public IInventoryItemGameObjectView ItemView { get; }
    }
}