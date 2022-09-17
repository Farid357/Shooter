namespace Shooter.Model.Inventory
{
    public sealed class DummyWeapon : IShootingWeapon
    {
        public void Shoot()
        {
            
        }

        public bool CanShoot => false;
    }
}