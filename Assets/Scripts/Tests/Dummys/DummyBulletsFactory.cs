using Shooter.Model;
using Shooter.Tools;

namespace Shooter.Test
{
    public sealed class DummyBulletsFactory : IFactory<IBullet>
    {
        public IBullet Create() => new DummyBullet();
        
    }
}