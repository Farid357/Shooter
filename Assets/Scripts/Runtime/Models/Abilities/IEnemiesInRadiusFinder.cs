using System.Collections.Generic;

namespace Shooter.Model
{
    public interface IEnemiesInRadiusFinder
    {
        bool TryFind(out List<IEnemy> enemies);
    }
}