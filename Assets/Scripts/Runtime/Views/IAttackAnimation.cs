using Cysharp.Threading.Tasks;

namespace Shooter.GameLogic
{
    public interface IAttackAnimation
    {
        UniTask Play();
    }
}