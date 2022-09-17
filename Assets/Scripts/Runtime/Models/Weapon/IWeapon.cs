namespace Shooter.Model
{
    public interface IWeapon : IShootingWeapon
    {
        public void VisualizeBullets();
        
        public void AddBullets(int bullets);
        
        public int Bullets { get; }

    }
}