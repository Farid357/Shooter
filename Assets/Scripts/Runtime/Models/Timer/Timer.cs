using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Timer : IUpdateble, ITimer
    {
        private readonly IView<float> _view;
        private float _cooldown;

        public Timer(IView<float> view, float cooldown = 1.2f)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _cooldown = cooldown.TryThrowLessThanOrEqualsToZeroException();
        }
        
        public bool IsEnded => _cooldown == 0;
        
        public void Restart(float newTime) => _cooldown = newTime.TryThrowLessThanOrEqualsToZeroException();

        public void Update(float deltaTime)
        {
            _cooldown = Math.Max(0, _cooldown - deltaTime);
            _view.Visualize(_cooldown);
        }
    }
}