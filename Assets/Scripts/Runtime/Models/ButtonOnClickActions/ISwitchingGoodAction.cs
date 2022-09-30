namespace Shooter.Model
{
    public interface ISwitchingGoodAction : IButtonClickAction
    {
        int Index { get; }
        
        bool CanSwitch(int index);
    }
}