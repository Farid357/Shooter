namespace Shooter.Model.Inventory
{
    public sealed class DummyPotion : IPotion
    {
        public bool CanShoot => false;
        
        public void Shoot()
        {
            
        }

        public void Disable()
        {
            
        }
    }
}