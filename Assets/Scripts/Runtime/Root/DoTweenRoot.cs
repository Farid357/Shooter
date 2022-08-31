using DG.Tweening;

namespace Shooter.Root
{
    public sealed class DoTweenRoot : CompositeRoot
    {
        public override void Compose() => DOTween.Init();
        
    }
}