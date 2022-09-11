namespace Shooter.Model
{
    public interface IWeapon
    {
        public void Shoot();

        public void VisualizeBullets();
        
        public void AddBullets(int bullets);
        
        public int Bullets { get; }
        
        public bool CanShoot { get; }
    }
}