using Shooter.Model;

namespace Shooter.Test
{
    public sealed class DummyBulletsView : IBulletsView
    {
        public int Bullets { get; private set; }

        public IShotView ShotView { get; }

        public void Visualize(int bullets)
        {
            Bullets = bullets;
        }
    }
}