using Shooter.Model;
using Shooter.Tools;
using Sirenix.OdinInspector;

namespace Shooter.GameLogic
{
    public abstract class BulletsFactory : SerializedMonoBehaviour, IFactory<IBullet>
    {
        public abstract IBullet Create();
    }
}