using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Timer : IUpdateble, ITimer
    {
        private readonly float _cooldown;
        private float _seconds;

        public Timer(float cooldown)
        {
            _cooldown = cooldown.TryThrowLessThanOrEqualsToZeroException();
        }

        public Timer(IFactory<IEnemyMovement> factory) : this(1.2f)
        {
            
        }

        public bool IsEnded => _seconds == 0;

        public void Update(float deltaTime)
        {
            _seconds += deltaTime;

            if (_seconds >= _cooldown)
            {
                _seconds = 0;
            }
        }
    }
}