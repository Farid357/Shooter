namespace Shooter.Model
{
    public interface ITimer
    {
        bool IsEnded { get; }
        
        void Restart(float newTime);
    }
}