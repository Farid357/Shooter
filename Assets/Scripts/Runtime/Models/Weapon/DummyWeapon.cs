namespace Shooter.Model.Inventory
{
    public sealed class DummyWeapon : IWeapon
    {
        public void Shoot()
        {
            
        }

        public bool CanShoot => false;
        
        public int Bullets { get; }
        
        public int StartBullets { get; }
        
        public void VisualizeBullets()
        {
            
        }

        public void AddBullets(int bullets)
        {
            
        }
    }
}