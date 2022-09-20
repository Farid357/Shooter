using System;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class IndependentTimer : IUpdateble, IIndependentTimer
    {
        private readonly IView<float> _view;
        private readonly float _cooldown;
        private float _seconds;

        public IndependentTimer(IView<float> view, float cooldown = 1.2f)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _cooldown = cooldown.TryThrowLessThanOrEqualsToZeroException();
            _seconds = cooldown.TryThrowLessThanOrEqualsToZeroException();
        }

        public bool IsEnded => _seconds == 0;

        public void Update(float deltaTime)
        {
            _seconds = Math.Max(0, _seconds - deltaTime);

            if (_seconds == 0)
                _seconds = _cooldown;
            
            _view.Visualize(_seconds);
        }
    }
}