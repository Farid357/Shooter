namespace Shooter.Model
{
    public interface IBulletsView
    {
        IShotView ShotView { get; }
        
        void Visualize(int bullets);
    }
}