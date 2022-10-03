namespace Shooter.Model
{
    public interface ISwitchingGoodAction
    {
        bool CanSwitchLeft();

        bool CanSwitchRight();
        
        void SwitchLeft();
        
        void SwitchRight();

    }
}