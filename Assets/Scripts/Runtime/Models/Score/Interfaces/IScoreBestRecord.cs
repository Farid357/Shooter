namespace Shooter.Model
{
    public interface IScoreBestRecord
    {
        void Increase(int amount);
        
        bool CanIncrease(int amount);
    }
}