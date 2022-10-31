namespace Shooter.Model
{
    public interface IWeapon : IShootingWeapon
    {
        int Bullets { get; }
        
        int StartBullets { get; }
        
        void VisualizeBullets();
        
        void AddBullets(int bullets);
        

    }
}