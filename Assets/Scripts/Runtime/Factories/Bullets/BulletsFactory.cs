using Shooter.Model;
using Shooter.Root;
using Shooter.Tools;
using Sirenix.OdinInspector;

namespace Shooter.GameLogic
{
    public abstract class BulletsFactory : SerializedMonoBehaviour, IFactory<IBullet>
    {
        public abstract void Init(ISystemUpdate systemUpdate);
        
        public abstract IBullet Create();
    }
}