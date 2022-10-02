namespace Shooter.Model
{
    public interface IWeapon : IShootingWeapon
    {
        public int Bullets { get; }
        
        public int StartBullets { get; }
        
        public void VisualizeBullets();
        
        public void AddBullets(int bullets);
        

    }
}