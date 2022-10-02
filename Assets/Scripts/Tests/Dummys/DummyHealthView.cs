using Shooter.Model;

namespace Shooter.Test
{
    public sealed class DummyHealthView : IHealthView
    {
        public bool HasVusualized { get; private set; }

        public void Visualize(int health)
        {
            HasVusualized = true;
        }
    }
}