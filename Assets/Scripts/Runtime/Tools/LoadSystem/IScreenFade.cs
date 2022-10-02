using System;

namespace Shooter.LoadSystem
{
    public interface IScreenFade
    {
        event Action OnDarkened;
        
        void FadeIn();
        
        void FadeOut();
    }
}