using System;
using Shooter.Model;
using Shooter.Tools;

namespace Shooter.GameLogic
{
    public interface IBulletsFactory : IFactory<IBullet>
    {
        event Action<Bullet> OnCreated;
    }
}