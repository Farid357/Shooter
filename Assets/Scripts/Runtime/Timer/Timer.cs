using System;
using System.Threading.Tasks;
using Shooter.Tools;

namespace Shooter.Model
{
    public sealed class Timer : ITimer
    {
        private readonly float _seconds;
        private bool _hasEnded;
        
        public Timer(float seconds)
        {
            _seconds = seconds.TryThrowLessOrEqualsToZeroException();
        }

        public async Task End()
        {
            if (_hasEnded)
                throw new InvalidOperationException(nameof(End));
            
            await Task.Delay(TimeSpan.FromSeconds(_seconds));
            _hasEnded = true;
        }

        public ITimer Restart() => new Timer(_seconds);
        
    }
}