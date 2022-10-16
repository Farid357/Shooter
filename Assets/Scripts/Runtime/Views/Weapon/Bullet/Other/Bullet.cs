using Shooter.Model;
using Sirenix.OdinInspector;

namespace Shooter.GameLogic
{
    public abstract class Bullet : SerializedMonoBehaviour, IBullet
    {
        public abstract void Throw();
    }
}