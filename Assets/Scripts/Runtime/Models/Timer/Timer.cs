using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Timer : IUpdateble, ITimer
    {
        private readonly IView<float> _view;
        private float _seconds;

        public Timer(IView<float> view, float cooldown = 1.2f)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _seconds = cooldown.TryThrowLessThanOrEqualsToZeroException();
        }
        
        public bool IsEnded => _seconds == 0;
        
        public void Restart(float newTime) => _seconds = newTime.TryThrowLessThanOrEqualsToZeroException();

        public void Update(float deltaTime)
        {
            _seconds = Math.Max(0, _seconds - deltaTime);
            _view.Visualize(_seconds);
        }
    }
}