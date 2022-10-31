using Shooter.Model;

namespace Shooter.Test
{
    public sealed class DummyBulletsView : IBulletsView
    {
        public int Bullets { get; private set; }

        public IShotView ShotView => new DummyShotView();

        public void Visualize(int bullets)
        {
            Bullets = bullets;
        }

        public void Disable()
        {
            
        }
    }
}